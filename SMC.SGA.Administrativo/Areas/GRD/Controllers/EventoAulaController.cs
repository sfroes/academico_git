using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.GRD.Constants;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.Common.Areas.GRD.Exceptions;
using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Academico.ServiceContract.Areas.DCT.Interfaces;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Calendarios.Common.Areas.CLD.Enums;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Calendarios.ServiceContract.Areas.ESF.Data;
using SMC.Calendarios.ServiceContract.Areas.ESF.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Security.Util;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.IntegracaoAcademico.ServiceContract.Areas.ACA.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.ACA.Interfaces;
using SMC.SGA.Administrativo.Areas.GRD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.SessionState;

namespace SMC.SGA.Administrativo.Areas.GRD.Controllers
{
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class EventoAulaController : SMCControllerBase
    {
        #region Services

        private ITurmaService TurmaService => Create<ITurmaService>();

        private IDivisaoTurmaService DivisaoTurmaService => Create<IDivisaoTurmaService>();

        private IEventoAulaService EventoAulaService => Create<IEventoAulaService>();

        private ITipoEventoService TipoEventoService => Create<ITipoEventoService>();

        private IHorarioService HorarioService => Create<IHorarioService>();

        private ITabelaHorarioService TabelaHorarioService => Create<ITabelaHorarioService>();

        private IColaboradorService ColaboradorService => Create<IColaboradorService>();

        private IAcademicoService AcademicoService => Create<IAcademicoService>();

        private ILocalSEFService LocalSEFService => Create<ILocalSEFService>();

        private IAgendaService AgendaService => Create<IAgendaService>();
        private ITurmaColaboradorService TurmaColaboradorService => Create<ITurmaColaboradorService>();

        #endregion

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarEventosTurma(SMCEncryptedLong seqTurma)
        {
            try
            {
                EventoAulaTurmaData result = this.EventoAulaService.BuscarEventosTurma(seqTurma);

                var retorno = new
                {
                    EventoAulaTurmaCabecalho = new
                    {
                        SeqTurma = SMCEncryptedLong.GetStringValue(seqTurma.Value),
                        result.EventoAulaTurmaCabecalho.CicloLetivoInicio,
                        result.EventoAulaTurmaCabecalho.CodigoFormatado,
                        result.EventoAulaTurmaCabecalho.DescricaoConfiguracaoComponente,
                        FimPeriodoLetivo = result.EventoAulaTurmaCabecalho.FimPeriodoLetivo.ToShortDateString(),
                        InicioPeriodoLetivo = result.EventoAulaTurmaCabecalho.InicioPeriodoLetivo.ToShortDateString(),
                        SeqCicloLetivoInicio = SMCEncryptedLong.GetStringValue(result.EventoAulaTurmaCabecalho.SeqCicloLetivoInicio),
                        result.EventoAulaTurmaCabecalho.SomenteLeitura,
                        result.EventoAulaTurmaCabecalho.MensagemFalha,
                        result.EventoAulaTurmaCabecalho.CodigoUnidadeSeo,
                        result.EventoAulaTurmaCabecalho.SeqAgendaTurma,
                        result.EventoAulaTurmaCabecalho.SeqCursoOfertaLocalidade
                    },
                    result.PermiteAlterarDataAgendamentoAula,
                    EventoAulaDivisoesTurma = result.EventoAulaDivisoesTurma.Select(s => new
                    {
                        s.AulaSabado,
                        s.CargaHorariaGrade,
                        CargaHorariaLancada = s.EventoAulas.Count(),
                        s.DescricaoDivisaoFormatada,
                        s.DescricaoLocalidade,
                        s.TemHistoricoEscolar,
                        Compartilhamentos = s.Compartilhamentos.Select(sc => new
                        {
                            key = SMCEncryptedLong.GetStringValue(sc.Seq),
                            value = sc.Descricao
                        }).ToList(),
                        EventoAulas = s.EventoAulas.Select(se => new
                        {
                            se.CodigoRecorrencia,
                            se.Data,
                            se.DiaSemana,
                            se.DiaSemanaDescricao,
                            se.DiaSemanaFormatada,
                            se.HoraFim,
                            se.HoraInicio,
                            se.Local,
                            CodigoLocalSEF = se.CodigoLocalSEF?.ToString(),
                            Seq = SMCEncryptedLong.GetStringValue(se.Seq),
                            SeqDivisaoTurma = SMCEncryptedLong.GetStringValue(se.SeqDivisaoTurma),
                            se.SeqHorarioAgd,
                            se.SeqEventoAgd,
                            se.Turno,
                            se.DescricaoColaboradores,
                            SituacaoApuracaoFrequencia = se.SituacaoApuracaoFrequencia.SMCGetDescription(),
                            se.Feriado,
                            Colaboradores = se.Colaboradores.Select(sc => new
                            {
                                Seq = SMCEncryptedLong.GetStringValue(sc.Seq),
                                SeqColaborador = SMCEncryptedLong.GetStringValue(sc.SeqColaborador),
                                SeqColaboradorSubstituto = SMCEncryptedLong.GetStringValue(sc.SeqColaboradorSubstituto),
                                sc.DescricaoFormatada
                            }).ToList()
                        }).ToList(),
                        FimPeriodoLetivo = s.FimPeriodoLetivo?.ToShortDateString(),
                        s.GrupoFormatado,
                        InicioPeriodoLetivo = s.InicioPeriodoLetivo?.ToShortDateString(),
                        s.Numero,
                        s.NumeroGrupo,
                        Seq = SMCEncryptedLong.GetStringValue(s.Seq),
                        SeqCursoOfertaLocalidadeTurno = SMCEncryptedLong.GetStringValue(s.SeqCursoOfertaLocalidadeTurno),
                        TipoDistribuicaoAula = s.TipoDistribuicaoAula.ToString(),
                        s.TipoDivisaoDescricao,
                        TipoPulaFeriado = SMCEnumHelper.GetDescription(s.TipoPulaFeriado ?? TipoPulaFeriado.Nenhum),
                        s.TurmaCodigo,
                        s.TurmaNumero,
                        s.QuantidadeSemanas
                    }).ToList()
                };

                return SMCJsonResultAngular(retorno);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarTabelaHorarioAGD(SMCEncryptedLong seqAgendaTurma, DateTime dataInicioPeriodoLetivo, DateTime dataFimPeriodoLetivo)
        {
            try
            {
                if (seqAgendaTurma == null)
                {
                    return null;
                }

                TabelaHorarioData result = this.TabelaHorarioService.BuscarTabelaHorarioAgendaTurma(new TabelaHorarioAgendaTurmaFiltroData()
                {
                    SeqAgenda = seqAgendaTurma,
                    DataInicioPeriodoLetivo = dataInicioPeriodoLetivo,
                    DataFimPeriodoLetivo = dataFimPeriodoLetivo
                });

                var retorno = new
                {
                    result.Seq,
                    result.SeqTipoCalendario,
                    result.Ativo,
                    result.Descricao,
                    result.Padrao,
                    horarios = result.Horarios.Select(s => new
                    {
                        seq = s.Seq,
                        seqTabelaHorario = s.SeqTabelaHorario,
                        seqDiaSemana = s.DiaSemana,
                        seqTurno = s.Turno,
                        diaSemana = SMCEnumHelper.GetDescription(s.DiaSemana),
                        horaInicio = s.HoraInicio.ToString(@"hh\:mm"),
                        horaFim = s.HoraFim.ToString(@"hh\:mm")

                    }).OrderBy(o => o.seqDiaSemana).ThenBy(t => t.horaInicio).ToList()
                };

                return SMCJsonResultAngular(retorno);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarDiaSemanaAGD()
        {
            List<object> retorno = new List<object>();

            foreach (var item in Enum.GetValues(typeof(TipoDiaSemana)).OfType<TipoDiaSemana>())
            {
                if (item != TipoDiaSemana.Nenhum && item != TipoDiaSemana.Domingo)
                {
                    if (item != TipoDiaSemana.Sabado)
                    {
                        retorno.Add(new { value = Convert.ToInt32(item), label = SMCEnumHelper.GetDescription(item) });
                    }
                    else
                    {
                        retorno.Add(new { value = Convert.ToInt32(item), label = SMCEnumHelper.GetDescription(item), disabled = true });

                    }
                }
            }

            return SMCJsonResultAngular(retorno);
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarTurnoAGD()
        {
            var retorno = new List<object>();

            foreach (var item in Enum.GetValues(typeof(Turno)).OfType<Turno>())
            {
                if (item != Turno.Nenhum)
                {
                    retorno.Add(new { key = Convert.ToInt32(item), value = SMCEnumHelper.GetDescription(item) });
                }
            }

            return SMCJsonResultAngular(retorno);
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarTipoRecorrenciaAGD()
        {
            var retorno = new List<object>();

            foreach (var item in Enum.GetValues(typeof(TipoPadraoRecorrencia)).OfType<TipoPadraoRecorrencia>())
            {
                if (item == TipoPadraoRecorrencia.Semanal || item == TipoPadraoRecorrencia.Mensal)
                {
                    retorno.Add(new { key = Convert.ToInt32(item), value = SMCEnumHelper.GetDescription(item) });
                }
            }

            return SMCJsonResultAngular(retorno);
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarTipoInicioRecorrenciaAGD()
        {
            return SMCDataSourceAngular<TipoInicioRecorrencia>(keyValue: true);
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarLocaisAGD(long seqAgendaTurma, int codigoUnidadeSeo)
        {
            try
            {
                bool turmaIntegradaSEF = AgendaService.VerificarIntegracaoSEF(seqAgendaTurma, TOKEN_TIPO_EVENTO_SEF.AULA);

                LocalSEFData[] locais = LocalSEFService.BuscarLocaisSEFLookup(new LocalSEFFiltroData() { CodigoUnidade = codigoUnidadeSeo });

                if (!locais.SMCAny())
                {
                    throw new EventoAulaTurmaSemTabelaHorarioException();
                }

                var retorno = new
                {
                    turmaIntegradaSEF,
                    locais = locais.Select(s => new
                    {
                        s.Seq,
                        s.SeqPai,
                        s.Descricao,
                        s.ItemFolha,
                        Selecionavel = s.ItemFolha
                    })
                };

                return SMCJsonResultAngular(retorno);
            }
            catch (Exception ex)
            {

                return SMCHandleErrorAngular(ex);
            }
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarLocalAGD(long codigoLocalSEF)
        {
            try
            {
                LocalSEFData local = LocalSEFService.BuscarLocalSEFLookup(codigoLocalSEF);
                return SMCJsonResultAngular(local.Descricao);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarColaboradores(long seqTurma, long seqCursoOfertaLocalidade)
        {
            List<ColaboradorGradeData> result = ColaboradorService.BuscarColaboradoresAptoLecionarGrade(new ColaboradorFiltroData()
            {
                SeqTurma = seqTurma,
                //VinculoAtivo = true, //Conforme demanda 49686 passamos a trazer todos os colaboradores com vinculo no periodo letivo
                AptoLecionarComponenteTurma = true,
                TipoAtividade = TipoAtividadeColaborador.Aula,
                SeqCursoOfertaLocalidade = seqCursoOfertaLocalidade
            }).ToList();

            var retorno = result.Select(s => new
            {
                Seq = SMCEncryptedLong.GetStringValue(s.Seq),
                s.Nome,
                s.NomeFormatado,
                s.Vinculos
            }).ToList();

            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarFeriados(int codigoUnidadeSeo, DateTime dataInicio, DateTime dataFim)
        {
            List<FeriadoData> result = AcademicoService.BuscarFeriados(codigoUnidadeSeo, dataInicio, dataFim);

            return SMCJsonResultAngular(result);
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_02.INCLUIR_EVENTO_AULA)]
        public ActionResult SalvarEventos(List<EventoAulaViewModel> model)
        {
            try
            {
                EventoAulaService.SalvarEventos(model.TransformList<EventoAulaData>());
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
            return SMCJsonResultAngular("");
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_03.ALTERAR_HORARIO)]
        public ActionResult EditarEventos(List<EventoAulaViewModel> model, List<long> seqsEventosExcluir)
        {
            try
            {
                EventoAulaService.EditarEventos(model.TransformList<EventoAulaData>(), seqsEventosExcluir);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
            return SMCJsonResultAngular("");
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_07.ALTERAR_LOCAL)]
        public ActionResult EditarLocalEventos(int? codigoLocalSEF, string local, List<long> seqEventos)
        {
            try
            {
                EventoAulaService.EditarLocalEventos(codigoLocalSEF, local, seqEventos);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
            return SMCJsonResultAngular("");
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_08.ALTERAR_COLABORADOR)]
        public ActionResult EditarColaboradoresEventos(List<EventoAulaColaboradorViewModel> colaboradores, List<long> seqEventos, long seqEventoTemplate, bool somenteColaborador)
        {
            try
            {
                colaboradores = colaboradores ?? new List<EventoAulaColaboradorViewModel>();
                EventoAulaService.EditarColaboradoresEventos(colaboradores.TransformList<EventoAulaColaboradorData>(), seqEventos, seqEventoTemplate, somenteColaborador);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
            return SMCJsonResultAngular("");
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_04.EXCLUIR_EVENTO_AULA)]
        public ActionResult ExcluirEventos(List<long> seqsEventoAula)
        {
            if (seqsEventoAula.SMCAny())
            {
                try
                {
                    EventoAulaService.ExcluirEventos(seqsEventoAula);
                }
                catch (Exception ex)
                {
                    return SMCHandleErrorAngular(ex);
                }
            }
            return SMCJsonResultAngular("");
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_02.INCLUIR_EVENTO_AULA)]
        public ContentResult ValidarColisao(List<EventoAulaValidacaoColisaoColaboradorViewModel> model)
        {
            var result = EventoAulaService.ValidarColisao(model.TransformList<EventoAulaValidacaoColisaoColaboradorData>());
            return SMCJsonResultAngular(result);
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_08.ALTERAR_COLABORADOR)]
        public ActionResult ValidarVinculoColaboradorPeriodo(long seqColaborador, DateTime dataInicio, DateTime dataFim)
        {
            try
            {
                var vinculoValido = EventoAulaService.ValidarVinculoColaboradorPeriodo(seqColaborador, dataInicio, dataFim);
                return SMCJsonResultAngular(vinculoValido);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_08.ALTERAR_COLABORADOR)]
        public ContentResult ValidarColisaoHorarioAluno(List<EventoAulaViewModel> model)
        {
            var result = EventoAulaService.ValidarColisaoHorarioAluno(model.TransformList<EventoAulaData>());

            return SMCJsonResultAngular(result);
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_08.ALTERAR_COLABORADOR)]
        public ContentResult ValidarColisaoHorarioSolicitacaoServico(List<EventoAulaViewModel> model)
        {
            var result = EventoAulaService.ValidarColisaoHorarioSolicitacaoServico(model.TransformList<EventoAulaData>());

            return SMCJsonResultAngular(result);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarTokensSeguranca()
        {
            var retorno = new[]
            {
                new {nome = "pequisarEventoAula", permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)},
                new {nome = "incluirEventoAula", permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_02.INCLUIR_EVENTO_AULA)},
                new {nome = "permiteAlterarDataAgendamentoAula", permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_02.PERMITE_ALTERAR_DATA_AGENDAMENTO_AULA)},
                new {nome = "alterarHorario",  permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_03.ALTERAR_HORARIO)},
                new {nome = "alterarColaborador",  permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_08.ALTERAR_COLABORADOR)},
                new {nome = "alterarLocal",  permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_07.ALTERAR_LOCAL)},
                new {nome = "detalheDivisaoTurmaGrade",  permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_03.DETALHE_DIVISAO_TURMA_GRADE) },
                new {nome = "excluirEventoAula",  permitido = SMCSecurityHelper.Authorize(UC_GRD_001_01_04.EXCLUIR_EVENTO_AULA) },
            };

            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ContentResult BuscarDadosCabecalhoAssociarProfessor(long seqTurma)
        {
            TurmaCabecalhoData result = TurmaService.BuscarTurmaCabecalho(seqTurma);
            var dadosColaboradores = TurmaColaboradorService.BuscarTurmaColaborador(seqTurma);

            var retorno = new
            {
                result.CodigoFormatado,
                result.CicloLetivoInicio,
                result.Vagas,
                result.DescricaoTipoTurma,
                SituacaoTurmaAtual = SMCEnumHelper.GetDescription(result.SituacaoTurmaAtual),
                configuacoesComponente = result.TurmaConfiguracoesCabecalho.Select(s => new
                {
                    s.ConfiguracaoPrincipal,
                    s.DescricaoConfiguracaoComponente,
                    s.SeqComponenteCurricular,
                    s.SeqConfiguracaoComponente
                }),
                ColaboradoresTurma = new
                {
                    dadosColaboradores.SeqTurma,
                    colaborador = dadosColaboradores.Colaborador.Select(s => s.Seq)
                }
            };

            return SMCJsonResultAngular(retorno);
        }

        [HttpPost]
        [SMCAuthorize(UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA)]
        public ActionResult AssociarProfessorResponsavel(TurmaColaboradorData turmaColaborador)
        {
            try
            {
                TurmaColaboradorService.SalvarTurmaColaborador(turmaColaborador);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
            return SMCJsonResultAngular("");
        }
    }
}