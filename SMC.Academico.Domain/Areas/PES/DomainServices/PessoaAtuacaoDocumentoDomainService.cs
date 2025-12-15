using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Domain.Areas.FIN.ValueObjects;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.ServiceContract.Areas.GED.Interfaces;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;


namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaAtuacaoDocumentoDomainService : AcademicoContextDomain<PessoaAtuacaoDocumento>
    {
        #region [Services]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private DocumentoRequeridoDomainService DocumentoRequeridoDomainService => Create<DocumentoRequeridoDomainService>();

        private ProcessoDomainService ProcessoDomainService => Create<ProcessoDomainService>();

        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService => Create<ConfiguracaoProcessoDomainService>();

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService => Create<ConfiguracaoEtapaDomainService>();

        private ITipoDocumentoService TipoDocumentoService => Create<ITipoDocumentoService>();

        private PessoaAtuacaoBloqueioDomainService PessoaAtuacaoBloqueioDomainService => Create<PessoaAtuacaoBloqueioDomainService>();

        private PessoaAtuacaoBloqueioItemDomainService PessoaAtuacaoBloqueioItemDomainService => Create<PessoaAtuacaoBloqueioItemDomainService>();

        private MotivoBloqueioDomainService MotivoBloqueioDomainService => Create<MotivoBloqueioDomainService>();

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => Create<SolicitacaoServicoDomainService>();

        #endregion [Services]

        public List<SMCDatasourceItem> BuscarDocumentosSelect(long seqPessoaAtuacao, long? seq)
        {
            //Buscar os dados do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            //Buscar o processo que tem o serviço de token ENTREGA_DOCUMENTACAO e está ativo (data válida)
            var specProcesso = new ProcessoFilterSpecification()
            {
                TokenServico = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                SeqUnidadeResponsavel = dadosOrigem.SeqEntidadeResponsavel,
                ProcessoAtivo = true
            };
            var seqProcesso = ProcessoDomainService.SearchProjectionByKey(specProcesso, p => p.Seq);

            //Buscar a configuração de processo que tem o token ENTREGA_DOCUMENTACAO que atende a pessoa atuação
            //Primeiro pesquisamos a configuração por tipo de atuação + curso
            var specConfiguracaoProcesso = new ConfiguracaoProcessoFilterSpecification()
            {
                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                SeqCursoOfertaLocalidadeTurno = dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                SeqProcesso = seqProcesso
            };
            var seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcesso, p => p.Seq);

            if (seqConfiguracaoProcesso == 0)
            {
                //Se não achou a configuração por curso, verifica por nível de ensino + tipo de atuação
                var specConfiguracaoProcesso2 = new ConfiguracaoProcessoFilterSpecification()
                {
                    SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqProcesso = seqProcesso
                };
                seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcesso2, p => p.Seq);
            }

            //Se não localizar a configuração do processo, exibir exceção de erro
            if (seqConfiguracaoProcesso == 0)
                throw new PessoaAtuacaoDocumentoException();

            //Busca os documentos requeridos da configuração de processo 
            var specDoc = new DocumentoRequeridoFilterSpecification()
            {
                SeqConfiguracaoProcesso = seqConfiguracaoProcesso
            };
            var listaDoc = DocumentoRequeridoDomainService.SearchProjectionBySpecification(specDoc, d => d.SeqTipoDocumento).ToList();

            //Busca as descrições dos tipos de documento nos dados mestres
            var tipos = TipoDocumentoService.BuscarTiposDocumentosSelect(listaDoc);

            var documentoPessoaAtuacaoSpec = new PessoaAtuacaoDocumentoFilterSpecification()
            {
                SeqPessoaAtuacao = seqPessoaAtuacao
            };

            if(seq.HasValue && seq.Value > 0)
            {
                var documentoPessoAtuacao = this.SearchProjectionBySpecification(documentoPessoaAtuacaoSpec, d => d.SeqTipoDocumento).ToList();
                var descricaoItemJaExistente = TipoDocumentoService.BuscarTiposDocumentosSelect(documentoPessoAtuacao);

                tipos.AddRange(descricaoItemJaExistente);
            }
            

            return tipos;

        }

        public SMCPagerData<PessoaAtuacaoDocumentoListarVO> BuscarTiposDocumentoLista(PessoaAtuacaoDocumentoFiltroVO filtro)
        {
            var spec = filtro.Transform<PessoaAtuacaoDocumentoFilterSpecification>();

            // Limpando a ordenação que será feita manualmente e setando o MaxResults para não buscar todos os registros da tabela
            int qtdeRegistros = this.Count();

            spec.MaxResults = qtdeRegistros > 0 ? qtdeRegistros : int.MaxValue;
            spec.ClearOrderBy();

            var lista = this.SearchProjectionBySpecification(spec, d => new PessoaAtuacaoDocumentoListarVO
            {
                Seq = d.Seq,
                SeqPessoaAtuacao = d.SeqPessoaAtuacao,
                SeqTipoDocumento = d.SeqTipoDocumento,
                DataEntrega = d.DataEntrega,
                NumeroProtocoloSolicitado = d.SolicitacaoDocumentoRequerido.SolicitacaoServico.NumeroProtocolo,
                Observacao = d.Observacao,
                UidArquivo = d.ArquivoAnexado.UidArquivo,
                SeqArquivoAnexado = d.ArquivoAnexado.Seq,
            });

            List<PessoaAtuacaoDocumentoListarVO> listaVO = lista.ToList();

            foreach (var item in listaVO)
            {
                var descricoes = TipoDocumentoService.BuscarDescricaoTipoDocumento(item.SeqTipoDocumento.Value);
                item.DescricaoTipoDocumento = descricoes;
            }

            int total = listaVO.Count();

            // Ordenação manual em toda a lista, não somente na página atual
            List<SMCSortInfo> listaOrdenacao = filtro.PageSettings.SortInfo;

            foreach (var sort in listaOrdenacao)
            {
                if (sort.FieldName == nameof(PessoaAtuacaoDocumentoListarVO.DescricaoTipoDocumento))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                    {
                        listaVO = listaVO.OrderBy(o => o.DescricaoTipoDocumento).ToList();
                    }
                    else
                    {
                        listaVO = listaVO.OrderByDescending(o => o.DescricaoTipoDocumento).ToList();
                    }
                }
                if (sort.FieldName == nameof(PessoaAtuacaoDocumentoListarVO.DataEntrega))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                    {
                        listaVO = listaVO.OrderBy(o => o.DataEntrega).ToList();
                    }
                    else
                    {
                        listaVO = listaVO.OrderByDescending(o => o.DataEntrega).ToList();
                    }
                }
            }

            // Configuração da página, recuperando os registros da página atual
            listaVO = listaVO.Skip((filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize).Take(filtro.PageSettings.PageSize).ToList();

            return new SMCPagerData<PessoaAtuacaoDocumentoListarVO>(listaVO, total);
        }



        public List<string> BuscarItensBloqueio(long seqPessoaAtuacao)
        {
            var gruposAuxiliares = RetornaGruposAuxiliaresDocumentoRequerido(seqPessoaAtuacao);
            if (gruposAuxiliares.Count() > 0)
            {
                var listaSeqsTipoDocumentoEntregues = this.SearchBySpecification(new PessoaAtuacaoDocumentoFilterSpecification()
                { SeqPessoaAtuacao = seqPessoaAtuacao }).Select(s => s.SeqTipoDocumento).ToList();

                var tiposDocumento = TipoDocumentoService.BuscarTiposDocumentosSelect(gruposAuxiliares.SelectMany(i => i.Itens).ToList());

                var listaRetorno = new List<string>();

                var listaItensGrupo = tiposDocumento.Select(t => new
                {
                    SeqTipoDocumento = t.Seq,
                    DescricaoTipoDocumento = t.Descricao,
                    MinimoObrigatorio = gruposAuxiliares.Where(g => g.Itens.Contains(t.Seq)).FirstOrDefault().MinimoObrigatorio,
                    NomeGrupo = gruposAuxiliares.Where(g => g.Itens.Contains(t.Seq)).FirstOrDefault().NomeGrupo

                }).ToList();

                string nomeGrupo = listaItensGrupo.FirstOrDefault().NomeGrupo;

                var itensGrupo = listaItensGrupo.Select(s => s.DescricaoTipoDocumento).ToList();
                var numMinimoObrigatorioGrupo = listaItensGrupo.FirstOrDefault().MinimoObrigatorio;

                int quantosDocumentosGruposEntregues = 0;

                foreach (var item in gruposAuxiliares)
                {
                    quantosDocumentosGruposEntregues = item.Itens.Count(c => listaSeqsTipoDocumentoEntregues.Contains(c));
                }

                string retornoGrupo = string.Empty;

                if (numMinimoObrigatorioGrupo > quantosDocumentosGruposEntregues)
                {
                    retornoGrupo = $"{nomeGrupo}: {string.Join(", ", itensGrupo)} - Mínimo de itens obrigatórios: {numMinimoObrigatorioGrupo}";
                }

                //Buscar os itens de bloqueio da pessoa atuação com situação bloqueado que possuem como motivo do bloqueio o token ENTREGA_DOCUMENTACAO
                var spec = new PessoaAtuacaoBloqueioFilterSpecification
                {
                    SeqPessoaAtuacao = seqPessoaAtuacao,
                    SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                    TokenMotivoBloqueio = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                };

                var listaDocumentosBloqueados = PessoaAtuacaoBloqueioDomainService.SearchProjectionBySpecification(spec, a => new
                {
                    Itens = a.Itens.Select(s => s.Descricao)

                }).ToList();

                var itensBloqueados = listaDocumentosBloqueados.SelectMany(s => s.Itens).ToList();


                foreach (var item in itensBloqueados)
                {
                    if (retornoGrupo.Contains(item))
                    {
                        listaRetorno.Add(retornoGrupo);
                    }
                    else
                    {
                        listaRetorno.Add(item);
                    }
                }

                return listaRetorno;
            }
            else
                return new List<string>();
        }

        public void ExcluirDocumento(long seqPessoaAtuacaoDocumento)
        {
            //Garantimos que se caso ocorra algum problema na deleção, seja feito um rollback
            using (var unityOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    //Busca o documento a ser excluido
                    var spec = new SMCSeqSpecification<PessoaAtuacaoDocumento>(seqPessoaAtuacaoDocumento);
                    var documento = this.SearchByKey(spec);

                    //Verifica se o documento é obrigatório para gerar o bloqueio 
                    if (VerificaDocumentoObrigatorio(documento.SeqTipoDocumento, documento.SeqPessoaAtuacao))
                    {
                        GerarBloqueio(documento.SeqPessoaAtuacao, documento.SeqTipoDocumento);
                    }

                    //Realiza a deleção
                    this.DeleteEntity(seqPessoaAtuacaoDocumento);

                    unityOfWork.Commit();
                }
                catch (Exception ex)
                {
                    unityOfWork.Rollback();
                    throw;
                }
            }

        }

        public List<GrupoAuxiliarDocumentoRequeridoVO> RetornaGruposAuxiliaresDocumentoRequerido(long seqPessoaAtuacao)
        {
            //Buscar os dados do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            //Buscar o processo que tem o serviço de token ENTREGA_DOCUMENTACAO e está ativo (data válida)
            var specProcesso = new ProcessoFilterSpecification()
            {
                TokenServico = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                SeqUnidadeResponsavel = dadosOrigem.SeqEntidadeResponsavel,
                ProcessoAtivo = true
            };
            var seqProcesso = ProcessoDomainService.SearchProjectionByKey(specProcesso, p => p.Seq);

            //Buscar a configuração de processo que tem o token ENTREGA_DOCUMENTACAO que atende a pessoa atuação
            //Primeiro pesquisamos a configuração por tipo de atuação + curso
            var specConfiguracaoProcessoPorCurso = new ConfiguracaoProcessoFilterSpecification()
            {
                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                SeqCursoOfertaLocalidadeTurno = dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                SeqProcesso = seqProcesso
            };
            var seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcessoPorCurso, p => p.Seq);

            if (seqConfiguracaoProcesso == 0)
            {
                //Se não achou a configuração por curso, verifica por nível de ensino + tipo de atuação
                var specConfiguracaoProcessoPorNivelEnsino = new ConfiguracaoProcessoFilterSpecification()
                {
                    SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqProcesso = seqProcesso
                };
                seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcessoPorNivelEnsino, p => p.Seq);
            }

            var specConfiguracaoEtapa = new ConfiguracaoEtapaFilterSpecification()
            {
                SeqConfiguracaoProcesso = seqConfiguracaoProcesso
            };

            var retorno = ConfiguracaoEtapaDomainService.SearchProjectionBySpecification(specConfiguracaoEtapa, x => new
            {
                GruposDocumentoRequerido = x.GruposDocumentoRequerido.Select(g => new GrupoAuxiliarDocumentoRequeridoVO
                {
                    Seq = g.Seq,
                    MinimoObrigatorio = g.MinimoObrigatorio,
                    NomeGrupo = g.Descricao,
                    Itens = g.Itens.Select(i => i.DocumentoRequerido.SeqTipoDocumento).ToList()
                })
            }).ToList();

            return retorno.SelectMany(r => r.GruposDocumentoRequerido).ToList();
        }

        public bool VerificaDocumentoObrigatorio(long seqTipoDocumento, long seqPessoaAtuacao)
        {
            var gruposAuxiliares = RetornaGruposAuxiliaresDocumentoRequerido(seqPessoaAtuacao);

            bool documentoComBloqueio = false;
            bool minimoEntregue = false;

            //Busca os documentos que estão em grupo de documentos
            var grupoDocumento = gruposAuxiliares.Where(w => w.Itens.Contains(seqTipoDocumento)).ToList();

            if (grupoDocumento.Any())
            {
                var listaGrupoTipoDocumentos = grupoDocumento.FirstOrDefault().Itens;
                var documentosEntregues = this.SearchBySpecification(new PessoaAtuacaoDocumentoFilterSpecification() { ListaSeqsTipoDocumento = listaGrupoTipoDocumentos.ToArray(), SeqPessoaAtuacao = seqPessoaAtuacao }).Count();

                minimoEntregue = documentosEntregues > gruposAuxiliares.FirstOrDefault().MinimoObrigatorio;
                documentoComBloqueio = documentosEntregues >= gruposAuxiliares.FirstOrDefault().MinimoObrigatorio;
            }

            if (minimoEntregue)
            {
                //Busca o tipo de documento obrigatório, cujo o processo de entrega está ativo e possui o token de serviço ENTREGA_DOCUMENTACAO
                var spec = new DocumentoRequeridoFilterSpecification()
                {
                    SeqTipoDocumento = seqTipoDocumento,
                    Obrigatorio = true,
                    ProcessoAtivo = true,
                    Token = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                };

                var docObrigatorio = DocumentoRequeridoDomainService.SearchBySpecification(spec);
                documentoComBloqueio = docObrigatorio.Any();
            }

            if (!documentoComBloqueio)
            {
                var spec = new DocumentoRequeridoFilterSpecification()
                {
                    SeqTipoDocumento = seqTipoDocumento,
                    Obrigatorio = true,
                    ProcessoAtivo = true,
                    Token = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                };

                var docObrigatorio = DocumentoRequeridoDomainService.SearchBySpecification(spec);
                documentoComBloqueio = docObrigatorio.Any();
            }

            return documentoComBloqueio;
        }
        public bool VerificarServico(long seqPessoaAtuacao)
        {
            var filtroSpec = new SolicitacaoServicoFilterSpecification
            {
                SeqPessoaAtuacao = seqPessoaAtuacao,
                CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento },
                TokenTipoServico = new List<string> { TOKEN_TIPO_SERVICO.RENOVACAO_MATRICULA, TOKEN_TIPO_SERVICO.MATRICULA_REABERTURA, TOKEN_TIPO_SERVICO.ENTREGA_DOCUMENTACAO }
            };

            var solicitacaoServico = SolicitacaoServicoDomainService.SearchProjectionBySpecification(filtroSpec, p => new
            {
                Seq = p.Seq,
                DescricaoServico = p.ConfiguracaoProcesso.Processo.Servico.Descricao
            }).Any();

            return solicitacaoServico;
        }

        public void GerarBloqueio(long seqPessoaAtuacao, long seqTipoDocumento)
        {
            //Associa a pessoa atuação para o bloqueio
            var associaBloqueio = new PessoaAtuacaoBloqueio()
            {
                SeqMotivoBloqueio = this.MotivoBloqueioDomainService.BuscarSeqMotivoBloqueioPorToken(TOKEN_MOTIVO_BLOQUEIO.ENTREGA_DOCUMENTACAO),
                SeqPessoaAtuacao = seqPessoaAtuacao,
                Descricao = "Pendência na entrega da documentação",
                DataBloqueio = DateTime.Now,
                Observacao = string.Empty,
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                CadastroIntegracao = true
            };

            //Gera um item para o bloqueio criado
            var descricaoTipoDocumento = TipoDocumentoService.BuscarDescricaoTipoDocumento(seqTipoDocumento);

            associaBloqueio.Itens = new List<PessoaAtuacaoBloqueioItem>();
            associaBloqueio.Itens.Add(new PessoaAtuacaoBloqueioItem()
            {
                Descricao = descricaoTipoDocumento,
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                CodigoIntegracaoSistemaOrigem = seqTipoDocumento.ToString()
            });

            PessoaAtuacaoBloqueioDomainService.SaveEntity(associaBloqueio);
        }
        public long SalvarDocumento(PessoaAtuacaoDocumentoListarVO pessoaAtuacaoDocumento)
        {
            //Busca solicitação nova ou em aberto, com Token de serviço "ENTREGA_DOCUMENTACAO" ou "SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU" para a pessoa atuação
            var filtroSpec = new SolicitacaoServicoFilterSpecification
            {
                SeqPessoaAtuacao = pessoaAtuacaoDocumento.SeqPessoaAtuacao,
                CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento },
                TokensServico = new List<string> { TOKEN_SERVICO.ENTREGA_DOCUMENTACAO, TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU },
            };

            var solicitacaoServico = SolicitacaoServicoDomainService.SearchProjectionByKey(filtroSpec, p => new
            {
                Seq = p.Seq,
                DescricaoServico = p.ConfiguracaoProcesso.Processo.Servico.Descricao
            });

            //Se achar, abortar ação de salvar
            if (solicitacaoServico != null && solicitacaoServico.Seq != 0 && pessoaAtuacaoDocumento.Seq == 0)
            {
                throw new PessoaAtuacaoDocumentoSituacaoDocumentoException(solicitacaoServico.DescricaoServico);
            }

            //Busca no Dados Mestres o tipo de documento
            var TipoDocumento = TipoDocumentoService.BuscarTipoDocumento(pessoaAtuacaoDocumento.SeqTipoDocumento.Value);

            //Verifica se o documento é único e o aluno está associado ao mesmo
            var filtro = new PessoaAtuacaoDocumentoFilterSpecification
            {
                SeqPessoaAtuacao = pessoaAtuacaoDocumento.SeqPessoaAtuacao,
                SeqTipoDocumento = TipoDocumento.Seq,
            };
            var associado = this.SearchByKey(filtro);

            var documentoUnico = TipoDocumento.Unico;

            //Caso o documento anexado seja requerido de uma solicitação, abortar a operação
            if (pessoaAtuacaoDocumento.SeqSolicitacaoDocumentoRequerido.HasValue)
            {
                throw new PessoaAtuacaoDocumentoDocumentoRequeridoException();
            }

            //Caso o arquivo anexado seja alterado, gravar um novo registro na tabela. Para efeito de rastreabilidade
            if (pessoaAtuacaoDocumento.SeqArquivoAnexado != null && pessoaAtuacaoDocumento.ArquivoAnexado?.State == SMCUploadFileState.Changed)
            {
                var modelLog = pessoaAtuacaoDocumento.Transform<PessoaAtuacaoDocumento>();

                if (modelLog != null)
                {
                    if (modelLog.SeqArquivoAnexado.HasValue)
                    {
                        PreencherValidacaoArquivo(modelLog, pessoaAtuacaoDocumento);

                        var arquivoLog = pessoaAtuacaoDocumento.ArquivoAnexado.Transform<ArquivoAnexado>();
                        this.ArquivoAnexadoDomainService.InsertEntity(arquivoLog);

                        this.UpdateFields(new PessoaAtuacaoDocumento { Seq = modelLog.Seq, SeqArquivoAnexado = arquivoLog.Seq }, x => x.SeqArquivoAnexado);
                    }
                }
            }

            //Garantimos caso a ação seja de alteração, não entrar na regra de documento único e associado.
            if (pessoaAtuacaoDocumento.SeqArquivoAnexado == null && (pessoaAtuacaoDocumento.SeqArquivoAnexado != 0 || pessoaAtuacaoDocumento.ArquivoAnexado?.State == SMCUploadFileState.Changed))
            {
                //Caso o documento seja único e esteja associado, abortar operação
                if (associado != null && documentoUnico)
                {
                    throw new PessoaAtuacaoDocumentoDocumentoUnicoAssociadoException(TipoDocumento.Descricao);
                }
            }

            //Verifcamos se o arquivo anexado foi alterado
            var arquivoAlterado = pessoaAtuacaoDocumento.ArquivoAnexado?.State == SMCUploadFileState.Changed;

            if (arquivoAlterado == false && pessoaAtuacaoDocumento.SeqSolicitacaoDocumentoRequerido.HasValue)
            {
                throw new PessoaAtuacaoDocumentoArquivoAlteradoException();
            }

            //Busca o tipo de documento obrigatório, cujo o processo de entrega está ativo e possui o token de serviço ENTREGA_DOCUMENTACAO
            var documentoRequeridoFilterSpec = new PessoaAtuacaoBloqueioFilterSpecification()
            {
                SeqPessoaAtuacao = pessoaAtuacaoDocumento.SeqPessoaAtuacao,
                SituacaoBloqueio = SituacaoBloqueio.Bloqueado,
                TokenMotivoBloqueio = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                SeqsTiposDocumentosBloqueio = new List<string>() { TipoDocumento.Seq.ToString() }
            };
            int total = 0;
            var pessoaAtuacaoBloqueio = this.PessoaAtuacaoBloqueioDomainService.SearchBySpecification(documentoRequeridoFilterSpec, out total,
                                                IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_Pessoa |
                                                IncludesPessoaAtuacaoBloqueio.PessoaAtuacao_DadosPessoais |
                                                IncludesPessoaAtuacaoBloqueio.MotivoBloqueio |
                                                IncludesPessoaAtuacaoBloqueio.MotivoBloqueio_TipoBloqueio |
                                                IncludesPessoaAtuacaoBloqueio.Comprovantes_ArquivoAnexado |
                                                IncludesPessoaAtuacaoBloqueio.Itens)
                                           .ToList();
            var docObrigatorio = PessoaAtuacaoBloqueioDomainService.SearchBySpecification(documentoRequeridoFilterSpec);
            bool existeDocumentoComBloqueio = pessoaAtuacaoBloqueio.Any();

            if (existeDocumentoComBloqueio)
            {
                //Prepara a atualização para desbloqueio
                foreach (var doc in pessoaAtuacaoBloqueio)
                {
                    doc.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                    doc.TipoDesbloqueio = TipoDesbloqueio.Efetivo;
                    doc.DataDesbloqueioEfetivo = DateTime.Now;
                    doc.UsuarioDesbloqueioEfetivo = SMCContext.User?.Identity?.Name;
                    doc.JustificativaDesbloqueio = "Entrega de documentação";
                    if (doc.Itens != null)
                    {
                        foreach (var pessoaAtuacaoBloqueioItem in doc.Itens)
                        {
                            pessoaAtuacaoBloqueioItem.SituacaoBloqueio = SituacaoBloqueio.Desbloqueado;
                            pessoaAtuacaoBloqueioItem.DataDesbloqueio = DateTime.Now;
                            pessoaAtuacaoBloqueioItem.UsuarioDesbloqueio = SMCContext.User?.Identity?.Name;
                        };
                    }
                    //Efetiva a atualização do desbloqueio e do item
                    PessoaAtuacaoBloqueioDomainService.SaveEntity(pessoaAtuacaoBloqueio);
                }
            }

            //Possuir o bloqueio acadêmico ENTREGA_DOCUMENTACAO, com situação bloqueado e código de integração igual ao seq do tipo documento
            var gruposAuxiliares = RetornaGruposAuxiliaresDocumentoRequerido(pessoaAtuacaoDocumento.SeqPessoaAtuacao).ToList();
            if (gruposAuxiliares.Count() > 0)
            {
                var minObrigatorio = gruposAuxiliares.FirstOrDefault().MinimoObrigatorio;
                var seqsTipoDocumentoGruposAuxiliares = gruposAuxiliares.SelectMany(s => s.Itens).ToList();

                List<long> listaSeqsTipoDocumentoEntregues = new List<long>();

                if (minObrigatorio > 1)
                {
                    listaSeqsTipoDocumentoEntregues = this.SearchBySpecification(new PessoaAtuacaoDocumentoFilterSpecification()
                    { SeqPessoaAtuacao = pessoaAtuacaoDocumento.SeqPessoaAtuacao }).Select(s => s.SeqTipoDocumento).ToList();
                }

                List<string> seqsPesquisados = new List<string>();

                var pertenceGrupo = seqsTipoDocumentoGruposAuxiliares.Any(a => a == pessoaAtuacaoDocumento.SeqTipoDocumento);
                var documentosEntregePertencentesGrupo = seqsTipoDocumentoGruposAuxiliares.Count(c => listaSeqsTipoDocumentoEntregues.Contains(c));

                if (pertenceGrupo && minObrigatorio >= 1)
                {

                    foreach (var item in seqsTipoDocumentoGruposAuxiliares)
                    {
                        seqsPesquisados.Add(item.ToString());
                    }
                }
                else
                {
                    seqsPesquisados.Add(pessoaAtuacaoDocumento.SeqTipoDocumento.ToString());
                }
            }

            //Transformação do VO em modelo para salvar
            var model = pessoaAtuacaoDocumento.Transform<PessoaAtuacaoDocumento>();

            if (pessoaAtuacaoDocumento.SeqArquivoAnexado == null && pessoaAtuacaoDocumento.ArquivoAnexado?.State == SMCUploadFileState.Changed)
            {
                //Adiciona o arquivo que foi anexado ao modelo
                PreencherValidacaoArquivo(model, pessoaAtuacaoDocumento);
                //Salva o documento da pessoa atuação e retorna seu sequencial
            }

            EnsureFileIntegrity(model, s => s.SeqArquivoAnexado, a => a.ArquivoAnexado);

            SaveEntity(model);

            return model.Seq;
        }
        private void PreencherValidacaoArquivo(PessoaAtuacaoDocumento model, PessoaAtuacaoDocumentoListarVO pessoaAtuacaoDocumentoListarVO)
        {
            //Modelando o arquivo a ser salvo
            var arquivoAnexado = new ArquivoAnexado();

            //Verificamos se o arquivo que veio no VO está nulo.
            //Se estiver, significa que o arquivo já pode estar gravado em banco e neste caso, temos que recuperá-lo.
            if (pessoaAtuacaoDocumentoListarVO.ArquivoAnexado.FileData == null)
            {
                //Buscamos os dados do arquivo para preencher os dados obrigatórios
                var spec = new SMCSeqSpecification<ArquivoAnexado>(pessoaAtuacaoDocumentoListarVO.SeqArquivoAnexado.GetValueOrDefault());
                var pessoaAtuacaoDocumentoArquivo = ArquivoAnexadoDomainService.SearchByKey(spec);
                arquivoAnexado.Conteudo = pessoaAtuacaoDocumentoArquivo.Conteudo;
                arquivoAnexado.UidArquivo = pessoaAtuacaoDocumentoArquivo.UidArquivo;
            }
            else
            {
                //Caso o arquivo VO não seja nulo, devemos criar um novo arquivo
                arquivoAnexado.Conteudo = pessoaAtuacaoDocumentoListarVO.ArquivoAnexado.FileData;
                arquivoAnexado.UidArquivo = Guid.NewGuid();
            }
            arquivoAnexado.Nome = pessoaAtuacaoDocumentoListarVO.ArquivoAnexado.Name;
            arquivoAnexado.Tamanho = (int)pessoaAtuacaoDocumentoListarVO.ArquivoAnexado.Size;
            arquivoAnexado.Tipo = pessoaAtuacaoDocumentoListarVO.ArquivoAnexado.Type;
            arquivoAnexado.Seq = pessoaAtuacaoDocumentoListarVO.SeqArquivoAnexado.GetValueOrDefault();

            model.ArquivoAnexado = arquivoAnexado;
        }

        public long[] BuscarSeqsDocumentosConfigurados(long seqPessoaAtuacao)
        {
            //Buscar os dados do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            //Buscar o processo que tem o serviço de token ENTREGA_DOCUMENTACAO e está ativo (data válida)
            var specProcesso = new ProcessoFilterSpecification()
            {
                TokenServico = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                SeqUnidadeResponsavel = dadosOrigem.SeqEntidadeResponsavel,
                ProcessoAtivo = true
            };
            var seqProcesso = ProcessoDomainService.SearchProjectionByKey(specProcesso, p => p.Seq);

            //Buscar a configuração de processo que tem o token ENTREGA_DOCUMENTACAO que atende a pessoa atuação
            //Primeiro pesquisamos a configuração por tipo de atuação + curso
            var specConfiguracaoProcesso = new ConfiguracaoProcessoFilterSpecification()
            {
                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                SeqCursoOfertaLocalidadeTurno = dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                SeqProcesso = seqProcesso
            };
            var seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcesso, p => p.Seq);

            if (seqConfiguracaoProcesso == 0)
            {
                //Se não achou a configuração por curso, verifica por nível de ensino + tipo de atuação
                var specConfiguracaoProcesso2 = new ConfiguracaoProcessoFilterSpecification()
                {
                    SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqProcesso = seqProcesso
                };
                seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcesso2, p => p.Seq);
            }

            //Se não localizar a configuração do processo, exibir exceção de erro
            if (seqConfiguracaoProcesso == 0)
                throw new PessoaAtuacaoDocumentoException();

            //Busca os documentos requeridos da configuração de processo 
            var specDoc = new DocumentoRequeridoFilterSpecification()
            {
                SeqConfiguracaoProcesso = seqConfiguracaoProcesso
            };
            var listaDoc = DocumentoRequeridoDomainService.SearchProjectionBySpecification(specDoc, d => d.SeqTipoDocumento).ToArray();

            return listaDoc;
        }

        public bool NaoExisteProcesso(long seqPessoaAtuacao)
        {
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            var specProcesso = new ProcessoFilterSpecification()
            {
                TokenServico = TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                SeqUnidadeResponsavel = dadosOrigem.SeqEntidadeResponsavel,
                ProcessoAtivo = true
            };
            var seqProcesso = ProcessoDomainService.SearchProjectionByKey(specProcesso, p => p.Seq);
            var specConfiguracaoProcessoPorCurso = new ConfiguracaoProcessoFilterSpecification()
            {
                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                SeqCursoOfertaLocalidadeTurno = dadosOrigem.SeqCursoOfertaLocalidadeTurno,
                SeqProcesso = seqProcesso
            };
            var seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcessoPorCurso, p => p.Seq);

            if (seqConfiguracaoProcesso == 0)
            {
                //Se não achou a configuração por curso, verifica por nível de ensino + tipo de atuação
                var specConfiguracaoProcessoPorNivelEnsino = new ConfiguracaoProcessoFilterSpecification()
                {
                    SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqProcesso = seqProcesso
                };
                seqConfiguracaoProcesso = ConfiguracaoProcessoDomainService.SearchProjectionByKey(specConfiguracaoProcessoPorNivelEnsino, p => p.Seq);
            }

            return seqConfiguracaoProcesso == 0;
        }
    }
}
