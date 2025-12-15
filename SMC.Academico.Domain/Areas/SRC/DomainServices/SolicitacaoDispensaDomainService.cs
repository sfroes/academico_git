using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.Specifications;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Helpers;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoDispensaDomainService : AcademicoContextDomain<SolicitacaoDispensa>
    {
        #region [ DomainService ]

        private AlunoFormacaoDomainService AlunoFormacaoDomainService => Create<AlunoFormacaoDomainService>();
        private SolicitacaoDispensaGrupoDomainService SolicitacaoDispensaGrupoDomainService => Create<SolicitacaoDispensaGrupoDomainService>();

        private MatrizCurricularDomainService MatrizCurricularDomainService => this.Create<MatrizCurricularDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => this.Create<PessoaAtuacaoDomainService>();

        private RegimeLetivoDomainService RegimeLetivoDomainService => this.Create<RegimeLetivoDomainService>();

        private CicloLetivoDomainService CicloLetivoDomainService => this.Create<CicloLetivoDomainService>();

        private DispensaDomainService DispensaDomainService => this.Create<DispensaDomainService>();

        private InstituicaoNivelTipoComponenteCurricularDomainService InstituicaoNivelTipoComponenteCurricularDomainService => this.Create<InstituicaoNivelTipoComponenteCurricularDomainService>();

        private AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => this.Create<AlunoHistoricoDomainService>();

        private ComponenteCurricularDomainService ComponenteCurricularDomainService => this.Create<ComponenteCurricularDomainService>();

        private CurriculoCursoOfertaDomainService CurriculoCursoOfertaDomainService => this.Create<CurriculoCursoOfertaDomainService>();

        private CurriculoCursoOfertaGrupoDomainService CurriculoCursoOfertaGrupoDomainService => this.Create<CurriculoCursoOfertaGrupoDomainService>();

        private GrupoCurricularComponenteDomainService GrupoCurricularComponenteDomainService => this.Create<GrupoCurricularComponenteDomainService>();

        private GrupoCurricularDomainService GrupoCurricularDomainService => this.Create<GrupoCurricularDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => this.Create<HistoricoEscolarDomainService>();

        private InstituicaoNivelDomainService InstituicaoNivelDomainService => this.Create<InstituicaoNivelDomainService>();

        private SolicitacaoServicoDomainService SolicitacaoServicoDomainService => this.Create<SolicitacaoServicoDomainService>();

        private TitulacaoDomainService TitulacaoDomainService => this.Create<TitulacaoDomainService>();

        private PlanoEstudoDomainService PlanoEstudoDomainService => this.Create<PlanoEstudoDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => this.Create<PlanoEstudoItemDomainService>();

        private SolicitacaoDispensaDestinoDomainService SolicitacaoDispensaDestinoDomainService => this.Create<SolicitacaoDispensaDestinoDomainService>();

        private SolicitacaoDispensaGrupoDestinoDomainService SolicitacaoDispensaGrupoDestinoDomainService => this.Create<SolicitacaoDispensaGrupoDestinoDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => this.Create<DivisaoTurmaDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => this.Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        #endregion [ DomainService ]

        #region [ Queries ]

        private string _inserirSolicitacaoDispensaPorSolicitacaoServico =
                        @" INSERT INTO SRC.solicitacao_dispensa (seq_solicitacao_servico) VALUES ({0})";

        #endregion [ Queries ]

        public (TotalHoraCreditoVO Cursado, TotalHoraCreditoVO Dispensa) CalcularTotaisDispensa(long seqSolicitacaoDispensa)
        {
            var cursado = new TotalHoraCreditoVO();
            var dispensa = new TotalHoraCreditoVO();

            var solicitacaoDispensa = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoDispensa), s => new DadosCalculoTotalDispensaVO
            {
                SeqsComponentesCurricularesDispensa = s.Destinos.Where(d => d.SeqComponenteCurricular.HasValue).Select(d => d.SeqComponenteCurricular.Value).ToList(),
                DadosGruposCurricularesDispensa = s.Destinos.Where(d => d.SeqCurriculoCursoOfertaGrupo.HasValue).Select(d => new DadosCalculoCurriculoCursoOfertaGrupo
                {
                    SeqCurriculoCursoOfertaGrupo = d.SeqCurriculoCursoOfertaGrupo.Value,
                    QuantidadeDispensaGrupo = d.QuantidadeDispensaGrupo
                }).ToList(),
                SeqsComponentesCurricularesOrigemInterna = s.OrigensInternas.Select(o => o.HistoricoEscolar.SeqComponenteCurricular.Value).ToList(),
                OrigensExternas = s.OrigensExternas.Select(e => new DadosCalculoTotalDispensaOrigemExternaVO
                {
                    FormatoCargaHoraria = e.FormatoCargaHoraria,
                    Credito = e.Credito,
                    CargaHoraria = e.CargaHoraria
                }).ToList(),
                SeqInstituicaoEnsino = s.AlunoHistorico.Aluno.Pessoa.SeqInstituicaoEnsino
            });

            return CalcularTotaisDispensa(solicitacaoDispensa);
        }

        /// <summary>
        /// Retorna o total de horas e créditos que foram selecionados tanto para "Cursados" quanto para "Dispensa" na solicitação de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoDispensa">Sequencial da solicitação de dispensa</param>
        /// <returns>Horas e créditos cursados e para dispensa</returns>
        public (TotalHoraCreditoVO Cursado, TotalHoraCreditoVO Dispensa) CalcularTotaisDispensa(DadosCalculoTotalDispensaVO solicitacaoDispensa)
        {
            var cursado = new TotalHoraCreditoVO();
            var dispensa = new TotalHoraCreditoVO();

            // Quantidade de carga horária para cálculo caso não encontre no componente curricular específico
            // Quando não encontrar componente curricular com parametrização, deve usar a parametrização do tipo Disciplina (Regra da Janice)
            var quantidadeHorasPorCreditoDisciplina = InstituicaoNivelTipoComponenteCurricularDomainService.BuscarQuantidadeHorasPorCreditoDisciplina(solicitacaoDispensa.SeqInstituicaoEnsino);

            // Calcula o total atual de carga horária e créditos cursados
            var totaisComponentesCursados = this.ComponenteCurricularDomainService.CalculaHoraCreditoComponenteCurricular(solicitacaoDispensa.SeqInstituicaoEnsino, solicitacaoDispensa.SeqsComponentesCurricularesOrigemInterna);

            cursado.TotalHoras = totaisComponentesCursados.TotalHoras;
            cursado.TotalHorasAula = totaisComponentesCursados.TotalHorasAula;
            cursado.TotalCreditos = totaisComponentesCursados.TotalCreditos;

            solicitacaoDispensa.OrigensExternas.ForEach(x =>
            {
                if (x.CargaHoraria.HasValue)
                {
                    switch (x.FormatoCargaHoraria)
                    {
                        case Common.Areas.CUR.Enums.FormatoCargaHoraria.HoraAula:
                            cursado.TotalHorasAula += x.CargaHoraria.Value;
                            cursado.TotalHoras += Convert.ToDecimal((x.CargaHoraria.Value * 50F) / 60F);
                            break;

                        case Common.Areas.CUR.Enums.FormatoCargaHoraria.Hora:
                            cursado.TotalHorasAula += Convert.ToDecimal((x.CargaHoraria.Value * 60F) / 50F);
                            cursado.TotalHoras += x.CargaHoraria.Value;
                            break;
                    }
                    //cursado.TotalHoras += x.CargaHoraria.Value;
                }

                if (x.Credito.HasValue)
                    cursado.TotalCreditos += x.Credito.Value;

                if (quantidadeHorasPorCreditoDisciplina.HasValue && quantidadeHorasPorCreditoDisciplina.Value > 0)
                {
                    cursado.TotalCreditosPorHora += (x.CargaHoraria.GetValueOrDefault() / quantidadeHorasPorCreditoDisciplina.Value);
                    cursado.TotalHorasPorCredito += (cursado.TotalCreditos * quantidadeHorasPorCreditoDisciplina.Value);
                }
            });

            // Calcula o total atual de carga horaria e créditos da dispensa
            var totaisComponentes = this.ComponenteCurricularDomainService.CalculaHoraCreditoComponenteCurricular(solicitacaoDispensa.SeqInstituicaoEnsino, solicitacaoDispensa.SeqsComponentesCurricularesDispensa);
            var totaisCurriculo = this.CurriculoCursoOfertaGrupoDomainService.CalculaHoraCreditoCurriculoCursoOfertaGrupo(solicitacaoDispensa.SeqInstituicaoEnsino, solicitacaoDispensa.DadosGruposCurricularesDispensa);

            dispensa.TotalHoras = totaisComponentes.TotalHoras + totaisCurriculo.TotalHoras;
            dispensa.TotalHorasAula = totaisComponentes.TotalHorasAula + totaisCurriculo.TotalHorasAula;
            dispensa.TotalCreditos = totaisComponentes.TotalCreditos + totaisCurriculo.TotalCreditos;
            dispensa.TotalCreditosPorHora = totaisComponentes.TotalCreditosPorHora + totaisCurriculo.TotalCreditosPorHora;
            dispensa.TotalHorasPorCredito = totaisComponentes.TotalHorasPorCredito + totaisCurriculo.TotalHorasPorCredito;

            return (cursado, dispensa);
        }

        /// <summary>
        /// Verifica se existe solicitação de dispensa para solicitação de serviço, se não existir cria um solicitação de dispensa apenas com o sequencial da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void CriarSolicitacaoDispensaPorSolicitacaoServico(long seqSolicitacaoServico)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico));

            if (registro == null)
                ExecuteSqlCommand(string.Format(_inserirSolicitacaoDispensaPorSolicitacaoServico, seqSolicitacaoServico));
        }

        /// <summary>
        /// Faz as validações para prosseguir com a criação de grupos para o atendimento de dispensa individual
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void SalvarAtendimentoDispensaAgrupamento(long seqSolicitacaoServico)
        {
            // Recupera os dados da solicitação
            var dadosSolicitacao = this.SearchProjectionByKey(new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico), s => new
            {
                SeqsDestinos = s.Destinos.Select(d => d.Seq),
                SeqsOrigensInternas = s.OrigensInternas.Select(oi => oi.Seq),
                SeqsOrigensExternas = s.OrigensExternas.Select(oe => oe.Seq),
                Grupos = s.Grupos.Select(g => new
                {
                    SeqsDestinos = g.Destinos.Select(d => d.SeqSolicitacaoDispensaDestino),
                    SeqsOrigensInternas = g.OrigensInternas.Select(oi => oi.SeqSolicitacaoDispensaOrigemInterna),
                    SeqsOrigensExternas = g.OrigensExternas.Select(oe => oe.SeqSolicitacaoDispensaOrigemExterna)
                })
            });

            /* Ao prosseguir, consistir:
             * - Se não tiver grupos deixa passar, pois  a solicitação pode ser indeferida.
             */
            if (dadosSolicitacao?.Grupos?.Any() ?? false)
            {
                /*
                 * - Se existir pelo menos 1 agrupamento salvo, consistir a mesma regra que está no botão de confirmação do deferimento
                 *   da solicitação:
                 *   - 1. Verificar se todos os itens da solicitação de dispensa cadastrados em UC_SRC_004_12_01 - Solicitação - Itens
                 *        Cursados para Dispensa (solicitacao_dispensa_origem_interna e solicitacao_dispensa_origem_externa) estão em
                 *        algum agrupamento (solicitação_dispensa_grupo).
                 *        Em caso de violação, abortar operação e exibir a mensagem de erro:
                 *        “Operação não permitida Existem um ou mais itens cursados que não foram agrupados”.
                 *   - 2. Verificar se todos os itens da solicitação de dispensa cadastrados em UC_SRC_004_11_01 - Solicitação - Itens
                 *        a Serem Dispensados (solicitacao_dispensa_destino) estão em algum agrupamento (solicitação_dispensa_grupo).
                 *        Em caso de violação, abortar operação e exibir a mensagem de erro:
                 *        “Operação não permitida. Existem um ou mais itens a serem dispensados que não foram agrupados”
                 */

                // Armazena todas as origens e destinos dos grupos informados
                var seqsOrigens = new List<long>();
                var seqsDestinos = new List<long>();
                foreach (var grupo in dadosSolicitacao.Grupos)
                {
                    seqsDestinos.AddRange(grupo.SeqsDestinos);
                    seqsOrigens.AddRange(grupo.SeqsOrigensInternas);
                    seqsOrigens.AddRange(grupo.SeqsOrigensExternas);
                }

                // Compara se todos os itens estão em grupos
                if (dadosSolicitacao.SeqsOrigensInternas.Any(oi => !seqsOrigens.Contains(oi)) || dadosSolicitacao.SeqsOrigensExternas.Any(oe => !seqsOrigens.Contains(oe)))
                    throw new SMCApplicationException("Operação não permitida. Um ou mais itens cursados não estão presentes em nenhum grupo.");

                if (dadosSolicitacao.SeqsDestinos.Any(d => !seqsDestinos.Contains(d)))
                    throw new SMCApplicationException("Operação não permitida. Um ou mais itens a serem dispensados não estão presentes em nenhum grupo.");
            }
        }

        /// <summary>
        /// Busca os dados da solicitação de dispensa de acordo com a solicitação de serviço e a pessoa atuação
        /// Retorna inclusive a lista de ciclos letivos disponivel para seleção do aluno
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação (aluno que solicitou o serviço)</param>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Modelo para realizar o passo de escolha dos itens a serem dispensados</returns>
        public SolicitacaoDispensaItensDispensadosVO PrepararModeloSolicitacaoDispensaItensDispensados(long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            var specSolicitacaoDispensa = new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico);

            var solicitacaoDispensa = this.SearchProjectionByKey(specSolicitacaoDispensa, s => new
            {
                SeqCicloLetivoSolicitacao = (long?)s.Destinos.FirstOrDefault().SeqCicloLetivo,
                ComponentesCurriculares = s.Destinos.Where(d => d.SeqComponenteCurricular.HasValue).Select(d => new
                {
                    SeqGrupoCurricularComponente = d.SeqGrupoCurricularComponente,
                    SeqComponenteCurricular = d.SeqGrupoCurricularComponente.HasValue ? null : d.SeqComponenteCurricular,
                    SeqComponenteCurricularAssunto = d.SeqComponenteCurricularAssunto
                }).ToList(),
                GruposCurriculares = s.Destinos.Where(d => d.SeqCurriculoCursoOfertaGrupo.HasValue).Select(d => new
                {
                    d.Seq,
                    d.SeqCurriculoCursoOfertaGrupo,
                    d.QuantidadeDispensaGrupo
                }).ToList(),
            });

            // Busca os ciclos letivos para o select
            // Listar para seleção os ciclos letivos do aluno-histórico em que o aluno teve em algum
            // momento situação de matriculado, matriculado em mobilidade ou provável formando sem data
            // de exclusão
            List<SMCDatasourceItem> ciclosLetivosSelect = this.AlunoDomainService.BuscarCiclosLetivosAlunoHistoricoSituacaoSelect(seqPessoaAtuacao);

            // Caso ainda não tenha cadastrado nenhum componente para dispensa recuperar
            // o ciclo letivo mais atual do aluno entre os que são listados no select
            long seqCicloLetivo;
            if (solicitacaoDispensa.SeqCicloLetivoSolicitacao.HasValue)
                seqCicloLetivo = solicitacaoDispensa.SeqCicloLetivoSolicitacao.Value;
            else
                seqCicloLetivo = ciclosLetivosSelect.Last().Seq;

            var seqsCurriculosCursosOfertasGrupos = solicitacaoDispensa.GruposCurriculares.Select(g => g.SeqCurriculoCursoOfertaGrupo.Value).ToList();

            var specCurriculoCursoOfertaGrupo = new SMCContainsSpecification<CurriculoCursoOfertaGrupo, long>(g => g.Seq, seqsCurriculosCursosOfertasGrupos.ToArray());

            var curriculosCursosOfertasGrupos = this.CurriculoCursoOfertaGrupoDomainService.SearchBySpecification(specCurriculoCursoOfertaGrupo).ToList();

            var modelo = new SolicitacaoDispensaItensDispensadosVO()
            {
                BloquearCicloLetivo = SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ALUNO,
                CiclosLetivos = ciclosLetivosSelect,
                SeqCicloLetivo = seqCicloLetivo,
                SeqPessoaAtuacao = seqPessoaAtuacao,
                SeqSolicitacaoServico = seqSolicitacaoServico,
                ComponentesCurriculares = solicitacaoDispensa.ComponentesCurriculares.Select(c => new SolicitacaoDispensaComponenteCurricularVO()
                {
                    SeqComponenteCurricular = c.SeqComponenteCurricular,
                    SeqGrupoCurricularComponente = c.SeqGrupoCurricularComponente,
                    SeqComponenteCurricularAssunto = c.SeqComponenteCurricularAssunto
                }).ToList(),
                GruposCurriculares = solicitacaoDispensa.GruposCurriculares.Select(g => new SolicitacaoDispensaGrupoCurricularVO()
                {
                    Seq = g.Seq,
                    BloquearTotalDispensado = this.GrupoCurricularDomainService.ValidarFormatoConfiguracaoGrupoCurricular(curriculosCursosOfertasGrupos.FirstOrDefault(c => c.Seq == g.SeqCurriculoCursoOfertaGrupo).SeqGrupoCurricular),
                    QuantidadeDispensaGrupo = g.QuantidadeDispensaGrupo,
                    SeqGrupoCurricular = curriculosCursosOfertasGrupos.FirstOrDefault(c => c.Seq == g.SeqCurriculoCursoOfertaGrupo).SeqGrupoCurricular
                }).ToList(),
            };

            /*
            var matriz = MatrizCurricularDomainService.BuscarMatrizCurricularAluno(seqPessoaAtuacao, seqCicloLetivo, true);
            if (matriz != null)
                modelo.SeqMatrizCurricular = matriz.Seq;*/

            // Recupera a matriz atual do aluno
            var dadosMatriz = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(seqPessoaAtuacao);
            modelo.SeqMatrizCurricular = dadosMatriz.SeqMatrizCurricular.GetValueOrDefault();

            if (dadosMatriz.SeqCurriculoCursoOferta.HasValue)
            {
                modelo.SeqCurriculoCursoOferta = dadosMatriz.SeqCurriculoCursoOferta.Value;
                modelo.ExibirGrupoComponente = true;
            }
            else
                modelo.ExibirGrupoComponente = false;

            modelo.SeqInstituicaoNivelResponsavel = this.AlunoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorAluno(seqPessoaAtuacao).SeqNivelEnsino;

            // Totais de horas cursadas e para dispensa
            var horasCursadasDispensa = CalcularTotaisDispensa(seqSolicitacaoServico);
            modelo.CursadosTotalCargaHorariaHoras = horasCursadasDispensa.Cursado.TotalHoras;
            modelo.CursadosTotalCargaHorariaHorasAula = horasCursadasDispensa.Cursado.TotalHorasAula;
            modelo.CursadosTotalCreditos = horasCursadasDispensa.Cursado.TotalCreditos;
            modelo.DispensaTotalCargaHorariaHoras = horasCursadasDispensa.Dispensa.TotalHoras;
            modelo.DispensaTotalCargaHorariaHorasAula = horasCursadasDispensa.Dispensa.TotalHorasAula;
            modelo.DispensaTotalCreditos = horasCursadasDispensa.Dispensa.TotalCreditos;

            if (modelo.ExibirGrupoComponente && modelo.ComponentesCurriculares != null)
            {
                // Caso tenha algum componente que esteja fora da matriz do aluno, remove ele da lista e retorna na lista auxiliar de erro para serem exibidos na tela.
                modelo.ComponentesCurricularesForaMatriz = modelo.ComponentesCurriculares.Where(c => !c.SeqGrupoCurricularComponente.HasValue).Select(x => new SMCDatasourceItem { Seq = x.SeqComponenteCurricular.GetValueOrDefault() }).ToList();
                if (modelo.ComponentesCurricularesForaMatriz.SMCAny())
                {
                    modelo.ComponentesCurriculares.RemoveAll(x => modelo.ComponentesCurricularesForaMatriz.Any(a => a.Seq == x.SeqComponenteCurricular));

                    // Percorre os itens e busca a descrição
                    foreach (var item in modelo.ComponentesCurricularesForaMatriz)
                        item.Descricao = ComponenteCurricularDomainService.BuscarComponenteCurricularDescricaoCompleta(item.Seq);
                }
            }

            return modelo;
        }

        /// <summary>
        /// Grava os dados da solicitação de dispensa destino para a solicitação de dispensa informada
        /// </summary>
        /// <param name="modelo">Modelo da solicitação de dispensa com a solicitação de dispensa destino preenchida</param>
        public void SalvarSolicitacaoDispensaItensDispensados(SolicitacaoDispensaItensDispensadosVO modelo)
        {
            using (var tran = SMCUnitOfWork.Begin())
            {
                // Recupera os itens formados ou dispensados
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(modelo.SeqPessoaAtuacao, true);
                var dadosHistoricoEscolar = HistoricoEscolarDomainService.ComponentesCriteriosHistoricoEscolar(dadosOrigem.SeqAlunoHistoricoAtual, false);

                var specSolicitacaoDispensa = new SMCSeqSpecification<SolicitacaoDispensa>(modelo.SeqSolicitacaoServico);

                var solicitacaoDispensa = this.SearchByKey(specSolicitacaoDispensa, IncludesSolicitacaoDispensa.Destinos);

                var destinos = new List<SolicitacaoDispensaDestino>();

                if (modelo.ComponentesCurriculares.Any())
                {
                    List<string> jaAprovados = new List<string>();
                    foreach (var item in modelo.ComponentesCurriculares)
                    {
                        if (item.SeqGrupoCurricularComponente.HasValue)
                            item.SeqComponenteCurricular = this.GrupoCurricularComponenteDomainService.SearchProjectionByKey(new SMCSeqSpecification<GrupoCurricularComponente>(item.SeqGrupoCurricularComponente.Value), p => p.SeqComponenteCurricular);

                        var itemAprovado = dadosHistoricoEscolar.FirstOrDefault(d => d.SeqComponente == item.SeqComponenteCurricular && (!d.SeqComponenteCurricularAssunto.HasValue || d.SeqComponenteCurricularAssunto == item.SeqComponenteCurricularAssunto));
                        if (itemAprovado != null)
                        {
                            if (itemAprovado.SeqComponenteCurricularAssunto.HasValue)
                                jaAprovados.Add($"{itemAprovado.DescricaoComponente} / {itemAprovado.DescricaoComponenteAssunto}");
                            else
                                jaAprovados.Add(itemAprovado.DescricaoComponente);
                        }

                        destinos.Add(new SolicitacaoDispensaDestino()
                        {
                            SeqSolicitacaoDispensa = modelo.SeqSolicitacaoServico,
                            SeqCicloLetivo = modelo.SeqCicloLetivo,
                            SeqComponenteCurricular = item.SeqComponenteCurricular,
                            SeqComponenteCurricularAssunto = item.SeqComponenteCurricularAssunto,
                            SeqGrupoCurricularComponente = item.SeqGrupoCurricularComponente,
                            Seq = solicitacaoDispensa?.Destinos?.FirstOrDefault(d => d.SeqComponenteCurricular == item.SeqComponenteCurricular &&
                                                                                   d.SeqComponenteCurricularAssunto == item.SeqComponenteCurricularAssunto &&
                                                                                   d.SeqGrupoCurricularComponente == item.SeqGrupoCurricularComponente)?.Seq ?? 0
                        });
                    }

                    if (jaAprovados != null && jaAprovados.Any())
                        throw new SolicitacaoDispensaComponenteCurricularAprovadoException(string.Join(", ", jaAprovados));
                }

                var destinoBancoComponente = solicitacaoDispensa?.Destinos?.Where(w => w.SeqGrupoCurricularComponente != null).Select(s => s.SeqGrupoCurricularComponente).ToList();
                var destinoComponente = modelo?.ComponentesCurriculares?.Select(s => s.SeqGrupoCurricularComponente).ToList();
                var diferencaComponente = destinoBancoComponente.Except(destinoComponente).ToList();

                if (diferencaComponente.Any())
                {
                    modelo.ComponentesCurricularesExcluidos = solicitacaoDispensa.Destinos.Where(w => diferencaComponente.Contains(w.SeqGrupoCurricularComponente)).Select(s => s.Seq).ToList();
                    foreach (var item in modelo.ComponentesCurricularesExcluidos)
                    {
                        if (SolicitacaoDispensaGrupoDomainService.VerificarAgrupamento(modelo.SeqSolicitacaoServico, item))
                            this.SolicitacaoDispensaGrupoDestinoDomainService.Excluir(item);
                    }
                }

                if (modelo.GruposCurriculares.Any())
                {
                    var seqsGruposCurricularesSelecionados = modelo.GruposCurriculares.Select(g => g.SeqGrupoCurricular.Value);

                    var specCurriculoCursoOfertaGrupo = new SMCContainsSpecification<CurriculoCursoOfertaGrupo, long>(g => g.SeqGrupoCurricular, seqsGruposCurricularesSelecionados.ToArray());

                    var curriculosCursosOfertasGrupos = this.CurriculoCursoOfertaGrupoDomainService.SearchBySpecification(specCurriculoCursoOfertaGrupo).ToList();

                    /*var specAluno = new SMCSeqSpecification<Aluno>(modelo.SeqPessoaAtuacao);

                    var seqCurriculoCursoOferta = this.AlunoDomainService.SearchProjectionByKey(specAluno, a =>
                                                         a.Historicos.FirstOrDefault(h => h.Atual)
                                                          .HistoricosCicloLetivo.Where(h => h.SeqCicloLetivo == modelo.SeqCicloLetivo).FirstOrDefault(c => !c.DataExclusao.HasValue)
                                                          .PlanosEstudo.Where(p => p.Atual).FirstOrDefault()
                                                          .MatrizCurricularOferta.MatrizCurricular.SeqCurriculoCursoOferta);*/
                    var dadosMatriz = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(modelo.SeqPessoaAtuacao);

                    foreach (var item in modelo.GruposCurriculares)
                    {
                        // busca o grupo sendo dispensado
                        var grupo = curriculosCursosOfertasGrupos.FirstOrDefault(c => c.SeqGrupoCurricular == item.SeqGrupoCurricular && c.SeqCurriculoCursoOferta == dadosMatriz.SeqCurriculoCursoOferta);
                        if (grupo == null)
                            throw new SMCApplicationException("Grupo curricular selecionado para dispensa é inválido! Favor verificar se o grupo pertence ao currículo do aluno.");

                        var itemAdd = new SolicitacaoDispensaDestino()
                        {
                            SeqSolicitacaoDispensa = modelo.SeqSolicitacaoServico,
                            SeqCicloLetivo = modelo.SeqCicloLetivo,
                            SeqCurriculoCursoOfertaGrupo = grupo.Seq,
                            QuantidadeDispensaGrupo = item.QuantidadeDispensaGrupo,
                        };
                        itemAdd.Seq = item.Seq > 0 ? item.Seq : solicitacaoDispensa?.Destinos?.FirstOrDefault(d => d.SeqCurriculoCursoOfertaGrupo == itemAdd.SeqCurriculoCursoOfertaGrupo)?.Seq ?? 0;
                        destinos.Add(itemAdd);
                    }
                }
                var destinoBancoGrupos = solicitacaoDispensa?.Destinos?.Where(w => w.SeqCurriculoCursoOfertaGrupo != null).Select(sm => sm.Seq).ToList();
                var destinoGrupos = modelo?.GruposCurriculares.Select(sm => sm.Seq).ToList();
                var diferencaGrupos = destinoBancoGrupos.Except(destinoGrupos).ToList();

                if (diferencaGrupos.Any())
                {
                    modelo.ComponentesCurricularesExcluidos = solicitacaoDispensa.Destinos.Where(w => diferencaGrupos.Contains(w.Seq)).Select(s => s.Seq).ToList();
                    foreach (var item in modelo.ComponentesCurricularesExcluidos)
                    {
                        if (SolicitacaoDispensaGrupoDomainService.VerificarAgrupamento(modelo.SeqSolicitacaoServico, item))
                            this.SolicitacaoDispensaGrupoDestinoDomainService.Excluir(item);
                    }
                }

                solicitacaoDispensa.Destinos = destinos;

                this.SaveEntity(solicitacaoDispensa);

                ValidarSolicitacaoDispensaItens(modelo);

                // Se está deferindo a solicitação... Verifica se deve criar o grupo automaticamente.
                // RN_SRC_089 - Solicitação - Criação automática de grupos de itens de dispensa
                if (SMCContext.ApplicationId == SIGLA_APLICACAO.SGA_ADMINISTRATIVO)
                    SolicitacaoDispensaGrupoDomainService.VerificarAgrupamentoAutomatico(modelo.SeqSolicitacaoServico);

                tran.Commit();
            }
        }

        /// <summary>
        /// Busca os dados da solicitação de dispensa de acordo com a solicitação de serviço e a pessoa atuação
        /// Retorna inclusive a lista de ciclos letivos disponivel para seleção do aluno em caso de dispensa externa
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação (aluno que solicitou o serviço)</param>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns>Modelo para realizar o passo de escolha dos itens a serem dispensados</returns>
        public SolicitacaoDispensaItensCursadosVO PrepararModeloSolicitacaoDispensaItensCursados(long seqPessoaAtuacao, long seqSolicitacaoServico)
        {
            var specSolicitacaoDispensa = new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico);

            var solicitacaoDispensa = this.SearchProjectionByKey(specSolicitacaoDispensa, s => new
            {
                SeqsComponentesCurricularesInterna = s.OrigensInternas.Select(i => i.HistoricoEscolar.SeqComponenteCurricular).ToList(),
                SeqsHistoricos = s.OrigensInternas.Select(i => i.SeqHistoricoEscolar).ToList(),
                OrigensExternas = s.OrigensExternas.ToList(),
                SeqInstituicaoEnsino = s.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
            });

            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao);

            List<ComponenteCurricularListaVO> cursadosInstituicao = new List<ComponenteCurricularListaVO>();
            if (solicitacaoDispensa.SeqsHistoricos.Any())
                cursadosInstituicao = ComponenteCurricularDomainService.BuscarComponentesCurricularesDispensaLookup(new ComponenteCurricularDispensaFiltroVO { SeqPessoaAtuacao = seqPessoaAtuacao, Seqs = solicitacaoDispensa.SeqsHistoricos.ToArray() }).ToList();

            var modelo = new SolicitacaoDispensaItensCursadosVO()
            {
                // Buscar os ciclos letivos da instituição + nivel de ensino do aluno
                CiclosLetivos = CicloLetivoDomainService.BuscarCiclosLetivosPorInstituicaoENivelSelect(solicitacaoDispensa.SeqInstituicaoEnsino, dadosOrigem.SeqNivelEnsino),
                SeqSolicitacaoServico = seqSolicitacaoServico,
                ItensCursadosOutrasInstituicoes = solicitacaoDispensa.OrigensExternas.TransformList<SolicitacaoDispensaItensCursadosOutrasInstituicoesVO>(),
                ItensCursadosNestaInstituicao = cursadosInstituicao,
                Titulacoes = TitulacaoDomainService.BuscarTitulacoesSelect(new TitulacaoFiltroVO() { Ativo = true, DescricaoAbrevida = true }),
            };

            // Totais de horas cursadas e para dispensa
            var horasCursadasDispensa = CalcularTotaisDispensa(seqSolicitacaoServico);
            modelo.CursadosTotalCargaHorariaHoras = horasCursadasDispensa.Cursado.TotalHoras;
            modelo.CursadosTotalCargaHorariaHorasAula = horasCursadasDispensa.Cursado.TotalHorasAula;
            modelo.CursadosTotalCreditos = horasCursadasDispensa.Cursado.TotalCreditos;
            modelo.DispensaTotalCargaHorariaHoras = horasCursadasDispensa.Dispensa.TotalHoras;
            modelo.DispensaTotalCargaHorariaHorasAula = horasCursadasDispensa.Dispensa.TotalHorasAula;
            modelo.DispensaTotalCreditos = horasCursadasDispensa.Dispensa.TotalCreditos;

            return modelo;
        }

        /// <summary>
        /// Grava os dados da solicitação de dispensa origem interna e origem externa para a solicitação de dispensa informada
        /// </summary>
        /// <param name="modelo">Modelo da solicitação de dispensa com a solicitação de dispensa rigem interna e origem externa preenchidas</param>
        public void SalvarSolicitacaoDispensaItensCursados(SolicitacaoDispensaItensCursadosVO modelo)
        {
            // Busca a solicitação de dispensa
            var specSolicitacaoDispensa = new SMCSeqSpecification<SolicitacaoDispensa>(modelo.SeqSolicitacaoServico);
            var solicitacaoDispensa = this.SearchByKey(specSolicitacaoDispensa, IncludesSolicitacaoDispensa.OrigensInternas);

            // Atualiza as origens internas
            var origemInterna = new List<SolicitacaoDispensaOrigemInterna>();
            if (modelo.ItensCursadosNestaInstituicao != null)
            {
                foreach (var item in modelo.ItensCursadosNestaInstituicao)
                {
                    var solicitacaoInterna = new SolicitacaoDispensaOrigemInterna();
                    solicitacaoInterna.SeqHistoricoEscolar = item.Seq;
                    solicitacaoInterna.Seq = solicitacaoDispensa.OrigensInternas?.FirstOrDefault(o => o.SeqHistoricoEscolar == item.Seq)?.Seq ?? 0;
                    origemInterna.Add(solicitacaoInterna);
                }
                solicitacaoDispensa.OrigensInternas = origemInterna;
            }
            else
                solicitacaoDispensa.OrigensInternas?.Clear();

            // Atualiza as origens externas
            solicitacaoDispensa.OrigensExternas = modelo.ItensCursadosOutrasInstituicoes.SMCAny() ?
                modelo.ItensCursadosOutrasInstituicoes.TransformList<SolicitacaoDispensaOrigemExterna>() :
                new List<SolicitacaoDispensaOrigemExterna>();

            // Se existe origem externa informada, verifica se é o caso de migração de dados sem a informação de instituição
            // de ensino. Se existir, erro.
            if (solicitacaoDispensa.OrigensExternas != null && solicitacaoDispensa.OrigensExternas.Any(x => string.IsNullOrEmpty(x.Instituicao)))
                throw new SolicitacaoDispensaComOrigemExternaSemInstituicaoException();

            // Salva as alterações, caso tenha origem interna ou externa. Se nenhum dos dois informados, erro!
            if ((solicitacaoDispensa.OrigensInternas != null && solicitacaoDispensa.OrigensInternas.Any()) ||
                (solicitacaoDispensa.OrigensExternas != null && solicitacaoDispensa.OrigensExternas.Any()))
                this.SaveEntity(solicitacaoDispensa);
            else
                throw new SolicitacaoDispensaNenhumItemCursadoException();
        }

        public void ValidarSolicitacaoDispensaItens(SolicitacaoDispensaItensDispensadosVO modelo)
        {
            // Recupera os sequenciais dos tipos de componentes curriculares selecionados pelo aluno
            var seqsComponentesSelecionados = modelo.ComponentesCurriculares.Where(x => x.SeqComponenteCurricular.HasValue).Select(x => x.SeqComponenteCurricular.Value).ToArray();

            // Caso tenha algum, faz as validações necessárias
            if (seqsComponentesSelecionados.Any())
            {
                /* 1. Na seleção de componentes curriculares a dispensar, não permtir seleção de componente cujo tipo foi
                      parametrizado em "Tipo de Componente por Instituição e Nível de Ensino" para não permitir cadastro de
                      dispensa. Em caso de violação, abortar a operação e exibir mensagem de erro:
                        MENSAGEM Existe componente selecionado em "Componentes curriculares a dispensar" que não permite dispensa.
                */

                // Recupera os sequenciais do aluno
                var seqsAluno = this.AlunoDomainService.BuscarInstituicaoNivelEnsinoESequenciaisPorAluno(modelo.SeqPessoaAtuacao);

                var specComponentesSelecionados = new SMCContainsSpecification<ComponenteCurricular, long>(g => g.Seq, seqsComponentesSelecionados);
                var componentesSelecionados = ComponenteCurricularDomainService.SearchProjectionBySpecification(specComponentesSelecionados, x => new { x.SeqTipoComponenteCurricular, x.Descricao }).ToList();

                // Recupera os sequenciais que permitem
                var naoPermiteDispensa = InstituicaoNivelTipoComponenteCurricularDomainService.SearchProjectionBySpecification(new InstituicaoNivelTipoComponenteCurricularFilterSpecification
                {
                    SeqNivelEnsino = seqsAluno.SeqNivelEnsino,
                    SeqInstituicaoEnsino = seqsAluno.SeqInstituicao,
                    PermiteCadastroDispensa = false,
                    SeqsTiposComponentesCurriculares = componentesSelecionados.Select(x => x.SeqTipoComponenteCurricular).Distinct().ToList()
                }, x => x.SeqTipoComponenteCurricular);

                if (naoPermiteDispensa.Any())
                {
                    var componentes = componentesSelecionados.Where(c => naoPermiteDispensa.Contains(c.SeqTipoComponenteCurricular)).Select(c => c.Descricao);
                    throw new SolicitacaoDispensaComponenteCurricularNaoPermiteDispensaException(SMCStringHelper.JoinWithLastSeparatorIgnoringNullOrEmpty(", ", " e ", componentes));
                }

                /* 2. Na seleção de componentes curriculares a dispensar, não permtir seleção de componente cursado com
                      aprovação pelo aluno ou dispensado para o aluno em questão. Em caso de violação, abortar a operação e
                      exibir mensagem de erro:
                        MENSAGEM Você já cursou com aprovação ou já foi dispensado de um ou mais componentes selecionados  em "Componentes curriculares a dispensar"
                */

                //// Recupera qual o aluno histórico atual
                //var seqAlunoHistoricoAtual = AlunoDomainService.SearchProjectionByKey(new SMCSeqSpecification<Aluno>(modelo.SeqPessoaAtuacao), x => x.Historicos.FirstOrDefault(h => h.Atual).Seq);

                //// Busca os componentes curriculares para validar aprovação
                //var componentesComAssunto = modelo.ComponentesCurriculares.Where(x => x.SeqComponenteCurricular.HasValue).Select(s => new HistoricoEscolarAprovadoFiltroVO
                //{
                //    SeqComponenteCurricular = s.SeqComponenteCurricular,
                //    SeqComponenteCurricularAssunto = s.SeqComponenteCurricularAssunto
                //}).ToList();

                //// Valida a aprovação dos componentes curriculares
                //var componentesAprovados = HistoricoEscolarDomainService.ObterComponentesCurricularesAprovados(seqAlunoHistoricoAtual, componentesComAssunto);
                //if (componentesAprovados.Any())
                //    throw new SolicitacaoDispensaComponenteCurricularAprovadoException(SMCStringHelper.JoinWithLastSeparatorIgnoringNullOrEmpty(", ", " e ", componentesAprovados.Select(c => c.DescricaoComponenteCurricularAprovado)));
            }

            /* 5. Verificar se o somatório de carga horária dos itens cursados está dentro do percentual de equivalência do somatório de carga
                  horária dos itens a serem dispensados (Se somatório a dispensar ou se somatório dos itens cursados for igual a zero, não realizar
                  esta consistência):
                  (somatório cursado/somatório a dispensar) x 100 >= Percentual de equivalência
                  Onde percentual de equivalência é o valor parametrizado em "Parâmetro por Instituição e Nível".
                  Em caso de violação, abortar a operação e exibir a mensagem de erro:
                  MENSAGEM "Para solicitar uma dispensa, é necessário que o somatório de carga horária dos itens cursados seja, no mínimo, [percentual de equivalência]% equivalente ao somatório de carga horária dos itens solicitados para dispensa".
            */
            var seqNivelEnsinoHistoricoAtual = this.AlunoHistoricoDomainService.BuscarNivelEnsinoHistoricoAtualAluno(modelo.SeqPessoaAtuacao);
            var instituicaoNivel = this.InstituicaoNivelDomainService.BuscarInstituicaoNivelPorNivelEnsino(seqNivelEnsinoHistoricoAtual);

            var horas = CalcularTotaisDispensa(modelo.SeqSolicitacaoServico);
            bool atingiuMinimo = false;
            decimal percent = 0;

            /*
                Novas regras passadas pela Janice

                - se itens a dispensar tiver 0 crédito e 0 CH pode passar
                - se itens a dispensar tiver CH e crédito tem que obedecer a regra de 80%s
                - se um lado tiver só CH e o outro só crédito (ou contrário), no lado que tiver só CH converter para crédito (dividir CH por 15) para fazer a comparação de 80%
                - se itens cursados tiver 0 crédito e 0 CH por agora deixar passar, no futuro teremos uma nova regra para isso, vou aguardar a Carol retornar de férias
             */

            if (horas.Dispensa.TotalCreditos <= 0 && horas.Dispensa.TotalHoras <= 0)
                atingiuMinimo = true;

            if (horas.Cursado.TotalCreditos <= 0 && horas.Cursado.TotalHoras <= 0)
                atingiuMinimo = true;

            if (!atingiuMinimo && (horas.Cursado.TotalHoras > 0 || horas.Cursado.TotalHorasPorCredito > 0))
            {
                percent = 0;
                if (horas.Cursado.TotalHoras > 0 && horas.Dispensa.TotalHoras > 0)
                    percent = horas.Cursado.TotalHoras / (decimal)horas.Dispensa.TotalHoras * 100;
                else if (horas.Cursado.TotalHorasPorCredito > 0 && horas.Dispensa.TotalHoras > 0)
                    percent = horas.Cursado.TotalHorasPorCredito / (decimal)horas.Dispensa.TotalHoras * 100;
                else if (horas.Cursado.TotalHoras > 0 && horas.Dispensa.TotalHorasPorCredito > 0)
                    percent = horas.Cursado.TotalHoras / (decimal)horas.Dispensa.TotalHorasPorCredito * 100;
                else if (horas.Cursado.TotalHorasPorCredito > 0 && horas.Dispensa.TotalHorasPorCredito > 0)
                    percent = horas.Cursado.TotalHorasPorCredito / (decimal)horas.Dispensa.TotalHorasPorCredito * 100;

                if (percent >= instituicaoNivel.PercentualEquivalencia)
                    atingiuMinimo = true;
            }

            if (!atingiuMinimo && (horas.Cursado.TotalCreditos > 0 || horas.Cursado.TotalCreditosPorHora > 0))
            {
                percent = 0;
                if (horas.Cursado.TotalCreditos > 0 && horas.Dispensa.TotalCreditos > 0)
                    percent = horas.Cursado.TotalCreditos / (decimal)horas.Dispensa.TotalCreditos * 100;
                else if (horas.Cursado.TotalCreditosPorHora > 0 && horas.Dispensa.TotalCreditos > 0)
                    percent = horas.Cursado.TotalCreditosPorHora / (decimal)horas.Dispensa.TotalCreditos * 100;
                else if (horas.Cursado.TotalCreditos > 0 && horas.Dispensa.TotalCreditosPorHora > 0)
                    percent = horas.Cursado.TotalCreditos / (decimal)horas.Dispensa.TotalCreditosPorHora * 100;
                else if (horas.Cursado.TotalCreditosPorHora > 0 && horas.Dispensa.TotalCreditosPorHora > 0)
                    percent = horas.Cursado.TotalCreditosPorHora / (decimal)horas.Dispensa.TotalCreditosPorHora * 100;

                if (percent >= instituicaoNivel.PercentualEquivalencia)
                    atingiuMinimo = true;
            }

            if (!atingiuMinimo)
                throw new SolicitacaoDispensaCursadosCargaHorariaException(instituicaoNivel.PercentualEquivalencia.ToString());
        }

        /// <summary>
        /// Verifica se deve realizar o deferimento automático de uma solicitação de dispensa que possui uma equivalencia correspondente
        /// aos itens da dispensa.
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de dispensa a ser avaliada</param>
		public void EfetuarDispensaAutomatica(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var spec = new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico);
            var solicitacao = SearchProjectionByKey(spec, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                ExisteOrigemExterna = x.OrigensExternas.Any(),
                OrigensInternas = x.OrigensInternas.Select(i => new
                {
                    Seq = i.Seq,
                    SeqPessoaAtuacaoHistorico = i.HistoricoEscolar.AlunoHistorico.SeqAluno,
                    SeqComponenteCurricular = i.HistoricoEscolar.SeqComponenteCurricular
                }).ToList(),
                Destinos = x.Destinos.Select(d => new
                {
                    Seq = d.Seq,
                    SeqComponenteCurricular = d.SeqComponenteCurricular
                }).ToList()
            });

            // Se existe origem externa na solicitação não pode ter deferimento automático
            if (solicitacao.ExisteOrigemExterna)
                return;

            // Verifica se tem pelo menos uma origem interna e um destino
            if (!solicitacao.OrigensInternas.Any() || !solicitacao.Destinos.Any())
                return;

            // Verifica se o sequencial da pessoa-atuação das origens internas é igual a pessoa-atuação da solicitação
            foreach (var origem in solicitacao.OrigensInternas)
            {
                if (origem.SeqPessoaAtuacaoHistorico != solicitacao.SeqPessoaAtuacao)
                    return;
            }

            // Busca a matriz do aluno
            var seqMatrizCurricular = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(solicitacao.SeqPessoaAtuacao).SeqMatrizCurricularOferta.GetValueOrDefault();

            // Verifica se existe algum registro de dispensa (equivalencia) com os mesmo itens da solicitação de dispensa,
            // que não tenha a matriz na exceção e esteja ativa.
            var dispensa = DispensaDomainService.BuscarDispensaEquivalente(solicitacao.OrigensInternas.Select(i => i.SeqComponenteCurricular.HasValue ? i.SeqComponenteCurricular.Value : 0).ToList(),
                                                                           solicitacao.Destinos.Select(d => d.SeqComponenteCurricular.HasValue ? d.SeqComponenteCurricular.Value : 0).ToList(),

                                                                           seqMatrizCurricular);
            // Se encontrou a dispensa...
            if (dispensa.SeqDispensa.HasValue)
            {
                // Grava um grupo para a solicitação de dispensa com os itens informados
                var grupo = new SolicitacaoDispensaGrupo()
                {
                    SeqSolicitacaoDispensa = seqSolicitacaoServico,
                    SeqDispensa = dispensa.SeqDispensa.Value,
                    ModoExibicaoHistoricoEscolar = dispensa.ModoExibicao.Value,
                    OrigensInternas = new List<SolicitacaoDispensaGrupoOrigemInterna>(),
                    Destinos = new List<SolicitacaoDispensaGrupoDestino>()
                };
                foreach (var origem in solicitacao.OrigensInternas)
                {
                    var origemGrupo = new SolicitacaoDispensaGrupoOrigemInterna()
                    {
                        SeqSolicitacaoDispensaOrigemInterna = origem.Seq
                    };
                    grupo.OrigensInternas.Add(origemGrupo);
                }
                foreach (var destino in solicitacao.Destinos)
                {
                    var destinoGrupo = new SolicitacaoDispensaGrupoDestino()
                    {
                        SeqSolicitacaoDispensaDestino = destino.Seq
                    };
                    grupo.Destinos.Add(destinoGrupo);
                }
                SolicitacaoDispensaGrupoDomainService.SaveEntity(grupo);

                // Faz o processo de realizar atendimento
                SolicitacaoServicoDomainService.RealizarAtendimento(seqSolicitacaoServico, true, "Solicitação deferida automaticamente por existir equivalência cadastrada.");
            }
        }

        /// <summary>
        /// Atualiza a descrição de uma solicitação de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser atualizada</param>
        /// <param name="indOriginal">Flag para atualizar a descrição original ou atualizada</param>
        /// <returns>Descrição da solicitação</returns>
        public string AtualizarDescricao(long seqSolicitacaoServico, bool indOriginal)
        {
            if (indOriginal)
                return AtualizarDescricaoOriginal(seqSolicitacaoServico);
            else
                return AtualizarDescricaoAtualizada(seqSolicitacaoServico);
        }

        /// <summary>
        /// Atualiza a descrição original de uma solicitação de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser atualizada</param>
        /// <returns>Descrição da solicitação</returns>
        private string AtualizarDescricaoOriginal(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var spec = new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico);
            var dadosSolicitacao = this.SearchProjectionByKey(spec, s => new
            {
                SeqPessoaAtuacao = s.SeqPessoaAtuacao,
                Destinos = s.Destinos.Select(x => new
                {
                    ComponenteCurricular = x.SeqComponenteCurricular.HasValue ? new
                    {
                        SeqComponenteCurricular = x.SeqComponenteCurricular,
                        SeqComponenteCurricularAssunto = x.SeqComponenteCurricularAssunto,
                        DescricaoComponenteCurricular = x.ComponenteCurricular.Descricao,
                        DescricaoComponenteCurricularAssunto = x.ComponenteCurricularAssunto.Descricao,
                        CargaHoraria = x.ComponenteCurricular.CargaHoraria,
                        Creditos = x.ComponenteCurricular.Credito,
                        Codigo = x.ComponenteCurricular.Codigo,
                    } : null,
                    GrupoComponente = x.SeqCurriculoCursoOfertaGrupo.HasValue ? new
                    {
                        DescricaoGrupoCurricular = x.CurriculoCursoOfertaGrupo.GrupoCurricular.Descricao,
                        DescricaoTipoConfiguracaoGrupoCurricular = x.CurriculoCursoOfertaGrupo.GrupoCurricular.TipoGrupoCurricular.Descricao,
                        HoraAula = x.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraAula,
                        Creditos = x.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeCreditos,
                        Itens = x.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeItens,
                        FormatoConfiguracaoGrupo = x.CurriculoCursoOfertaGrupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                        QuantidadeDispensaGrupo = x.QuantidadeDispensaGrupo,
                    } : null,
                }),
                OrigemExterna = s.OrigensExternas.Select(x => new
                {
                    SeqCicloLetivo = x.SeqCicloLetivo,
                    DescricaoCicloLetivo = x.CicloLetivo.Descricao,
                    Dispensa = x.Dispensa,
                    CargaHoraria = x.CargaHoraria,
                    FormatoCargaHoraria = x.FormatoCargaHoraria,
                    Credito = x.Credito,
                    Nota = x.Nota,
                    Conceito = x.Conceito,
                    DescricaoInstituicao = x.Instituicao,
                    SeqTitulacaoProfessor = x.SeqTitulacaoProfessor,
                    DescricaoTitulacaoProdessor = x.TitulacaoProfessor.DescricaoMasculino,
                    NomeProfessor = x.NomeProfessor,
                    Observacao = x.Observacao
                }),
                OrigemInterna = s.OrigensInternas.Select(x => new
                {
                    SeqComponenteCurricular = x.HistoricoEscolar.SeqComponenteCurricular,
                    Nota = x.HistoricoEscolar.Nota,
                    Conceito = x.HistoricoEscolar.EscalaApuracaoItem.Descricao,
                    SeqComponenteCurricularAssunto = x.HistoricoEscolar.SeqComponenteCurricularAssunto,
                    DescricaoComponenteCurricular = x.HistoricoEscolar.ComponenteCurricular.Descricao,
                    DescricaoComponenteCurricularAssunto = x.HistoricoEscolar.ComponenteCurricularAssunto.Descricao,
                    CargaHoraria = x.HistoricoEscolar.CargaHorariaRealizada,
                    Credito = x.HistoricoEscolar.Credito,
                    Codigo = x.HistoricoEscolar.ComponenteCurricular.Codigo
                })
            });

            if (dadosSolicitacao != null)
            {
                // Templates para criação dos elementos abaixo
                Func<SMCSize, SMCSize, SMCSize, SMCSize, string, object, string> GerarItem = (sizeMD, sizeXS, sizeSM, sizeLG, label, valor) => { return $"<div class=\"{SMCSizeHelper.GetSizeClasses(sizeMD, sizeXS, sizeSM, sizeLG)}\"><div class=\"smc-display\"><label>{label ?? string.Empty }</label><p>{ valor ?? string.Empty }</p></div></div>"; };
                Func<string, string, string> GerarFieldset = (label, conteudo) => { return $"<fieldset><legend>{label ?? string.Empty }</legend>{conteudo ?? string.Empty }</fieldset>"; };
                Func<string, string, string> GerarFieldsetTerciario = (label, conteudo) => { return $"<fieldset class=\"smc-sga-fieldset-terciario\"><legend>{label ?? string.Empty }</legend>{conteudo ?? string.Empty }</fieldset>"; };

                // Monta os dados de destinos
                string itensComponente = string.Empty;
                string itensGrupo = string.Empty;
                foreach (var item in dadosSolicitacao.Destinos)
                {
                    if (item.ComponenteCurricular != null)
                    {
                        if (!string.IsNullOrEmpty(itensComponente))
                            itensComponente += "<hr>";

                        // Formata a Descrição
                        itensComponente += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "ID", item.ComponenteCurricular.Codigo);
                        if (item.ComponenteCurricular.SeqComponenteCurricularAssunto.HasValue)
                            itensComponente += GerarItem(SMCSize.Grid14_24, SMCSize.Grid18_24, SMCSize.Grid14_24, SMCSize.Grid14_24, "Componente curricular", $"{item.ComponenteCurricular.DescricaoComponenteCurricular} - {item.ComponenteCurricular.DescricaoComponenteCurricularAssunto}");
                        else
                            itensComponente += GerarItem(SMCSize.Grid14_24, SMCSize.Grid18_24, SMCSize.Grid14_24, SMCSize.Grid14_24, "Componente curricular", item.ComponenteCurricular.DescricaoComponenteCurricular);

                        if (item.ComponenteCurricular.CargaHoraria.HasValue)
                            itensComponente += GerarItem(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Carga horaria", item.ComponenteCurricular.CargaHoraria.Value);

                        if (item.ComponenteCurricular.Creditos.HasValue)
                            itensComponente += GerarItem(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Crédito", item.ComponenteCurricular.Creditos.Value);
                    }
                    else if (item.GrupoComponente != null)
                    {
                        if (!string.IsNullOrEmpty(itensGrupo))
                            itensGrupo += "<hr>";

                        itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid16_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Grupo curricular", item.GrupoComponente.DescricaoGrupoCurricular);

                        if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria)
                            itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid6_24, item.GrupoComponente.DescricaoTipoConfiguracaoGrupoCurricular, $"{item.GrupoComponente.HoraAula} horas-aula");
                        else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito)
                            itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid6_24, item.GrupoComponente.DescricaoTipoConfiguracaoGrupoCurricular, $"{item.GrupoComponente.Creditos} crédito(s)");
                        else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens)
                            itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid6_24, item.GrupoComponente.DescricaoTipoConfiguracaoGrupoCurricular, $"{item.GrupoComponente.Itens} iten(s)");

                        if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria)
                            itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Total a ser dispensado", $"{item.GrupoComponente.QuantidadeDispensaGrupo} horas-aula");
                        else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito)
                            itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Total a ser dispensado", $"{item.GrupoComponente.QuantidadeDispensaGrupo} crédito(s)");
                        else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens)
                            itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Total a ser dispensado", $"{item.GrupoComponente.QuantidadeDispensaGrupo} iten(s)");
                    }
                }

                // Monta os dados de origens externas
                string itensOrigemExterna = string.Empty;
                foreach (var item in dadosSolicitacao.OrigemExterna)
                {
                    if (!string.IsNullOrEmpty(itensOrigemExterna))
                        itensOrigemExterna += "<hr>";

                    if (item.SeqCicloLetivo.HasValue)
                        itensOrigemExterna += GerarItem(SMCSize.Grid4_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Ciclo letivo", item.DescricaoCicloLetivo);

                    itensOrigemExterna += GerarItem(SMCSize.Grid20_24, SMCSize.Grid16_24, SMCSize.Grid20_24, SMCSize.Grid20_24, "Descrição", item.Dispensa);

                    if (item.CargaHoraria.HasValue)
                        itensOrigemExterna += GerarItem(SMCSize.Grid4_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Carga horária", item.CargaHoraria);

                    if (item.FormatoCargaHoraria.HasValue)
                        itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid16_24, SMCSize.Grid7_24, SMCSize.Grid5_24, "Formato de carga horária", SMCEnumHelper.GetDescription(item.FormatoCargaHoraria));

                    if (item.Credito.HasValue)
                        itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid5_24, SMCSize.Grid5_24, "Créditos", item.Credito);

                    if (item.Nota.HasValue)
                        itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid5_24, "Nota", item.Nota);

                    if (!string.IsNullOrEmpty(item.Conceito))
                        itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid5_24, "Conceito", item.Conceito);

                    if (!string.IsNullOrEmpty(item.DescricaoInstituicao))
                        itensOrigemExterna += GerarItem(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid11_24, "Instituição", item.DescricaoInstituicao);

                    if (item.SeqTitulacaoProfessor.HasValue)
                        itensOrigemExterna += GerarItem(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24, "Titulação", item.DescricaoTitulacaoProdessor);

                    if (!string.IsNullOrEmpty(item.NomeProfessor))
                        itensOrigemExterna += GerarItem(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid11_24, "Professor", item.NomeProfessor);

                    if (!string.IsNullOrEmpty(item.Observacao))
                        itensOrigemExterna += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Observacao", item.Observacao);
                }

                // Monta os dados de origens internas
                string itensOrigemInterna = string.Empty;
                foreach (var item in dadosSolicitacao.OrigemInterna)
                {
                    if (!string.IsNullOrEmpty(itensOrigemInterna))
                        itensOrigemInterna += "<hr>";

                    itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "ID", item.Codigo);

                    if (item.SeqComponenteCurricularAssunto.HasValue)
                        itensOrigemInterna += GerarItem(SMCSize.Grid13_24, SMCSize.Grid18_24, SMCSize.Grid13_24, SMCSize.Grid13_24, "Componente curricular", $"{item.DescricaoComponenteCurricular} - {item.DescricaoComponenteCurricularAssunto}");
                    else
                        itensOrigemInterna += GerarItem(SMCSize.Grid13_24, SMCSize.Grid18_24, SMCSize.Grid13_24, SMCSize.Grid13_24, "Componente curricular", item.DescricaoComponenteCurricular);

                    itensOrigemInterna += GerarItem(SMCSize.Grid3_24, SMCSize.Grid6_24, SMCSize.Grid3_24, SMCSize.Grid3_24, "Carga horaria", item.CargaHoraria);
                    itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "Crédito", item.Credito);

                    if (item.Nota.HasValue)
                        itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "Nota", item.Nota);

                    if (!string.IsNullOrWhiteSpace(item.Conceito))
                        itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "Conceito", item.Conceito);
                }

                // Monta os fieldsets de grupo e componente
                string fieldsetOrigemInterna = string.Empty;
                string fieldsetOrigemExterna = string.Empty;
                string fieldsetGrupo = string.Empty;
                string fieldsetComponente = string.Empty;

                if (!string.IsNullOrEmpty(itensOrigemExterna))
                    fieldsetOrigemExterna += GerarFieldsetTerciario("Itens cursados em outras instituições", itensOrigemExterna);

                if (!string.IsNullOrEmpty(itensOrigemInterna))
                    fieldsetOrigemInterna += GerarFieldsetTerciario("Itens cursados nesta instituição", itensOrigemInterna);

                if (!string.IsNullOrEmpty(itensComponente))
                    fieldsetComponente += GerarFieldsetTerciario("Componente curricular", itensComponente);

                if (!string.IsNullOrEmpty(itensGrupo))
                    fieldsetGrupo += GerarFieldsetTerciario("Grupo curricular", itensGrupo);

                // Monta a descrição final
                string novaDescricao = string.Empty;

                if (!string.IsNullOrEmpty(fieldsetOrigemExterna) || !string.IsNullOrEmpty(fieldsetOrigemInterna))
                    novaDescricao += GerarFieldset("Itens cursados", $"{fieldsetOrigemExterna}{fieldsetOrigemInterna}");

                if (!string.IsNullOrEmpty(fieldsetComponente) || !string.IsNullOrEmpty(fieldsetGrupo))
                    novaDescricao += GerarFieldset("Itens a serem dispensados", $"{fieldsetComponente}{fieldsetGrupo}");

                // Salva a Descrição
                this.UpdateFields<SolicitacaoDispensa>(new SolicitacaoDispensa { Seq = seqSolicitacaoServico, DescricaoOriginal = novaDescricao }, x => x.DescricaoOriginal);

                return novaDescricao;
            }

            return null;
        }

        /// <summary>
        /// Atualiza a descrição atualizada de uma solicitação de dispensa
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser atualizada</param>
        /// <returns>Descrição da solicitação</returns>
        private string AtualizarDescricaoAtualizada(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação agrupados
            var spec = new SolicitacaoDispensaGrupoFilterSpecification()
            {
                SeqSolicitacaoDispensa = seqSolicitacaoServico
            };
            var grupos = SolicitacaoDispensaGrupoDomainService.SearchProjectionBySpecification(spec, g => new
            {
                ModoExibicaoHistoricoEscolar = g.ModoExibicaoHistoricoEscolar,
                Destinos = g.Destinos.Select(x => new
                {
                    ComponenteCurricular = x.SolicitacaoDispensaDestino.SeqComponenteCurricular.HasValue ? new
                    {
                        SeqComponenteCurricular = x.SolicitacaoDispensaDestino.SeqComponenteCurricular,
                        SeqComponenteCurricularAssunto = x.SolicitacaoDispensaDestino.SeqComponenteCurricularAssunto,
                        DescricaoComponenteCurricular = x.SolicitacaoDispensaDestino.ComponenteCurricular.Descricao,
                        DescricaoComponenteCurricularAssunto = x.SolicitacaoDispensaDestino.ComponenteCurricularAssunto.Descricao,
                        CargaHoraria = x.SolicitacaoDispensaDestino.ComponenteCurricular.CargaHoraria,
                        Creditos = x.SolicitacaoDispensaDestino.ComponenteCurricular.Credito,
                        Codigo = x.SolicitacaoDispensaDestino.ComponenteCurricular.Codigo,
                    } : null,
                    GrupoComponente = x.SolicitacaoDispensaDestino.SeqCurriculoCursoOfertaGrupo.HasValue ? new
                    {
                        DescricaoGrupoCurricular = x.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.Descricao,
                        DescricaoTipoConfiguracaoGrupoCurricular = x.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.TipoGrupoCurricular.Descricao,
                        HoraAula = x.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraAula,
                        Creditos = x.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeCreditos,
                        Itens = x.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeItens,
                        FormatoConfiguracaoGrupo = x.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                        QuantidadeDispensaGrupo = x.SolicitacaoDispensaDestino.QuantidadeDispensaGrupo,
                    } : null,
                }),
                OrigemExterna = g.OrigensExternas.Select(x => new
                {
                    SeqCicloLetivo = x.SolicitacaoDispensaOrigemExterna.SeqCicloLetivo,
                    DescricaoCicloLetivo = x.SolicitacaoDispensaOrigemExterna.CicloLetivo.Descricao,
                    Dispensa = x.SolicitacaoDispensaOrigemExterna.Dispensa,
                    CargaHoraria = x.SolicitacaoDispensaOrigemExterna.CargaHoraria,
                    FormatoCargaHoraria = x.SolicitacaoDispensaOrigemExterna.FormatoCargaHoraria,
                    Credito = x.SolicitacaoDispensaOrigemExterna.Credito,
                    Nota = x.SolicitacaoDispensaOrigemExterna.Nota,
                    Conceito = x.SolicitacaoDispensaOrigemExterna.Conceito,
                    DescricaoInstituicao = x.SolicitacaoDispensaOrigemExterna.Instituicao,
                    SeqTitulacaoProfessor = x.SolicitacaoDispensaOrigemExterna.SeqTitulacaoProfessor,
                    DescricaoTitulacaoProdessor = x.SolicitacaoDispensaOrigemExterna.TitulacaoProfessor.DescricaoMasculino,
                    NomeProfessor = x.SolicitacaoDispensaOrigemExterna.NomeProfessor,
                    Observacao = x.SolicitacaoDispensaOrigemExterna.Observacao
                }),
                OrigemInterna = g.OrigensInternas.Select(x => new
                {
                    SeqComponenteCurricular = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.SeqComponenteCurricular,
                    Nota = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.Nota,
                    Conceito = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.EscalaApuracaoItem.Descricao,
                    SeqComponenteCurricularAssunto = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.SeqComponenteCurricularAssunto,
                    DescricaoComponenteCurricular = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricular.Descricao,
                    DescricaoComponenteCurricularAssunto = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricularAssunto.Descricao,
                    CargaHoraria = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.CargaHorariaRealizada,
                    Credito = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.Credito,
                    Codigo = x.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricular.Codigo
                })
            });

            if (grupos != null)
            {
                // Templates para criação dos elementos abaixo
                Func<SMCSize, SMCSize, SMCSize, SMCSize, string, object, string> GerarItem = (sizeMD, sizeXS, sizeSM, sizeLG, label, valor) => { return $"<div class=\"{SMCSizeHelper.GetSizeClasses(sizeMD, sizeXS, sizeSM, sizeLG)}\"><div class=\"smc-display\"><label>{label ?? string.Empty }</label><p>{ valor ?? string.Empty }</p></div></div>"; };
                Func<string, string, string> GerarFieldset = (label, conteudo) => { return $"<fieldset><legend>{label ?? string.Empty }</legend>{conteudo ?? string.Empty }</fieldset>"; };
                Func<string, string, string> GerarFieldsetTerciario = (label, conteudo) => { return $"<fieldset class=\"smc-sga-fieldset-terciario\"><legend>{label ?? string.Empty }</legend>{conteudo ?? string.Empty }</fieldset>"; };

                // Monta a descrição final
                string novaDescricao = string.Empty;

                // Para cada grupo
                foreach (var grupo in grupos)
                {
                    // Monta os dados de destinos
                    string itensComponente = string.Empty;
                    string itensGrupo = string.Empty;
                    foreach (var item in grupo.Destinos)
                    {
                        if (item.ComponenteCurricular != null)
                        {
                            if (!string.IsNullOrEmpty(itensComponente))
                                itensComponente += "<hr>";

                            // Formata a Descrição
                            itensComponente += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "ID", item.ComponenteCurricular.Codigo);
                            if (item.ComponenteCurricular.SeqComponenteCurricularAssunto.HasValue)
                                itensComponente += GerarItem(SMCSize.Grid14_24, SMCSize.Grid18_24, SMCSize.Grid14_24, SMCSize.Grid14_24, "Componente curricular", $"{item.ComponenteCurricular.DescricaoComponenteCurricular} - {item.ComponenteCurricular.DescricaoComponenteCurricularAssunto}");
                            else
                                itensComponente += GerarItem(SMCSize.Grid14_24, SMCSize.Grid18_24, SMCSize.Grid14_24, SMCSize.Grid14_24, "Componente curricular", item.ComponenteCurricular.DescricaoComponenteCurricular);

                            if (item.ComponenteCurricular.CargaHoraria.HasValue)
                                itensComponente += GerarItem(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Carga horaria", item.ComponenteCurricular.CargaHoraria.Value);

                            if (item.ComponenteCurricular.Creditos.HasValue)
                                itensComponente += GerarItem(SMCSize.Grid4_24, SMCSize.Grid6_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Crédito", item.ComponenteCurricular.Creditos.Value);
                        }
                        else if (item.GrupoComponente != null)
                        {
                            if (!string.IsNullOrEmpty(itensGrupo))
                                itensGrupo += "<hr>";

                            itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid16_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Grupo curricular", item.GrupoComponente.DescricaoGrupoCurricular);

                            if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria)
                                itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid6_24, item.GrupoComponente.DescricaoTipoConfiguracaoGrupoCurricular, $"{item.GrupoComponente.HoraAula} horas-aula");
                            else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito)
                                itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid6_24, item.GrupoComponente.DescricaoTipoConfiguracaoGrupoCurricular, $"{item.GrupoComponente.Creditos} crédito(s)");
                            else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens)
                                itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid8_24, SMCSize.Grid6_24, SMCSize.Grid6_24, item.GrupoComponente.DescricaoTipoConfiguracaoGrupoCurricular, $"{item.GrupoComponente.Itens} iten(s)");

                            if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.CargaHoraria)
                                itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Total a ser dispensado", $"{item.GrupoComponente.QuantidadeDispensaGrupo} horas-aula");
                            else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Credito)
                                itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Total a ser dispensado", $"{item.GrupoComponente.QuantidadeDispensaGrupo} crédito(s)");
                            else if (item.GrupoComponente.FormatoConfiguracaoGrupo == Common.Areas.CUR.Enums.FormatoConfiguracaoGrupo.Itens)
                                itensGrupo += GerarItem(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid6_24, "Total a ser dispensado", $"{item.GrupoComponente.QuantidadeDispensaGrupo} iten(s)");
                        }
                    }

                    // Monta os dados de origens externas
                    string itensOrigemExterna = string.Empty;
                    foreach (var item in grupo.OrigemExterna)
                    {
                        if (!string.IsNullOrEmpty(itensOrigemExterna))
                            itensOrigemExterna += "<hr>";

                        if (item.SeqCicloLetivo.HasValue)
                            itensOrigemExterna += GerarItem(SMCSize.Grid4_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Ciclo letivo", item.DescricaoCicloLetivo);

                        itensOrigemExterna += GerarItem(SMCSize.Grid20_24, SMCSize.Grid16_24, SMCSize.Grid20_24, SMCSize.Grid20_24, "Descrição", item.Dispensa);

                        if (item.CargaHoraria.HasValue)
                            itensOrigemExterna += GerarItem(SMCSize.Grid4_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid4_24, "Carga horária", item.CargaHoraria);

                        if (item.FormatoCargaHoraria.HasValue)
                            itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid16_24, SMCSize.Grid7_24, SMCSize.Grid5_24, "Formato de carga horária", SMCEnumHelper.GetDescription(item.FormatoCargaHoraria));

                        if (item.Credito.HasValue)
                            itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid5_24, SMCSize.Grid5_24, "Créditos", item.Credito);

                        if (item.Nota.HasValue)
                            itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid5_24, "Nota", item.Nota);

                        if (!string.IsNullOrEmpty(item.Conceito))
                            itensOrigemExterna += GerarItem(SMCSize.Grid5_24, SMCSize.Grid8_24, SMCSize.Grid4_24, SMCSize.Grid5_24, "Conceito", item.Conceito);

                        if (!string.IsNullOrEmpty(item.DescricaoInstituicao))
                            itensOrigemExterna += GerarItem(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid11_24, "Instituição", item.DescricaoInstituicao);

                        if (item.SeqTitulacaoProfessor.HasValue)
                            itensOrigemExterna += GerarItem(SMCSize.Grid3_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid2_24, "Titulação", item.DescricaoTitulacaoProdessor);

                        if (!string.IsNullOrEmpty(item.NomeProfessor))
                            itensOrigemExterna += GerarItem(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid11_24, "Professor", item.NomeProfessor);

                        if (!string.IsNullOrEmpty(item.Observacao))
                            itensOrigemExterna += GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Observacao", item.Observacao);
                    }

                    // Monta os dados de origens internas
                    string itensOrigemInterna = string.Empty;
                    foreach (var item in grupo.OrigemInterna)
                    {
                        if (!string.IsNullOrEmpty(itensOrigemInterna))
                            itensOrigemInterna += "<hr>";

                        itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "ID", item.Codigo);

                        if (item.SeqComponenteCurricularAssunto.HasValue)
                            itensOrigemInterna += GerarItem(SMCSize.Grid13_24, SMCSize.Grid18_24, SMCSize.Grid13_24, SMCSize.Grid13_24, "Componente curricular", $"{item.DescricaoComponenteCurricular} - {item.DescricaoComponenteCurricularAssunto}");
                        else
                            itensOrigemInterna += GerarItem(SMCSize.Grid13_24, SMCSize.Grid18_24, SMCSize.Grid13_24, SMCSize.Grid13_24, "Componente curricular", item.DescricaoComponenteCurricular);

                        itensOrigemInterna += GerarItem(SMCSize.Grid3_24, SMCSize.Grid6_24, SMCSize.Grid3_24, SMCSize.Grid3_24, "Carga horaria", item.CargaHoraria);
                        itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "Crédito", item.Credito);

                        if (item.Nota.HasValue)
                            itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "Nota", item.Nota);

                        if (!string.IsNullOrWhiteSpace(item.Conceito))
                            itensOrigemInterna += GerarItem(SMCSize.Grid2_24, SMCSize.Grid6_24, SMCSize.Grid2_24, SMCSize.Grid2_24, "Conceito", item.Conceito);
                    }

                    // Monta os fieldsets de itens
                    string fieldsetOrigemInterna = string.Empty;
                    string fieldsetOrigemExterna = string.Empty;
                    string fieldsetDestino = string.Empty;

                    if (!string.IsNullOrEmpty(itensOrigemExterna))
                        fieldsetOrigemExterna += GerarFieldsetTerciario("Itens cursados em outras instituições", itensOrigemExterna);

                    if (!string.IsNullOrEmpty(itensOrigemInterna))
                        fieldsetOrigemInterna += GerarFieldsetTerciario("Itens cursados nesta instituição", itensOrigemInterna);

                    if (!string.IsNullOrEmpty(itensComponente) || !string.IsNullOrEmpty(itensGrupo))
                    {
                        if (!string.IsNullOrEmpty(itensComponente) && !string.IsNullOrEmpty(itensGrupo))
                            itensComponente += "<hr>";
                        fieldsetDestino += GerarFieldsetTerciario("Itens a serem dispensados", $"{itensComponente}{itensGrupo}");
                    }

                    // Inclui o grupo na nova descrição
                    string modoExibe = GerarItem(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, "Exibir no histórico escolar como", grupo.ModoExibicaoHistoricoEscolar.SMCGetDescription());
                    novaDescricao += GerarFieldset("Grupo", $"{modoExibe}{fieldsetOrigemExterna}{fieldsetOrigemInterna}{fieldsetDestino}");
                }

                // Salva a Descrição
                this.UpdateFields<SolicitacaoDispensa>(new SolicitacaoDispensa { Seq = seqSolicitacaoServico, DescricaoAtualizada = novaDescricao }, x => x.DescricaoAtualizada);

                return novaDescricao;
            }
            return null;
        }

        /// <summary>
        /// Realiza o atendimento de uma solicitação de dispensa (RN_SRC_056)
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser deferida</param>
        public void DeferirSolicitacaoDispensa(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação de dispensa
            var solicitacao = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                Seq = x.Seq,
                SeqTemplateProcessoSgf = x.ConfiguracaoProcesso.Processo.Servico.SeqTemplateProcessoSgf,
                SeqCicloLetivo = x.Destinos.FirstOrDefault().SeqCicloLetivo,
                DescricaoCicloLetivo = x.Destinos.FirstOrDefault().CicloLetivo.Descricao,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                OrigensInternas = x.OrigensInternas,
                OrigensExternas = x.OrigensExternas,
                Destinos = x.Destinos,
                SeqAlunoHistoricoAtual = x.SeqAlunoHistorico.Value,
                OrigensInternasAgrupadas = x.Grupos.SelectMany(g => g.OrigensInternas.Select(o => o.SeqSolicitacaoDispensaOrigemInterna)).ToList(),
                OrigensExternasAgrupadas = x.Grupos.SelectMany(g => g.OrigensExternas.Select(o => o.SeqSolicitacaoDispensaOrigemExterna)).ToList(),
                DestinosAgrupados = x.Grupos.SelectMany(g => g.Destinos.Select(d => d.SeqSolicitacaoDispensaDestino)).ToList(),
                x.NumeroProtocolo
            });

            /* 1. Não é permitido deferir uma solicitação de dispensa se nenhum item tiver sido informado
             * na página "Itens a serem dispensados" (DESTINO).  Em caso de violação, abortar a operação
             * e exibir a mensagem de erro: “Operação não permitida. Para deferir uma solicitação de
             * dispensa, é necessário informar pelo menos um item a ser dispensado".
             */
            if (solicitacao.Destinos.SMCCount() == 0)
                throw new SolicitacaoDispensaDeferidaSemDestinoException();

            // Busca as situações do template de processo de dispensa
            var etapas = SGFHelper.BuscarEtapasSGFCache(solicitacao.SeqTemplateProcessoSgf);
            var seqSituacaoEtapaDeferida = etapas.OrderByDescending(x => x.Ordem).FirstOrDefault().Situacoes.FirstOrDefault(x => x.SituacaoFinalProcesso && x.SituacaoFinalEtapa && x.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso).Seq;

            /*  2.1. Verificar se todos os itens da solicitação de dispensa cadastrados em
             *  UC_SRC_004_12_01 - Solicitação - Itens Cursados para Dispensa (solicitacao_dispensa_origem_interna
             *  e solicitacao_dispensa_origem_externa) estão em algum agrupamento (solicitação_dispensa_grupo).
             *  Em caso de violação, abortar operação e exibir a mensagem de erro: “Operação não permitida.
             *  Existem um ou mais itens cursados que não foram agrupados.”
             */
            foreach (var origem in solicitacao.OrigensExternas)
            {
                if (!solicitacao.OrigensExternasAgrupadas.Contains(origem.Seq))
                    throw new SolicitacaoDispensaOrigemNaoAgrupadaException();
            }
            foreach (var origem in solicitacao.OrigensInternas)
            {
                if (!solicitacao.OrigensInternasAgrupadas.Contains(origem.Seq))
                    throw new SolicitacaoDispensaOrigemNaoAgrupadaException();

                /* 3. Verificar se os itens cursados nesta instituição (origem interna) já foram utilizados em
                 * solicitações de dispensa anteriores PARA O MESMO CURSO OFERTA LOCALIDADE TURNO (mesmo aluno histórico),
                 * também como itens cursados, e com aprovação. Se sim, os itens cursados nesta instituição não podem ser
                 * utilizados novamente. Em caso de violação, abortar a operação e exibir a mensagem:
                 * “Operação não permitida. Existe item cursado nesta solicitação de dispensa que já foi utilizado em
                 * solicitação anterior com aprovação”.
                 */
                // Busca outra solicitação de dispensa com o mesmo histórico escolar da origem que esteja
                // aprovada para o mesmo aluno_historico
                SolicitacaoDispensaFilterSpecification spec = new SolicitacaoDispensaFilterSpecification()
                {
                    SeqAlunoHistorico = solicitacao.SeqAlunoHistoricoAtual,
                    SeqHistoricoEscolar = origem.SeqHistoricoEscolar,
                    SeqSituacaoEtapaAtual = seqSituacaoEtapaDeferida
                };
                if (this.Count(spec) > 0)
                    throw new SolicitacaoDispensaComOrigemInternaJaDeferidaOutraSolicitacaoException();
            }

            /* 2.2. Verificar se todos os itens da solicitação de dispensa cadastrados em
             * UC_SRC_004_11_01 - Solicitação - Itens a Serem Dispensados (solicitacao_dispensa_destino)
             * estão em algum agrupamento (solicitação_dispensa_grupo). Em caso de violação, abortar operação
             * e exibir a mensagem de erro: “Operação não permitida. Existem um ou mais itens a serem dispensados
             * que não foram agrupados.”
             */
            foreach (var destino in solicitacao.Destinos)
            {
                if (!solicitacao.DestinosAgrupados.Contains(destino.Seq))
                    throw new SolicitacaoDispensaDestinoNaoAgrupadoException();
            }

            /* 4. Verificar se em "Itens a serem dispensados" foram selecionados componentes que estão em histórico escolar
             * com situação aprovado ou dispensado. Se sim, exibir a mensagem e abortar a operação. Neste caso, nada deve
             * ser feito com a solicitação
             * MENSAGEM:
             * "Operação não permitida. Os seguintes componentes já estão aprovados ou dispensados: [Lista de componentes com o ciclo letivo separados por vírgula]
             */
            List<HistoricoEscolarAprovadoFiltroVO> listaDestino = solicitacao.Destinos.Where(d => d.SeqComponenteCurricular.HasValue).Select(d => new HistoricoEscolarAprovadoFiltroVO()
            {
                SeqComponenteCurricular = d.SeqComponenteCurricular,
                SeqComponenteCurricularAssunto = d.SeqComponenteCurricularAssunto
            }).ToList();
            if (listaDestino.Count > 0)
            {
                string listaAprovadoDispensado = HistoricoEscolarDomainService.VerificarHistoricoComponentesAprovadosDispensados(solicitacao.SeqPessoaAtuacao, listaDestino, null);
                if (!string.IsNullOrEmpty(listaAprovadoDispensado))
                {
                    throw new SolicitacaoDispensaComponenteCurricularAprovadoException(listaAprovadoDispensado);
                }
            }

            /*
             * 5. Verificar se em "Itens a serem dispensados" foram selecionados componentes+assunto que estão
             * em algum plano de estudo (ind_atual = 1) do aluno sem histórioco escolar para esse componente,
             * assunto (se houver) e ciclo letivo. Se sim, exibir a mensagem de confirmação:
             * "Os seguintes componentes estão no plano de estudo do aluno: [Lista de componentes+assunto de componente
             * com o ciclo letivo separados por vírgula]. O deferimento da dispensa fará uma alteração de plano
             * removendo este item. Deseja prosseguir com o deferimento?"
             * Se usuário clicar em não, abortar a operação.
             * Se usuário clicar em sim, prosseguir e aplicar a regra RN_APR_047 - Alteração de plano com matrícula em outro ciclo letivo
             * ASSERT REALIZADO NO CONTROLLER
             */
            var componentePlanoSemHistorico = SolicitacaoServicoDomainService.VerificarTurmasPlanoEstudoSemHistorico(solicitacao.SeqPessoaAtuacao, listaDestino);
            if (componentePlanoSemHistorico != null && componentePlanoSemHistorico.Count > 0)
            {
                // Chama método que realiza a regra RN_APR_047
                var justificativa = string.Format("Removido itens dispensados pela solicitação: {0}", solicitacao.NumeroProtocolo);
                var situacaoHistorico = "Dispensa";
                var lista = componentePlanoSemHistorico.Select(i => new PlanoEstudoAlterarVO() { SeqPlanoEstudo = i.SeqPlanoEstudo, SeqPlanoEstudoItemRemover = i.SeqPlanoEstudoItem }).ToList();
                PlanoEstudoDomainService.RemoverItensPlanoEstudoAtual(lista, seqSolicitacaoServico, justificativa, solicitacao.DescricaoCicloLetivo, situacaoHistorico);

                // Marca no destino da dispensa que houve exclusão do item no plano
                solicitacao.Destinos.Where(w => componentePlanoSemHistorico.Any(s => s.SeqComponenteCurricular == w.SeqComponenteCurricular && s.SeqComponenteCurricularAssunto == w.SeqComponenteCurricularAssunto)).SMCForEach(f =>
                {
                    this.SolicitacaoDispensaDestinoDomainService.UpdateFields(new SolicitacaoDispensaDestino { Seq = f.Seq, ItemExcluidoPlanoEstudo = true }, x => x.ItemExcluidoPlanoEstudo);
                });
            }

            // 6. Verificar se o aluno possui alguma solicitação ativa conforme RN_APR_048 - Atualização de solicitações ativas
            SolicitacaoServicoDomainService.VerificarSolicitacaoAlteracaoPlanoEmAberto(solicitacao.SeqPessoaAtuacao, listaDestino);

            // 7. Salvar no histórico escolar do aluno-histórico em questão, para cada item da solicitação da dispensa
            HistoricoEscolarDomainService.SalvarHistoricoDispensaIndividual(seqSolicitacaoServico);

            // 8. Salva a descrição atualizada da solicitação
            AtualizarDescricao(seqSolicitacaoServico, false);
        }

        /// <summary>
        /// Realiza a reabertura de uma solicitação de dispensa (RN_SRC_057)
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser reaberta</param>
        public void ReabrirSolicitacao(long seqSolicitacaoServico)
        {
            // Exclui o histórico escolar criado pela solicitação
            HistoricoEscolarDomainService.ExcluirHistoricoEscolarPorSolicitacao(seqSolicitacaoServico);

            // Limpa a descrição atualizada da solicitação de dispensa
            var solicitacaoDispensa = new SolicitacaoDispensa()
            {
                Seq = seqSolicitacaoServico,
                DescricaoAtualizada = null
            };
            this.UpdateFields<SolicitacaoDispensa>(solicitacaoDispensa, x => x.DescricaoAtualizada);
        }

        public AtendimentoDispensaAgrupamentoVO BuscarDadosAgrupamentoAtendimentoDispensa(long seqSolicitacaoServico)
        {
            var seqNivelEnsino = this.SearchProjectionByKey(seqSolicitacaoServico, x => x.AlunoHistorico.SeqNivelEnsino);

            var spec = new SolicitacaoDispensaGrupoFilterSpecification { SeqSolicitacaoDispensa = seqSolicitacaoServico };
            var dadosGrupos = SolicitacaoDispensaGrupoDomainService.SearchProjectionBySpecification(spec, x => new
            {
                x.Seq,
                x.SeqDispensa,
                x.ModoExibicaoHistoricoEscolar,
                x.SeqSolicitacaoDispensa,
                OrigensInternas = x.OrigensInternas.Select(oi => new
                {
                    Codigo = oi.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricular.Codigo,
                    oi.Seq,
                    oi.SeqSolicitacaoDispensaOrigemInterna,
                    Descricao = oi.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricular.Descricao,
                    DescricaoAssunto = oi.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricularAssunto.Descricao,
                    CargaHoraria = oi.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricular.CargaHoraria,
                    Credito = oi.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricular.Credito,
                    SeqTipoComponenteCurricular = (long?)oi.SolicitacaoDispensaOrigemInterna.HistoricoEscolar.ComponenteCurricular.SeqTipoComponenteCurricular
                }),
                OrigensExternas = x.OrigensExternas.Select(oe => new
                {
                    oe.Seq,
                    oe.SeqSolicitacaoDispensaOrigemExterna,
                    Descricao = oe.SolicitacaoDispensaOrigemExterna.Dispensa,
                    CargaHoraria = oe.SolicitacaoDispensaOrigemExterna.CargaHoraria,
                    Credito = oe.SolicitacaoDispensaOrigemExterna.Credito
                }),
                Destino = x.Destinos.Select(d => new
                {
                    d.Seq,
                    d.SeqSolicitacaoDispensaDestino,
                    SeqCurriculoCursoOfertaGrupo = (long?)d.SolicitacaoDispensaDestino.SeqCurriculoCursoOfertaGrupo,
                    DescricaoComponente = d.SolicitacaoDispensaDestino.ComponenteCurricular.Descricao,
                    CodigoComponente = d.SolicitacaoDispensaDestino.ComponenteCurricular.Codigo,
                    SeqTipoComponenteCurricular = (long?)d.SolicitacaoDispensaDestino.ComponenteCurricular.SeqTipoComponenteCurricular,
                    DescricaoAssuntoComponenteCurricular = d.SolicitacaoDispensaDestino.ComponenteCurricularAssunto.Descricao,
                    CargaHorariaComponenteCurricular = d.SolicitacaoDispensaDestino.ComponenteCurricular.CargaHoraria,
                    CreditoComponenteCurricular = d.SolicitacaoDispensaDestino.ComponenteCurricular.Credito,
                    DescricaoGrupo = d.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.Descricao,
                    DescricaoTipoConfiguracaoGrupo = d.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                    FormatoConfiguracaoGrupo = d.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                    QuantidadeHoraRelogio = d.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraRelogio,
                    QuantidadeHoraAula = d.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraAula,
                    QuantidadeItens = d.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeItens,
                    QuantidadeCreditos = d.SolicitacaoDispensaDestino.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeCreditos,
                    DescricaoAssunto = d.SolicitacaoDispensaDestino.ComponenteCurricularAssunto.Descricao,
                    QuantidadeDispensaGrupo = d.SolicitacaoDispensaDestino.QuantidadeDispensaGrupo
                })
            }).ToList();

            Func<string, long?, string, string, string, string, FormatoConfiguracaoGrupo?, short?, short?, short?, short?, short?, long?, string> descricaoDestino = (string codigoComponente, long? seqCurriculoCursoOfertaGrupo, string descricaoComponente, string descricaoGrupo, string descricaoAssunto, string descricaoTipoConfiguracao, FormatoConfiguracaoGrupo? formatoConfiguracao, short? cargaHorariaHoras, short? cargaHorariaHorasAula, short? creditos, short? quantidadeItens, short? quantidadeDispensaGrupo, long? seqTipoComponenteCurricular) =>
            {
                var descricao = string.Empty;

                if (seqCurriculoCursoOfertaGrupo.HasValue)
                {
                    descricao = GrupoCurricularDomainService.GerarDescricaoGrupoCurricular(descricaoGrupo,
                                                                descricaoTipoConfiguracao,
                                                                formatoConfiguracao,
                                                                cargaHorariaHoras,
                                                                cargaHorariaHorasAula,
                                                                creditos,
                                                                quantidadeItens);
                    if (quantidadeDispensaGrupo.HasValue)
                        descricao += $" - Total a ser dispensado: {quantidadeDispensaGrupo}";
                }
                else
                {
                    var specConfig = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                    {
                        SeqNivelEnsino = seqNivelEnsino,
                        SeqTipoComponenteCurricular = seqTipoComponenteCurricular
                    };
                    var config = InstituicaoNivelTipoComponenteCurricularDomainService.SearchByKey(specConfig);

                    descricao = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(codigoComponente, descricaoComponente, creditos, cargaHorariaHoras ?? cargaHorariaHorasAula, config.FormatoCargaHoraria);
                    descricao += (string.IsNullOrEmpty(descricaoAssunto) ? string.Empty : $" ({descricaoAssunto})");
                }

                return descricao;
            };

            var dadosParsed = dadosGrupos.Select(d => new SolicitacaoDispensaGrupoVO
            {
                Seq = d.Seq,
                SeqDispensa = d.SeqDispensa,
                SeqSolicitacaoDispensa = d.SeqSolicitacaoDispensa,
                ModoExibicaoHistoricoEscolar = d.ModoExibicaoHistoricoEscolar,
                OrigensInternas = d.OrigensInternas.Select(o => new SolicitacaoDispensaGrupoOrigemInternaVO
                {
                    Seq = o.Seq,
                    SeqSolicitacaoDispensaOrigemInterna = o.SeqSolicitacaoDispensaOrigemInterna,
                    Descricao = descricaoDestino(o.Codigo, null, o.Descricao, null, o.DescricaoAssunto, null, null, null, o.CargaHoraria, o.Credito, null, null, o.SeqTipoComponenteCurricular), // o.Descricao + (string.IsNullOrEmpty(o.DescricaoAssunto) ? string.Empty : $" ({o.DescricaoAssunto})"),
                }).ToList(),
                OrigensExternas = d.OrigensExternas.Select(o =>
                {
                    var desc = o.Descricao;
                    if (o.CargaHoraria.HasValue && o.CargaHoraria.Value > 0)
                        desc += $" - {o.CargaHoraria} Horas";

                    if (o.Credito.HasValue && o.Credito.Value > 0)
                        desc += $" - {o.Credito} Créditos";

                    return new SolicitacaoDispensaGrupoOrigemExternaVO
                    {
                        Seq = o.Seq,
                        SeqSolicitacaoDispensaOrigemExterna = o.SeqSolicitacaoDispensaOrigemExterna,
                        Descricao = desc
                    };
                }).ToList(),
                Destinos = d.Destino.Select(o => new SolicitacaoDispensaGrupoDestinoVO
                {
                    Seq = o.Seq,
                    SeqSolicitacaoDispensaGrupo = o.SeqSolicitacaoDispensaDestino,
                    Descricao = (o.SeqCurriculoCursoOfertaGrupo.HasValue ? descricaoDestino(null, o.SeqCurriculoCursoOfertaGrupo, null, o.DescricaoGrupo, null, o.DescricaoTipoConfiguracaoGrupo, o.FormatoConfiguracaoGrupo, o.QuantidadeHoraRelogio, o.QuantidadeHoraAula, o.QuantidadeCreditos, o.QuantidadeItens, o.QuantidadeDispensaGrupo, null) :
                                                                           descricaoDestino(o.CodigoComponente, null, o.DescricaoComponente, null, o.DescricaoAssunto, null, null, null, o.CargaHorariaComponenteCurricular, o.CreditoComponenteCurricular, null, null, o.SeqTipoComponenteCurricular))
                }).ToList()
            }).ToList();

            var datasources = BuscarItensAgrupamentoAtendimentoDispensa(seqSolicitacaoServico, null);

            return new AtendimentoDispensaAgrupamentoVO
            {
                SeqSolicitacaoServico = seqSolicitacaoServico,
                Grupos = dadosParsed,
                ItensOrigensInternas = datasources.ItensOrigensInternas,
                ItensOrigensExternas = datasources.ItensOrigensExternas,
                ItensDestinos = datasources.ItensDestinos
            };
        }

        public AtendimentoDispensaAgrupamentoItensVO BuscarItensAgrupamentoAtendimentoDispensa(long seqSolicitacaoServico, long? seqGrupo)
        {
            // Recupera o sequencial do nível de ensino
            var seqNivelEnsino = this.SearchProjectionByKey(seqSolicitacaoServico, x => x.AlunoHistorico.SeqNivelEnsino);

            // Carrega os datasources dos itens
            var datasources = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                ItensOrigensInternas = x.OrigensInternas.Select(i => new
                {
                    Codigo = i.HistoricoEscolar.ComponenteCurricular.Codigo,
                    Seq = i.Seq,
                    Descricao = i.HistoricoEscolar.ComponenteCurricular.Descricao,
                    DescricaoAssunto = i.HistoricoEscolar.ComponenteCurricularAssunto.Descricao,
                    CargaHoraria = i.HistoricoEscolar.ComponenteCurricular.CargaHoraria,
                    Credito = i.HistoricoEscolar.ComponenteCurricular.Credito,
                    SeqTipoComponenteCurricular = (long?)i.HistoricoEscolar.ComponenteCurricular.SeqTipoComponenteCurricular
                }).ToList(),
                ItensOrigensExternas = x.OrigensExternas.Select(i => new
                {
                    Seq = i.Seq,
                    Descricao = i.Dispensa,
                    CargaHoraria = i.CargaHoraria,
                    Credito = i.Credito
                }).ToList(),
                ItensDestinos = x.Destinos.Select(i => new
                {
                    CodigoComponente = i.ComponenteCurricular.Codigo,
                    SeqTipoComponenteCurricular = (long?)i.ComponenteCurricular.SeqTipoComponenteCurricular,
                    DescricaoAssuntoComponenteCurricular = i.ComponenteCurricularAssunto.Descricao,
                    CargaHorariaComponenteCurricular = i.ComponenteCurricular.CargaHoraria,
                    CreditoComponenteCurricular = i.ComponenteCurricular.Credito,

                    Seq = i.Seq,
                    Descricao = i.ComponenteCurricular.Descricao,
                    SeqCurriculoCursoOfertaGrupo = i.SeqCurriculoCursoOfertaGrupo,
                    DescricaoGrupo = i.CurriculoCursoOfertaGrupo.GrupoCurricular.Descricao,
                    DescricaoTipoConfiguracaoGrupo = i.CurriculoCursoOfertaGrupo.GrupoCurricular.TipoConfiguracaoGrupoCurricular.Descricao,
                    FormatoConfiguracaoGrupo = i.CurriculoCursoOfertaGrupo.GrupoCurricular.FormatoConfiguracaoGrupo,
                    QuantidadeHoraRelogio = i.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraRelogio,
                    QuantidadeHoraAula = i.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeHoraAula,
                    QuantidadeItens = i.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeItens,
                    QuantidadeCreditos = i.CurriculoCursoOfertaGrupo.GrupoCurricular.QuantidadeCreditos,
                    DescricaoAssunto = i.ComponenteCurricularAssunto.Descricao,
                    QuantidadeDispensaGrupo = i.QuantidadeDispensaGrupo
                }).ToList(),
                SelecionadosOutrosGrupos = x.Grupos.Where(g => g.Seq != seqGrupo).Select(g => new
                {
                    SeqGrupo = g.Seq,
                    SeqsOrigensExternas = g.OrigensExternas.Select(oe => oe.SeqSolicitacaoDispensaOrigemExterna),
                    SeqsOrigensInternas = g.OrigensInternas.Select(oe => oe.SeqSolicitacaoDispensaOrigemInterna),
                    SeqsDestinos = g.Destinos.Select(d => d.SeqSolicitacaoDispensaDestino)
                })
            });

            // Cria o retorno
            var ret = new AtendimentoDispensaAgrupamentoItensVO
            {
                ItensDestinos = new List<SMCDatasourceItem>(),
                ItensOrigensExternas = new List<SMCDatasourceItem>(),
                ItensOrigensInternas = new List<SMCDatasourceItem>()
            };

            // Adiciona as origens externas
            foreach (var item in datasources.ItensOrigensExternas)
            {
                // Não exibe os itens ja selecionados em outros grupos
                if (datasources.SelecionadosOutrosGrupos.Any(g => g.SeqsOrigensExternas.Contains(item.Seq)))
                    continue;

                string desc = item.Descricao;
                if (item.CargaHoraria.HasValue && item.CargaHoraria.Value > 0)
                    desc += $" - {item.CargaHoraria} Horas";
                if (item.Credito.HasValue && item.Credito.Value > 0)
                    desc += $" - {item.Credito} Créditos";

                ret.ItensOrigensExternas.Add(new SMCDatasourceItem
                {
                    Seq = item.Seq,
                    Descricao = desc
                });
            }

            // Adiciona as origens internas
            /*Se o item cursado for de origem interna, exibir RN_CUR_040 - Exibição descrição componente curricular + '(' + <Descrição do componente curricular assunto, quando houver> + '')'.*/
            foreach (var item in datasources.ItensOrigensInternas)
            {
                // Não exibe os itens ja selecionados em outros grupos
                if (datasources.SelecionadosOutrosGrupos.Any(g => g.SeqsOrigensInternas.Contains(item.Seq)))
                    continue;

                // Carrega a configuração para este componente
                var specConfig = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                {
                    SeqNivelEnsino = seqNivelEnsino,
                    SeqTipoComponenteCurricular = item.SeqTipoComponenteCurricular
                };
                var config = InstituicaoNivelTipoComponenteCurricularDomainService.SearchByKey(specConfig);

                var descricao = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(item.Codigo, item.Descricao, item.Credito, item.CargaHoraria, config.FormatoCargaHoraria);
                descricao += (string.IsNullOrEmpty(item.DescricaoAssunto) ? string.Empty : $" ({item.DescricaoAssunto})");

                ret.ItensOrigensInternas.Add(new SMCDatasourceItem
                {
                    Seq = item.Seq,
                    Descricao = descricao
                });
            }
            ret.ItensOrigensInternas = ret.ItensOrigensInternas.OrderBy(i => i.Descricao).ToList();

            // Adiciona os destinos
            /*Se o item a ser dispensado for componente, exibir conforme RN_CUR_040 - Exibição descrição componente curricular + '(' + <Descrição do componente curricular assunto, quando houver> + ')'.
             *Se o item a ser dispensado for grupo curricular, exibir RN_CUR_045 - Exibição descrição grupo curricular '+' Total a ser dispensado: <qtd_dispensa_grupo>*/
            foreach (var item in datasources.ItensDestinos)
            {
                // Não exibe os itens ja selecionados em outros grupos
                if (datasources.SelecionadosOutrosGrupos.Any(g => g.SeqsDestinos.Contains(item.Seq)))
                    continue;

                var descricao = string.Empty;

                if (item.SeqCurriculoCursoOfertaGrupo.HasValue)
                {
                    descricao = GrupoCurricularDomainService.GerarDescricaoGrupoCurricular(item.DescricaoGrupo,
                                                                item.DescricaoTipoConfiguracaoGrupo,
                                                                item.FormatoConfiguracaoGrupo,
                                                                item.QuantidadeHoraRelogio,
                                                                item.QuantidadeHoraAula,
                                                                item.QuantidadeCreditos,
                                                              item.QuantidadeItens);
                    if (item.QuantidadeDispensaGrupo.HasValue)
                        descricao += $" - Total a ser dispensado: {item.QuantidadeDispensaGrupo}";
                }
                else
                {
                    // Carrega a configuração para este componente
                    var specConfig = new InstituicaoNivelTipoComponenteCurricularFilterSpecification()
                    {
                        SeqNivelEnsino = seqNivelEnsino,
                        SeqTipoComponenteCurricular = item.SeqTipoComponenteCurricular
                    };
                    var config = InstituicaoNivelTipoComponenteCurricularDomainService.SearchByKey(specConfig);

                    descricao = ComponenteCurricularDomainService.GerarDescricaoComponenteCurricular(item.CodigoComponente, item.Descricao, item.CreditoComponenteCurricular, item.CargaHorariaComponenteCurricular, config.FormatoCargaHoraria);
                    descricao += (string.IsNullOrEmpty(item.DescricaoAssunto) ? string.Empty : $" ({item.DescricaoAssunto})");
                    //descricao = item.Descricao + (string.IsNullOrEmpty(item.DescricaoAssunto) ? string.Empty : $" ({item.DescricaoAssunto})");
                }

                ret.ItensDestinos.Add(new SMCDatasourceItem
                {
                    Seq = item.Seq,
                    Descricao = descricao
                });
            }
            ret.ItensDestinos = ret.ItensDestinos.OrderBy(i => i.Descricao).ToList();

            return ret;
        }

        /// <summary>
        /// Verifica se é uma solicitação de dispensa pelo token e se na dispensa destino possui a flag item_excluido_plano
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        /// <returns></returns>
        public bool VerificarReabrirDispensaItemPlano(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação para reabertura
            var spec = new SMCSeqSpecification<SolicitacaoDispensa>(seqSolicitacaoServico);
            var dadosSolicitacao = this.SearchProjectionByKey(spec, s => new
            {
                TokenServico = s.ConfiguracaoProcesso.Processo.Servico.Token,
                ExiteItemPlano = s.Destinos.Any(a => a.ItemExcluidoPlanoEstudo.HasValue && a.ItemExcluidoPlanoEstudo.Value == true)
            });

            if (dadosSolicitacao != null && dadosSolicitacao.TokenServico == TOKEN_SERVICO.DISPENSA_INDIVIDUAL && dadosSolicitacao.ExiteItemPlano)
            {
                return true;
            }

            return false;
        }
    }
}