using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
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

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class DocumentoRequeridoDomainService : AcademicoContextDomain<DocumentoRequerido>
    {
        #region Services

        private ITipoDocumentoService TipoDocumentoService
        {
            get { return this.Create<ITipoDocumentoService>(); }
        }

        #endregion

        #region DomainServices

        private ConfiguracaoEtapaDomainService ConfiguracaoEtapaDomainService
        {
            get { return this.Create<ConfiguracaoEtapaDomainService>(); }
        }

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService
        {
            get { return this.Create<SolicitacaoServicoDomainService>(); }
        }

        private GrupoDocumentoRequeridoItemDomainService GrupoDocumentoRequeridoItemDomainService
        {
            get { return this.Create<GrupoDocumentoRequeridoItemDomainService>(); }
        }

        private SolicitacaoDocumentoRequeridoDomainService SolicitacaoDocumentoRequeridoDomainService
        {
            get { return this.Create<SolicitacaoDocumentoRequeridoDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        private PessoaAtuacaoDocumentoDomainService PessoaAtuacaoDocumentoDomainService
        {
            get { return this.Create<PessoaAtuacaoDocumentoDomainService>(); }
        }

        private ProcessoEtapaDomainService ProcessoEtapaDomainService
        {
            get { return Create<ProcessoEtapaDomainService>(); }
        }

        private ConfiguracaoProcessoDomainService ConfiguracaoProcessoDomainService
        {
            get { return Create<ConfiguracaoProcessoDomainService>(); }
        }

        private ProcessoDomainService ProcessoDomainService
        {
            get { return Create<ProcessoDomainService>(); }
        }

        private ServicoDomainService ServicoDomainService
        {
            get { return Create<ServicoDomainService>(); }
        }

        #endregion

        public DocumentoRequeridoVO BuscarDocumentoRequerido(long seqDocumentoRequerido)
        {
            var documentoRequerido = this.SearchByKey(new SMCSeqSpecification<DocumentoRequerido>(seqDocumentoRequerido), x => x.ConfiguracaoEtapa.ProcessoEtapa);
            var situacaoEtapa = documentoRequerido.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa;

            var retorno = documentoRequerido.Transform<DocumentoRequeridoVO>();
            retorno.DescricaoConfiguracaoEtapa = documentoRequerido.ConfiguracaoEtapa.Descricao;
            retorno.SeqProcessoEtapa = documentoRequerido.ConfiguracaoEtapa.SeqProcessoEtapa;
            retorno.CamposReadyOnly = situacaoEtapa == SituacaoEtapa.Liberada || situacaoEtapa == SituacaoEtapa.Encerrada;

            return retorno;
        }

        public SMCPagerData<DocumentoRequeridoListarVO> BuscarDocumentosRequeridos(DocumentoRequeridoFiltroVO filtro)
        {
            /*A ORDENAÇÃO E PAGINAÇÃO DESTE MÉTODO FORAM FEITAS MANUALMENTE PARA ORDENAR
            PELO CAMPO TIPO DOCUMENTO, QUE SE ENCONTRA NO BANCO DADOS MESTRES E NÃO NO ACADÊMICO*/

            var spec = filtro.Transform<DocumentoRequeridoFilterSpecification>();

            //LIMPANDO A ORDENAÇÃO QUE SERÁ FEITA MANUALMENTE E SETANDO O MAXRESULTS PARA NÃO BUSCAR TODOS OS REGISTROS DA TABELA
            int qtdeRegistros = this.Count();
            spec.MaxResults = qtdeRegistros > 0 ? qtdeRegistros : int.MaxValue;
            spec.ClearOrderBy();

            var lista = this.SearchProjectionBySpecification(spec, a => new DocumentoRequeridoListarVO()
            {
                Seq = a.Seq,
                SeqConfiguracaoEtapa = a.SeqConfiguracaoEtapa,
                SeqProcessoEtapa = a.ConfiguracaoEtapa.SeqProcessoEtapa,
                SituacaoEtapa = a.ConfiguracaoEtapa.ProcessoEtapa.SituacaoEtapa,
                SeqTipoDocumento = a.SeqTipoDocumento,
                Obrigatorio = a.Obrigatorio,
                PermiteUploadArquivo = a.PermiteUploadArquivo,
                ObrigatorioUpload = a.ObrigatorioUpload,
                VersaoDocumento = a.VersaoDocumento
            });

            List<DocumentoRequeridoListarVO> listaVO = lista.ToList();
            listaVO.ForEach(a => a.DescricaoTipoDocumento = this.TipoDocumentoService.BuscarTipoDocumento(a.SeqTipoDocumento).Descricao);

            int total = listaVO.Count();

            //ORDENAÇÃO MANUAL EM TODA A LISTA, NÃO SOMENTE NA PÁGINA ATUAL
            List<SMCSortInfo> listaOrdenacao = filtro.PageSettings.SortInfo;

            foreach (var sort in listaOrdenacao)
            {
                if (sort.FieldName == nameof(DocumentoRequeridoListarVO.DescricaoTipoDocumento))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.DescricaoTipoDocumento).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.DescricaoTipoDocumento).ToList();
                }
                if (sort.FieldName == nameof(DocumentoRequeridoListarVO.Obrigatorio))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.Obrigatorio).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.Obrigatorio).ToList();
                }
                if (sort.FieldName == nameof(DocumentoRequeridoListarVO.PermiteUploadArquivo))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.PermiteUploadArquivo).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.PermiteUploadArquivo).ToList();
                }
                if (sort.FieldName == nameof(DocumentoRequeridoListarVO.ObrigatorioUpload))
                {
                    if (sort.Direction == SMCSortDirection.Ascending)
                        listaVO = listaVO.OrderBy(o => o.ObrigatorioUpload).ToList();
                    else
                        listaVO = listaVO.OrderByDescending(o => o.ObrigatorioUpload).ToList();
                }
            }

            //CONFIGURAÇÃO DE PAGINAÇÃO, RECUPERANDO OS REGISTROS DA PÁGINA ATUAL
            listaVO = listaVO.Skip((filtro.PageSettings.PageIndex - 1) * filtro.PageSettings.PageSize).Take(filtro.PageSettings.PageSize).ToList();

            return new SMCPagerData<DocumentoRequeridoListarVO>(listaVO, total);
        }

        public long Salvar(DocumentoRequeridoVO modelo)
        {
            ValidarModeloSalvar(modelo);

            var dominio = modelo.Transform<DocumentoRequerido>();

            //Somente criar solicitações de documento requerido caso seja uma nova entrada
            if (modelo.Seq == 0)
            {
                var listaSolicitacoesDocumento = new List<SolicitacaoDocumentoRequerido>();
                var configuracaoEtapa = this.ConfiguracaoEtapaDomainService.SearchByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(modelo.SeqConfiguracaoEtapa));

                var listaSolicitacoesEmAberto = this.SolicitacaoServicoDomainService.SearchBySpecification(new SolicitacaoServicoFilterSpecification()
                {
                    SeqConfiguracaoProcesso = configuracaoEtapa.SeqConfiguracaoProcesso,
                    CategoriasSituacoes = new List<CategoriaSituacao> { CategoriaSituacao.Novo, CategoriaSituacao.EmAndamento, CategoriaSituacao.Concluido }

                }).ToList();

                foreach (var solicitacaoServico in listaSolicitacoesEmAberto)
                {
                    SolicitacaoDocumentoRequerido solicitacaoDocumentoRequerido = new SolicitacaoDocumentoRequerido()
                    {
                        SeqSolicitacaoServico = solicitacaoServico.Seq,
                        SituacaoEntregaDocumento = SituacaoEntregaDocumento.AguardandoEntrega,
                        FormaEntregaDocumento = null,
                        VersaoDocumento = null,
                        DataEntrega = null,
                        Observacao = null,
                        SeqArquivoAnexado = null,
                        DataPrazoEntrega = null
                    };


                    if (solicitacaoServico.SeqConfiguracaoProcesso > 0)
                    {

                        var configuracaoProcessoSpec = new ConfiguracaoProcessoFilterSpecification { Seq = solicitacaoServico.SeqConfiguracaoProcesso };
                        var configuracaoProcesso = ConfiguracaoProcessoDomainService.SearchByKey(configuracaoProcessoSpec);

                        var processoSpec = new ProcessoFilterSpecification { Seq = configuracaoProcesso.SeqProcesso };

                        var processo = ProcessoDomainService.SearchByKey(processoSpec);

                        var servicoSpec = new ServicoFilterSpecification { Seq = processo.SeqServico };
                        var servico = ServicoDomainService.SearchByKey(servicoSpec);
                        var listaTokensPermitidos = new List<string>()
                        {
                            TOKEN_SERVICO.ENTREGA_DOCUMENTACAO,
                            TOKEN_SERVICO.SOLICITACAO_RENOVACAO_MATRICULA_STRICTO_SENSU,
                            TOKEN_SERVICO.MATRICULA_REABERTURA
                        };

                        if (listaTokensPermitidos.Contains(servico.Token))
                        {
                            var pessoaAtuacaoDocumentoSpec = new PessoaAtuacaoDocumentoFilterSpecification
                            {
                                SeqPessoaAtuacao = solicitacaoServico.SeqPessoaAtuacao,
                                SeqTipoDocumento = modelo.SeqTipoDocumento
                            };

                            var pessoaAtuacaoDocumento = PessoaAtuacaoDocumentoDomainService.SearchByKey(pessoaAtuacaoDocumentoSpec);

                            if (pessoaAtuacaoDocumento != null)
                            {
                                if (pessoaAtuacaoDocumento.SeqSolicitacaoDocumentoRequerido.HasValue)
                                {
                                    var specSolicitacaoDoc = new SolicitacaoDocumentoRequeridoFilterSpecification()
                                    {
                                        Seq = pessoaAtuacaoDocumento.SeqSolicitacaoDocumentoRequerido.Value
                                    };
                                    var solicitacaoDocumentoRequeridoExistente = SolicitacaoDocumentoRequeridoDomainService.SearchByKey(specSolicitacaoDoc);

                                    solicitacaoDocumentoRequerido.SeqDocumentoRequerido = solicitacaoDocumentoRequeridoExistente.SeqDocumentoRequerido;
                                    solicitacaoDocumentoRequerido.SituacaoEntregaDocumento = solicitacaoDocumentoRequeridoExistente.SituacaoEntregaDocumento;
                                    solicitacaoDocumentoRequerido.FormaEntregaDocumento = solicitacaoDocumentoRequeridoExistente.FormaEntregaDocumento;
                                    solicitacaoDocumentoRequerido.VersaoDocumento = solicitacaoDocumentoRequeridoExistente.VersaoDocumento;
                                    solicitacaoDocumentoRequerido.DataEntrega = solicitacaoDocumentoRequeridoExistente.DataEntrega;
                                    solicitacaoDocumentoRequerido.Observacao = solicitacaoDocumentoRequeridoExistente.Observacao;
                                    solicitacaoDocumentoRequerido.SeqArquivoAnexado = solicitacaoDocumentoRequeridoExistente.SeqArquivoAnexado;
                                    solicitacaoDocumentoRequerido.DataPrazoEntrega = solicitacaoDocumentoRequeridoExistente.DataPrazoEntrega;
                                    solicitacaoDocumentoRequerido.DescricaoInconformidade = solicitacaoDocumentoRequeridoExistente.DescricaoInconformidade;
                                    solicitacaoDocumentoRequerido.EntregaPosterior = solicitacaoDocumentoRequeridoExistente.EntregaPosterior;
                                    solicitacaoDocumentoRequerido.ObservacaoSecretaria = solicitacaoDocumentoRequeridoExistente.ObservacaoSecretaria;
                                    solicitacaoDocumentoRequerido.EntregueAnteriormente = true;
                                }
                                else
                                {
                                    solicitacaoDocumentoRequerido.SituacaoEntregaDocumento = SituacaoEntregaDocumento.Deferido;
                                    solicitacaoDocumentoRequerido.FormaEntregaDocumento = pessoaAtuacaoDocumento.SeqArquivoAnexado.HasValue ? FormaEntregaDocumento.Upload : FormaEntregaDocumento.Nenhum;
                                    solicitacaoDocumentoRequerido.VersaoDocumento = pessoaAtuacaoDocumento.SeqArquivoAnexado.HasValue ? VersaoDocumento.CopiaSimples : VersaoDocumento.Nenhum;
                                    solicitacaoDocumentoRequerido.DataEntrega = pessoaAtuacaoDocumento.DataEntrega;
                                    solicitacaoDocumentoRequerido.SeqArquivoAnexado = pessoaAtuacaoDocumento.SeqArquivoAnexado;
                                    solicitacaoDocumentoRequerido.ObservacaoSecretaria = pessoaAtuacaoDocumento.Observacao;
                                    solicitacaoDocumentoRequerido.EntregueAnteriormente = true;
                                }
                            }
                        }
                    }

                    var listaDocumentoVo = new List<DocumentoVO>();
                    var documentoRequerido = dominio.Transform<DocumentoVO>();

                    listaDocumentoVo.Add(documentoRequerido);

                    var situacaoGrupos = ValidarSituacoesGruposDocumentos(listaDocumentoVo);

                    var situacoesDocumentos = ValidarSituacoesGruposDocumentos(listaDocumentoVo);

                    solicitacaoServico.SituacaoDocumentacao = ConfirmarSituacaoDocumento(situacoesDocumentos, situacaoGrupos);

                    if (modelo.Sexo.HasValue)
                    {
                        var pessoaAtuacao = this.PessoaAtuacaoDomainService.SearchByKey(new SMCSeqSpecification<PessoaAtuacao>(solicitacaoServico.SeqPessoaAtuacao), IncludesPessoaAtuacao.DadosPessoais);

                        if (modelo.Sexo.Value == pessoaAtuacao.DadosPessoais?.Sexo)
                            listaSolicitacoesDocumento.Add(solicitacaoDocumentoRequerido);
                    }
                    else
                        listaSolicitacoesDocumento.Add(solicitacaoDocumentoRequerido);
                }

                dominio.SolicitacoesDocumentoRequerido = listaSolicitacoesDocumento;
            }

            this.SaveEntity(dominio);

            return dominio.Seq;
        }

        public void ValidarModeloSalvar(DocumentoRequeridoVO modelo)
        {
            var situacaoEtapa = this.ConfiguracaoEtapaDomainService.SearchProjectionByKey(new SMCSeqSpecification<ConfiguracaoEtapa>(modelo.SeqConfiguracaoEtapa), x => x.ProcessoEtapa.SituacaoEtapa);
            var documentoRequeridoPorTipoDocumento = this.SearchBySpecification(new DocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = modelo.SeqConfiguracaoEtapa, SeqTipoDocumento = modelo.SeqTipoDocumento }).ToList();

            if (situacaoEtapa == SituacaoEtapa.Liberada || situacaoEtapa == SituacaoEtapa.Encerrada)
                throw new ConfiguracaoEtapaOperacaoNaoPermitidaException();

            if (documentoRequeridoPorTipoDocumento.Any(a => a.Seq != modelo.Seq))
                throw new TipoDocumentoJaAssociadoEmOutraEtapaDocumentoException();

            if (modelo.Seq != 0)
            {
                var documentoRequeridoAntigo = this.SearchByKey(new SMCSeqSpecification<DocumentoRequerido>(modelo.Seq), x => x.ConfiguracaoEtapa);
                var solicitacaoServicoAssociadaConfiguracaoProcesso = this.SolicitacaoServicoDomainService.Count(new SolicitacaoServicoFilterSpecification() { SeqConfiguracaoProcesso = documentoRequeridoAntigo.ConfiguracaoEtapa.SeqConfiguracaoProcesso }) > 0;

                if (modelo.SeqTipoDocumento != documentoRequeridoAntigo.SeqTipoDocumento && solicitacaoServicoAssociadaConfiguracaoProcesso)
                    throw new ExistemSolicitacoesAssociadasTrocaTipoDocumentoException();

                var grupoDocumentoPorDocumento = this.GrupoDocumentoRequeridoItemDomainService.SearchBySpecification(new GrupoDocumentoRequeridoItemFilterSpecification() { SeqDocumentoRequerido = modelo.Seq }, x => x.GrupoDocumentoRequerido);

                if (grupoDocumentoPorDocumento.Any(a => a.GrupoDocumentoRequerido.UploadObrigatorio) && (modelo.Obrigatorio || !modelo.PermiteUploadArquivo || modelo.ObrigatorioUpload))
                    throw new DocumentoAssociadoGrupoUploadObrigatorioException();

                if (grupoDocumentoPorDocumento.Any(a => !a.GrupoDocumentoRequerido.UploadObrigatorio) && (modelo.Obrigatorio || modelo.ObrigatorioUpload))
                    throw new DocumentoAssociadoGrupoUploadNaoObrigatorioException();

                var tipoDocumento = TipoDocumentoService.BuscarTipoDocumento(modelo.SeqTipoDocumento);

                if (modelo.PermiteVarios && tipoDocumento != null && tipoDocumento.Unico)
                    throw new TipoDocumentoUnicoException();

                // Ao alterar o campo "Permite entrega posterior?" de "Sim" para "Não"
                if (modelo.PermiteEntregaPosterior == false && documentoRequeridoAntigo.PermiteEntregaPosterior == true)
                {
                    var spec = new SolicitacaoServicoFilterSpecification() { SeqConfiguracaoProcesso = documentoRequeridoAntigo.ConfiguracaoEtapa.SeqConfiguracaoProcesso };
                    var solicitacaoServicoAssociadaConfigProcesso = this.SolicitacaoServicoDomainService.SearchProjectionBySpecification(spec, x => new { x.DocumentosRequeridos }).ToList();

                    // Se existir alguma solicitação de serviço associada à configuração do processo com o documento requerido em questão marcado para ser entregue posteriormente
                    if (solicitacaoServicoAssociadaConfigProcesso.SMCAny())
                    {
                        var solicitacaoServico = solicitacaoServicoAssociadaConfigProcesso?.Where(c => c.DocumentosRequeridos.SMCAny(d => d.EntregaPosterior == true)).Count();

                        // Abortar a operação e exibir a seguinte mensagem impeditiva
                        if (solicitacaoServico != null && solicitacaoServico > 0)
                        {
                            // Operação não permitida. O campo “Permite entrega posterior?” não pode ser alterado, pois existe solicitação de serviço com este tipo de documento marcado para ser entregue posteriormente.
                            throw new DocumentoRequeridoCampoPermiteEntregaPosteriorNaoPodeSerAlteradoException();
                        }
                    }

                    // FIX: Carol - Foi solicitado para retirar o campo ExibeTermoResponsabilidadeEntrega do GrupoDocumentoRequerido
                    // Se o documento requerido em questão fizer parte de um grupo de documentos que está configurado para exibir o termo de responsabilidade de entrega
                    // abortar a operação e emitir mensagem impeditiva
                    // var grupoDocumentoExibeTermo = grupoDocumentoPorDocumento.Any(c => c.GrupoDocumentoRequerido.ExibeTermoResponsabilidadeEntrega = true);
                    // if (grupoDocumentoExibeTermo)
                    // throw new DocumentoRequeridoCampoExibeTermoResponsabilidadeException();
                }
            }
            else
            {
                // Ao incluir um novo registro, se o tipo de documento já tiver sido configurado em outra etapa 
                // do processo, abortar a operação e emitir a seguinte mensagem impeditiva: 
                // "Operação não permitida. O tipo de documento informado já foi associado à etapa <descrição da etapa do processo>."
                var spec = new SMCSeqSpecification<ProcessoEtapa>(modelo.SeqProcessoEtapa);
                var seqProcesso = this.ProcessoEtapaDomainService.SearchProjectionByKey(spec, x => x.SeqProcesso);
                var filtro = new ConfiguracaoEtapaFiltroVO()
                {
                    SeqProcesso = seqProcesso
                };
                var etapas = this.ConfiguracaoEtapaDomainService.BuscarConfiguracoesEtapa(filtro);
                var descricapConfiguracaoProcesso = etapas.SelectMany(c => c.ConfiguracoesEtapa).FirstOrDefault(d => d.SeqConfiguracaoEtapa == modelo.SeqConfiguracaoEtapa).DescricaoConfiguracaoProcesso;
                foreach (var etapa in etapas)
                {
                    if (etapa.SeqProcessoEtapa != modelo.SeqProcessoEtapa)
                    {
                        var configuracaoFiltrada = etapa.ConfiguracoesEtapa.FirstOrDefault(c => c.DescricaoConfiguracaoProcesso.Equals(descricapConfiguracaoProcesso));
                        var documentosEtapa = this.SearchBySpecification(new DocumentoRequeridoFilterSpecification() { SeqConfiguracaoEtapa = configuracaoFiltrada.SeqConfiguracaoEtapa, SeqTipoDocumento = modelo.SeqTipoDocumento }).ToList();
                        if (documentosEtapa.Any())
                        {
                            throw new TipoDocumentoRequeridoJaExistenteEmEtapaException(etapa.DescricaoEtapa);
                        }
                    }
                }
            }
        }

        public void Excluir(long seq)
        {
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                try
                {
                    ValidarModeloExcluir(seq);

                    var documentoRequerido = this.SearchByKey(new SMCSeqSpecification<DocumentoRequerido>(seq));
                    this.DeleteEntity(documentoRequerido);

                    unitOfWork.Commit();
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        private void ValidarModeloExcluir(long seq)
        {
            var documentoRequerido = this.SearchByKey(new SMCSeqSpecification<DocumentoRequerido>(seq), x => x.ConfiguracaoEtapa.ProcessoEtapa);
            var solicitacoesDocumentoRequerido = this.SolicitacaoDocumentoRequeridoDomainService.SearchBySpecification(new SolicitacaoDocumentoRequeridoFilterSpecification() { SeqDocumentoRequerido = seq }).ToList();

            if (solicitacoesDocumentoRequerido.Any())
                throw new ExclusaoDocumentoAssociadoSolicitacaoException();
        }

        public DocumentoRequeridoVO BuscarDescricaoDocumentoRequeridoPermiteVarios(long seqDocumentoRequerido)
        {
            var documentoRequerido = this.SearchProjectionByKey(new SMCSeqSpecification<DocumentoRequerido>(seqDocumentoRequerido), d => new DocumentoRequeridoVO
            {
                Seq = d.SeqTipoDocumento,
                PermiteVarios = d.PermiteVarios
            });

            documentoRequerido.DescricaoTipoDocumento = TipoDocumentoService.BuscarTipoDocumento(documentoRequerido.Seq).Descricao;

            return documentoRequerido;
        }


        public static List<(bool Pendente, SituacaoDocumentacao Situacao)> ValidarSituacoesGruposDocumentos(List<DocumentoVO> documentosRequeridos)
        {
            var situacaoGrupos = new List<(bool Pendente, SituacaoDocumentacao Situacao)>();
            var docsComGrupos = documentosRequeridos.Where(c => c.Grupos.SMCAny()).ToList();
            var grupos = docsComGrupos.SelectMany(c => c.Grupos).Distinct().ToList();

            foreach (var grupo in grupos)
            {
                var documentosGrupo = documentosRequeridos.Where(c => c.Grupos.SMCAny(d => d.Seq == grupo.Seq)).ToList();

                if (documentosGrupo.SMCAny())
                {
                    var situacoesValidadas = documentosGrupo.Where(c => c.Documentos.All(d =>
                                                                        d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                                        d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel)).ToList();

                    // 1 - Verificar se os grupos de documentos obrigatórios estão com o número mínimo de documentos em uma das situações: "Deferido" ou "Aguardando análise do setor responsável".
                    if (situacoesValidadas.Count >= grupo.NumeroMinimoDocumentosRequerido)
                    {
                        // 1.1 - Se estiverem, salvar a situação da documentação da solicitação com o valor "Entregue"
                        situacaoGrupos.Add((false, SituacaoDocumentacao.Entregue));
                    }
                    else
                    {
                        var docsDeferidoAnalisePendente = documentosGrupo.Where(c => c.Documentos.All(d =>
                                                                                     d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                                                     d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel ||
                                                                                     d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente)).ToList();

                        // 1.2 - Se não estiver, verificar se estão com as situações "Deferido", "Aguardando análise do setor responsável" e "Pendente"
                        if (docsDeferidoAnalisePendente.SMCAny())
                        {
                            // 1.2.1 - Se estiverem, salvar a situação da documentação da solicitação com valor "Entregue com pendência"
                            situacaoGrupos.Add((true, SituacaoDocumentacao.EntregueComPendencia));
                        }
                        else
                        {
                            // 1.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando Validação"
                            var situacoesAguardandoValidacao = documentosGrupo.Where(c => c.Documentos.All(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao)).ToList();

                            if (situacoesAguardandoValidacao.SMCAny())
                            {
                                // 1.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando validação"
                                situacaoGrupos.Add((false, SituacaoDocumentacao.AguardandoValidacao));
                            }
                            else
                            {
                                var situacoesAguardandoEntregaIndeferido = documentosGrupo.Where(c => c.Documentos.All(d => d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega ||
                                                                                                                            d.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido)).ToList();

                                // 1.2.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando entrega" ou "Indeferido";
                                if (situacoesAguardandoEntregaIndeferido.SMCAny())
                                {
                                    // 1.2.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando (nova) entrega".
                                    situacaoGrupos.Add((true, SituacaoDocumentacao.AguardandoEntrega));
                                }
                            }
                        }
                    }
                }
                else
                {
                    // 1.2.2.2.2 - Caso não exista lista de documentos requeridos para a solicitação, salvar a situação da documentação com o valor "Não requerida".
                    situacaoGrupos.Add((false, SituacaoDocumentacao.NaoRequerida));
                }
            }

            return situacaoGrupos;
        }

        /// <summary>
        /// Retorna a Situação do Documento baseado em documentos e Grupo de documentos que já estão validados separadamente.        
        /// </summary>
        public SituacaoDocumentacao ConfirmarSituacaoDocumento(List<(bool Pendente, SituacaoDocumentacao Situacao)> SituacaoDocumentos, List<(bool Pendente, SituacaoDocumentacao Situacao)> SituacaoGrupos)
        {
            /** Regra atualizada (48817):
             1 - Verificar se todos os documentos obrigatórios e se os grupos de documentos obrigatórios estão com o número mínimo de documentos em uma das situações: "Deferido" ou "Aguardando análise do setor responsável".
             1.1 - Se estiverem, salvar a situação da documentação da solicitação com o valor "Entregue";
             1.2 - Se não estiver, verificar se estão com as situações "Deferido", "Aguardando análise do setor responsável" e "Pendente"
             1.2.1 -Se estiverem, salvar a situação da documentação da solicitação com valor "Entregue com pendência"
             1.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando Validação"
             1.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando validação"
             1.2.2.2 - Se não estiverem, verificar se pelo menos um está com a situação "Aguardando entrega" ou "Indeferido";
             1.2.2.2.1 - Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando (nova) entrega".
             1.2.2.2.2 - Caso não exista lista de documentos requeridos para a solicitação, salvar a situação da documentação com o valor "Não requerida".
            **/

            // Se não existir uma lista de documentos e uma lista de grupos de documentos, então é uma solicitação não requerida
            bool existemDocumentosParaValidar = SituacaoDocumentos.Count > 0 || SituacaoGrupos.Count > 0;

            if (existemDocumentosParaValidar)
            {
                // Se Todos os documentos Obrigatórios e todos os grupos foram validados individualmente como Entregue
                if (SituacaoDocumentos.All(c => c.Situacao == SituacaoDocumentacao.Entregue) && SituacaoGrupos.All(c => c.Situacao == SituacaoDocumentacao.Entregue))
                {
                    return SituacaoDocumentacao.Entregue;
                }
                // Se Todos os documentos Obrigatórios e todos os grupos foram validados individualmente como Entregue com pendência, tirando os que já estão marcados como entregue
                else if (SituacaoDocumentos.Where(c => c.Situacao != SituacaoDocumentacao.Entregue).All(c => c.Situacao == SituacaoDocumentacao.EntregueComPendencia)
                          && SituacaoGrupos.Where(c => c.Situacao != SituacaoDocumentacao.Entregue).All(c => c.Situacao == SituacaoDocumentacao.EntregueComPendencia))
                {
                    return SituacaoDocumentacao.EntregueComPendencia;
                }
                // Se pelo menos um documento ou grupo de documentos estiver com a situação Aguardando validação
                else if (SituacaoDocumentos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoValidacao) || SituacaoGrupos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoValidacao))
                {
                    return SituacaoDocumentacao.AguardandoValidacao;
                }
                // Se pelo menos um documento ou grupo de documentos estiver marcado como aguardando entrega
                else if (SituacaoDocumentos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoEntrega) || SituacaoGrupos.Any(c => c.Situacao == SituacaoDocumentacao.AguardandoEntrega))
                {
                    return SituacaoDocumentacao.AguardandoEntrega;
                }
                else
                {
                    return SituacaoDocumentacao.NaoRequerida;
                }
            }
            else
            {
                return SituacaoDocumentacao.NaoRequerida;
            }
        }

        public List<(bool Pendente, SituacaoDocumentacao Situacao)> ValidarSituacoesDocumentosObrigatorios(IEnumerable<DocumentoVO> documentos)
        {
            // Recupera as situações dos documentos obrigatórios que não possuem grupos
            var situacoesObrigatoriosSemGrupo = SituacoesDocumentos(documentos);

            return situacoesObrigatoriosSemGrupo;
        }

        private List<(bool Pendente, SituacaoDocumentacao Situacao)> SituacoesDocumentos(IEnumerable<DocumentoVO> documentos)
        {
            List<(bool Pendente, SituacaoDocumentacao Situacao)> situacoesDocumentos = new List<(bool Pendente, SituacaoDocumentacao Situacao)>();

            documentos.ToList().ForEach(d =>
            {
                /* Alterar a regra do comando “Salvar” do UC_SRC_004_02_01 - Registrar Documentos:

                    * Verificar se todos os documentos obrigatórios estão em uma das situações: "Deferido" ou "Aguardando análise do setor responsável".
                        Se estiverem, salvar a situação da documentação da solicitação com o valor "Entregue";

                    * Se não estiver, verificar se estão com as situações "Deferido", "Aguardando análise do setor responsável" e "Pendente"
                        Se estiverem, salvar a situação da documentação da solicitação com valor "Entregue com pendência"

                    * Se não estiverem, verificar se pelo menos um está com a situação "Aguardando Validação"
                        Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando validação"

                    * Se não estiverem, verificar se pelo menos um está com a situação "Aguardando entrega" ou "Indeferido";
                        Se estiver, salvar a situação da documentação da solicitação com valor "Aguardando (nova) entrega".

                    * Caso não exista lista de documentos requeridos para a solicitação, salvar a situação da documentação com o valor "Não requerida".
                */

                var pendente = false;
                var situacao = SituacaoDocumentacao.Nenhum;

                if (d.Documentos?.All(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                            dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel) ?? false)
                {
                    pendente = false;
                    situacao = SituacaoDocumentacao.Entregue;
                }
                else if (d.Documentos?.All(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Deferido ||
                                                 dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoAnaliseSetorResponsavel ||
                                                 dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Pendente) ?? false)
                {
                    pendente = false;
                    situacao = SituacaoDocumentacao.EntregueComPendencia;
                }
                else if (d.Documentos?.Any(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoValidacao) ?? false)
                {
                    pendente = true;
                    situacao = SituacaoDocumentacao.AguardandoValidacao;
                }
                else if (d.Documentos?.Any(dd => dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.AguardandoEntrega ||
                                                 dd.SituacaoEntregaDocumento == SituacaoEntregaDocumento.Indeferido) ?? false)
                {
                    pendente = true;
                    situacao = SituacaoDocumentacao.AguardandoEntrega;
                }
                else
                {
                    pendente = false;
                    situacao = SituacaoDocumentacao.NaoRequerida;
                }

                situacoesDocumentos.Add((pendente, situacao));
            });

            return situacoesDocumentos;
        }

    }
}