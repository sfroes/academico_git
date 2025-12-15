using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class ConvocacaoDomainService : AcademicoContextDomain<Convocacao>
    {
        #region [ DomainServices ]

        private ChamadaDomainService ChamadaDomainService
        {
            get { return Create<ChamadaDomainService>(); }
        }

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService
        {
            get { return Create<SolicitacaoServicoEnvioNotificacaoDomainService>(); }
        }

        private INotificacaoService NotificacaoService
        {
            get { return Create<INotificacaoService>(); }
        }

        private CurriculoDomainService CurriculoDomainService
        {
            get { return Create<CurriculoDomainService>(); }
        }

        private GrupoEscalonamentoDomainService GrupoEscalonamentoDomainService
        {
            get { return Create<GrupoEscalonamentoDomainService>(); }
        }

        private IngressanteDomainService IngressanteDomainService
        {
            get { return Create<IngressanteDomainService>(); }
        }

        private IngressanteHistoricoSituacaoDomainService IngressanteHistoricoSituacaoDomainService
        {
            get { return Create<IngressanteHistoricoSituacaoDomainService>(); }
        }

        private InstituicaoNivelTipoOrientacaoDomainService InstituicaoNivelTipoOrientacaoDomainService
        {
            get { return Create<InstituicaoNivelTipoOrientacaoDomainService>(); }
        }

        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService
        {
            get { return Create<InstituicaoNivelTipoVinculoAlunoDomainService>(); }
        }

        private InstituicaoTipoEntidadeFormacaoEspecificaDomainService InstituicaoTipoEntidadeFormacaoEspecificaDomainService
        {
            get { return Create<InstituicaoTipoEntidadeFormacaoEspecificaDomainService>(); }
        }

        private MatrizCurricularDomainService MatrizCurricularDomainService
        {
            get { return Create<MatrizCurricularDomainService>(); }
        }

        private OrientacaoPessoaAtuacaoDomainService OrientacaoPessoaAtuacaoDomainService
        {
            get { return Create<OrientacaoPessoaAtuacaoDomainService>(); }
        }

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService
        {
            get { return Create<SolicitacaoServicoDomainService>(); }
        }

        private ConvocadoDomainService ConvocadoDomainService => this.Create<ConvocadoDomainService>();

        #endregion [ DomainServices ]

        public SMCPagerData<Convocacao> ListarConvocacoes(ConvocacaoFilterSpecification filtro)
        {
            filtro.SetOrderBy(a => a.Descricao);
            var result = SearchBySpecification(filtro, out int total,
                a => a.Chamadas,
                a => a.CampanhaCicloLetivo.CicloLetivo).ToList();
            foreach (var convocacao in result)
            {
                foreach (var chamada in convocacao.Chamadas)
                {
                    var situacoesIngressantes = ChamadaDomainService.SearchProjectionByKey(new SMCSeqSpecification<Chamada>(chamada.Seq), p =>
                        p.Convocados.SelectMany(s =>
                            s.Ingressantes.Select(si => si.HistoricosSituacao
                                .OrderBy(o => o.Seq).FirstOrDefault().SituacaoIngressante
                        )).Distinct());
                    chamada.SituacoesIngressantesDaChamada = situacoesIngressantes?.ToArray() ?? new SituacaoIngressante[0];

                    if (!chamada.Processando.HasValue)
                    {
                        chamada.Processando = false;
                    }
                }
            }

            return new SMCPagerData<Convocacao>(result, total);
        }

        public bool VerificarExistenciaConvocados(long seqInstituicao, long seqChamada)
        {
            /* 1. Caso não exista nenhum ingressante com oferta convocada pela chamada em questão e com a situação atual “Aguardando liberação para matrícula”
             * (situação do ingressante), exibir a mensagem de confirmação:
             * “Não existem ingressantes para serem liberados para matrícula. Deseja encerrar a chamada?”
             *  1.1. Caso o usuário informe “Não”, abortar a operação.
             *  1.2. Caso o usuário informe “Sim”, mudar a situação da chamada em questão para “Chamada encerrada”.*/

            var seqsConvocados = ChamadaDomainService.SearchProjectionByKey(seqChamada, x => x.Convocados.Select(c => c.Seq)).ToArray();
            var specLiberados = new IngressanteFilterSpecification()
            {
                SeqsConvocados = seqsConvocados,
                SituacaoIngressanteAtual = SituacaoIngressante.AguardandoLiberacaMatricula
            };
            return IngressanteDomainService.Count(specLiberados) > 0;
        }

        /// <summary>
        /// Realiza as validações de acordo com RN_ALN_043 e libera os ingressantes convocados pela chamada para realizar a matrícula
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição (necessario para utilizar RawQuery)</param>
        /// <param name="seqChamada">Sequencial da chamada</param>
        /// <returns>Retorna um objeto com os impedimentos validados</returns>
        public ConvocacaoImpedimentosDeMatriculaVO VerificarImpedimentosExecutarMatriculaPorChamada(long seqInstituicao, long seqChamada)
        {
            // Cria o objeto de retorno
            var convocacao = new ConvocacaoImpedimentosDeMatriculaVO();

            var existemConvocados = VerificarExistenciaConvocados(seqInstituicao, seqChamada);
            if (!existemConvocados)
            {
                // Não existem convocados para liberação da matrícula
                //throw new NaoExistemConvocadosParaLiberacaoException();
                ChamadaDomainService.EncerrarChamada(seqChamada);

                // Informa que não teve impedimentos para não exibir a janelinha modal
                convocacao.SemImpedimento = true;
            }
            else
            {
                // Carrega os dados da chamada
                var chamada = ChamadaDomainService.SearchProjectionByKey(seqChamada, x => new
                {
                    SeqsConvocados = x.Convocados.Select(c => c.Seq).ToList(),
                    DescricaoProcessoSeletivo = x.Convocacao.ProcessoSeletivo.Descricao,
                    DescricaoCampanha = x.Convocacao.CampanhaCicloLetivo.Campanha.Descricao
                });

                // Verifica se todos os ingressantes convocados atendem ao critério de tipo orientação
                if (!ValidarConvocadosTipoOrientacao(chamada.SeqsConvocados.ToArray()))
                    throw new OrientacaoConvocadosException();

                // Campos de cabeçalho e controle
                convocacao.NumeroConvocados = chamada.SeqsConvocados.Count();
                convocacao.DescricaoCampanha = chamada.DescricaoCampanha;
                convocacao.DescricaoProcessoSeletivo = chamada.DescricaoProcessoSeletivo;
                convocacao.SemImpedimento = false;

                //Recupera todos os convocados
                var specIngressantesConvocados = new IngressanteFilterSpecification() { SeqsConvocados = chamada.SeqsConvocados.ToArray() };
                var grupoIntresantes = IngressanteDomainService.SearchProjectionBySpecification(specIngressantesConvocados, p => new
                {
                    p.DadosPessoais.Nome,
                    p.DadosPessoais.NomeSocial,
                    p.Seq,
                    GrupoEscalonamento = p.SolicitacoesServico.Select(s => new
                    {
                        s.GrupoEscalonamento.Descricao,
                        DataFim = s.GrupoEscalonamento.Itens.Min(si => si.Escalonamento.DataFim)
                    }).FirstOrDefault()
                }, true).ToList();

                /*
                    * RN_ALN_043 (olhar também RN_ALN_019)
                    * Passo 2.1
                    * Todos os ingressantes devem possuir um grupo de escalonamento válido, ou seja,
                    * todos os itens de escalonamento do grupo devem possuir uma data fim não expirada (maior ou igual a data atual).
                    * Caso tenha ingressante com um ticket cadastrado para algum item do grupo, considerar este ticket como a data fim do item em questão.
                */

                // Verifica se o grupo de escalonamento está válido
                foreach (var ingressante in grupoIntresantes)
                {
                    if (ingressante.GrupoEscalonamento.DataFim < DateTime.Now)
                    {
                        ConvocadoImpedimentoVO impedimento = convocacao.Impedimentos.FirstOrDefault(i => i.SeqIngressante == ingressante.Seq);

                        if (impedimento == null)
                        {
                            impedimento = new ConvocadoImpedimentoVO
                            {
                                SeqIngressante = ingressante.Seq,
                                NomeIngressante = (!string.IsNullOrEmpty(ingressante.NomeSocial) ? $"{ingressante.NomeSocial} ({ingressante.Nome})" : ingressante.Nome)
                            };
                            convocacao.Impedimentos.Add(impedimento);
                        }
                        impedimento.Impedimentos.Add(ImpedimentoLiberacaoMatricula.GrupoEscalonamentoComItemExpirado.SMCGetDescription());
                    }
                }

                /*
                    * RN_ALN_043 (olhar também RN_ALN_019)
                    * Passo 2.2.2
                    * Todos os ingressantes, cujo vínculo foi parametrizado por instituição nível para exigir oferta de matriz,
                    * devem ter formações específicas de seus cursos associadas, cujos tipos e quantidades foram parametrizados por instituição e tipo de entidade
                    * para serem obrigatórios na associação do ingressante.
                */

                //Recupera dos convocados todos que tem tipo de vinculo que exige curso
                var seqsConvocadosCurso = IngressanteDomainService.BuscarIngressantesQueExigeCursoPorConvocados(chamada.SeqsConvocados.ToArray(), seqInstituicao);

                // Caso exija curso, verifica a formação específica.
                if (seqsConvocadosCurso.Count > 0)
                {
                    //Recupera as formações específicas de acordo com a instituição e o tipo entidade externada programa
                    var tiposFormacaoEspecifica = InstituicaoTipoEntidadeFormacaoEspecificaDomainService.BuscarTiposObrigatorioComFilhos(seqInstituicao);

                    //Recuperar os ingressantes com os tipos de formações específicas
                    var specConvocados = new IngressanteFilterSpecification() { SeqsConvocados = seqsConvocadosCurso.Select(s => s.SeqConvocado).ToArray(), SeqChamada = seqChamada };
                    specConvocados.MaxResults = int.MaxValue;

                    var ingressantesTiposFormacao = IngressanteDomainService.SearchProjectionBySpecification(specConvocados, p =>
                        new IngressanteConvocadoVinculoVO()
                        {
                            Seq = p.Seq,
                            Nome = p.DadosPessoais.Nome,
                            NomeSocial = p.DadosPessoais.NomeSocial,
                            SeqsTipoFormacaoEspecifica = p.FormacoesEspecificas.Select(s => s.FormacaoEspecifica.SeqTipoFormacaoEspecifica).ToList()
                        }).ToList();

                    var ingressantesImpedido = ingressantesTiposFormacao.Where(w => !tiposFormacaoEspecifica.All(a => w.SeqsTipoFormacaoEspecifica.Any(n => a.SeqsFilhos.Contains(n))));
                    ingressantesImpedido.SMCForEach(s =>
                    {
                        ConvocadoImpedimentoVO impedimento = convocacao.Impedimentos.FirstOrDefault(i => i.SeqIngressante == s.Seq);
                        if (impedimento == null)
                        {
                            impedimento = new ConvocadoImpedimentoVO
                            {
                                SeqIngressante = s.Seq,
                                NomeIngressante = (!string.IsNullOrEmpty(s.NomeSocial) ? $"{s.NomeSocial} ({s.Nome})" : s.Nome)
                            };
                            convocacao.Impedimentos.Add(impedimento);
                        }
                        impedimento.Impedimentos.Add(ImpedimentoLiberacaoMatricula.FormacaoEspecificaNaoAssociada.SMCGetDescription());
                    });
                }

                //Liberar os convocados para matrícula
                if (convocacao.Impedimentos.Count == 0)
                {
                    // RN_ALN_043 2.2.5
                    IngressanteDomainService.ValidarLiberacaoMatriculaFinanceiro(chamada.SeqsConvocados.ToArray());

                    var specLiberados = new IngressanteFilterSpecification() { SeqsConvocados = chamada.SeqsConvocados.ToArray(), SituacaoIngressanteAtual = SituacaoIngressante.AguardandoLiberacaMatricula };
                    specLiberados.MaxResults = int.MaxValue;

                    var ingressantesLiberados = IngressanteDomainService.BuscarInformacoesNotificacoesIngressanteLiberado(specLiberados);
                    using (var unit = SMCUnitOfWork.Begin())
                    {
                        IngressanteHistoricoSituacaoDomainService.AtualizarSituacaoNaLiberacaoMatriculaIngressante(ingressantesLiberados.Select(p => p.SeqIngressante).ToList());
                        EnviarNotificacoesLiberacao(ingressantesLiberados);
                        ChamadaDomainService.EncerrarChamada(seqChamada);
                        unit.Commit();
                    }

                    convocacao.SemImpedimento = true;
                }
                else
                    convocacao.Impedimentos = convocacao.Impedimentos.OrderBy(c => c.NomeIngressante).ToList();
            }

            return convocacao;
        }

        /// <summary>
        /// Valida se o ingressante orientação caso o tipo de orientação seja obrigatório
        /// </summary>
        /// <param name="seqsConvocados">Lista com sequencial dos convocados</param>
        /// <returns>Verdadeiro se todos ingressantes atendem ao requisito</returns>
        public bool ValidarConvocadosTipoOrientacao(long[] seqsConvocados)
        {
            var specConvocados = new IngressanteFilterSpecification() { SeqsConvocados = seqsConvocados };
            specConvocados.MaxResults = int.MaxValue;

            return ValidarConvocadosTipoOrientacao(specConvocados);
        }

        /// <summary>
        /// Valida se o ingressante orientação caso o tipo de orientação seja obrigatório
        /// </summary>
        /// <param name="seqIngressante">Sequencial do ingressante a ser validado</param>
        /// <returns>Verdadeiro se todos ingressantes atendem ao requisito</returns>
        public bool ValidarConvocadosTipoOrientacao(long seqIngressante)
        {
            var specConvocados = new IngressanteFilterSpecification() { Seq = seqIngressante };

            return ValidarConvocadosTipoOrientacao(specConvocados);
        }

        /// <summary>
        /// Valida se o ingressante orientação caso o tipo de orientação seja obrigatório
        /// </summary>
        /// <param name="filtros">Filtro por convocados ou ingressante</param>
        /// <returns>Verdadeiro se todos ingressantes atendem ao requisito</returns>
        private bool ValidarConvocadosTipoOrientacao(IngressanteFilterSpecification filtros)
        {
            try
            {
                //Recuperar os ingressantes com os tipos de formações específicas
                //PessoaAtuacao -> TermosIntercambio
                var ingressantes = IngressanteDomainService.SearchProjectionBySpecification(filtros,
                    p => new { Seq = p.Seq, SeqsTipoIntercambio = p.TermosIntercambio.Select(s => s.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio) }).ToList();

                foreach (var item in ingressantes)
                {
                    //Recupera os dados de vínculo com os tipos de termos associados e o tipos de orientação
                    var dadosVinculo = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(item.Seq, true);
                    var tiposOrientacao = new List<long>();

                    if (item.SeqsTipoIntercambio.Count() > 0)
                    {
                        //Recuperar os tipos de orientação de mesmo tipo de intercambio e vinculo do aluno
                        tiposOrientacao = InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOrientacoesVinculo(dadosVinculo.Seq, item.SeqsTipoIntercambio.ToArray());
                    }
                    else
                    {
                        //Recuperar os tipos de orientação que não possuem tipo de intercambio
                        tiposOrientacao = InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOrientacoesVinculo(dadosVinculo.Seq, null);
                    }

                    //Verifica se a pessoa atuação possui orientação com os mesmos tipos
                    if (tiposOrientacao.Count > 0)
                    {
                        if (OrientacaoPessoaAtuacaoDomainService.ValidarOrientacoesPessoaAtuacaoConvocacao(item.Seq, tiposOrientacao.ToArray()))
                            continue;
                        else
                            return false;
                    }
                    else
                        continue;
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void EnviarNotificacoesLiberacao(List<NotificacaoConvocacaoVO> dados)
        {
            foreach (var item in dados)
            {
                Dictionary<string, string> dadosMerge = new Dictionary<string, string>();
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, !string.IsNullOrEmpty(item.NomeSocial) ? item.NomeSocial : item.Nome);
                dadosMerge.Add(TOKEN_TAG_NOTIFICACAO.DSC_OFERTA_CAMPANHA, string.Join(", ", item.DescricaoOfertaCampanha));

                // Envia a notificação
                var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                {
                    SeqSolicitacaoServico = item.SeqSolicitacaoServico,
                    TokenNotificacao = TOKEN_TIPO_NOTIFICACAO.LIBERACAO_MATRICULA,
                    DadosMerge = dadosMerge,
                    EnvioSolicitante = true,
                    ConfiguracaoPrimeiraEtapa = false
                };
                SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
            }
        }

        public long SalvarConvocacao(Convocacao convocacao)
        {
            if (convocacao.Chamadas != null && convocacao.Chamadas.Count > 0)
            {
                // Para cada chamada, verifica se está alterando o grupo de escalonamento 
                // Regra: Não permitir trocar o grupo de escalonamento se houver convocado para a chamada em questão.
                // Em caso de violação desta regra, abortar a operação e emitir a mensagem de erro:
                // "Alteração não permitira. Já existem convocados para a chamada <número da chamada>. Não é possível
                // alterar o grupo de escalonamento."
                foreach (var chamada in convocacao.Chamadas)
                {
                    // Se chamada já está salva em banco
                    if (chamada.Seq > 0)
                    {
                        // Busca a chamada para ver se está alterando o grupo de escalonamento
                        var specChamada = new SMCSeqSpecification<Chamada>(chamada.Seq);
                        var chamadaBanco = ChamadaDomainService.SearchByKey(specChamada);
                        if (chamadaBanco.SeqGrupoEscalonamento != chamada.SeqGrupoEscalonamento)
                        {
                            // Verifica se tem convocado para a chamada
                            var specConvocado = new ConvocadoFilterSpecification() { SeqChamada = chamada.Seq };
                            var qtdConvocado = ConvocadoDomainService.Count(specConvocado);
                            if (qtdConvocado > 0)
                                throw new AlteracaoGrupoEscalonamentoNaoPermitidaException(chamada.Numero.ToString());
                        }
                    }
                }

                // Validar modificações de grupo escalonamento do ingressante que tem a situação da chamada como liberação pra matrícula
                var seqsChamadas = convocacao.Chamadas.Where(w => w.SituacaoChamada == SituacaoChamada.AguardandoLiberacaoParaMatricula).Select(s => new { Seq = s.Seq, SeqGrupoEscalonamento = s.SeqGrupoEscalonamento });
                foreach (var ingressanteChamada in seqsChamadas)
                {
                    SolicitacaoServicoDomainService.AtualizarSolicitacaoServicoGrupoEscalonamento(ingressanteChamada.Seq, ingressanteChamada.SeqGrupoEscalonamento);
                }

                // Incrementando o número da chamada
                short max = convocacao.Chamadas.Max(a => a.Numero);
                foreach (var chamada in convocacao.Chamadas.Where(a => a.Numero == 0))
                {
                    max++;
                    chamada.Numero = max;
                }
            }

            convocacao.QuantidadeChamadasRegulares = (short)convocacao.Chamadas.Count;
            SaveEntity(convocacao);
            return convocacao.Seq;
        }
    }
}