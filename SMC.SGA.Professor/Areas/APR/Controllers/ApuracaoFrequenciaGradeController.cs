using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Professor.Areas.APR.Controllers
{
    public class ApuracaoFrequenciaGradeController : SMCControllerBase
    {
        #region [Servicos]

        private IApuracaoFrequenciaGradeService ApuracaoFrequenciaGradeService => Create<IApuracaoFrequenciaGradeService>();

        #endregion [Servicos]

        [SMCAuthorize(UC_APR_006_02_01.LANÇAMENTO_FREQUENCIA_GRADE)]
        public ActionResult BuscarLancamentoFrequencia(long seqOrigemAvaliacao)
        {
            var data = ApuracaoFrequenciaGradeService.BuscarLancamentoFrequencia(seqOrigemAvaliacao);
            var result = new
            {
                SeqOrigemAvaliacao = SMCEncryptedLong.GetStringValue(data.SeqOrigemAvaliacao),
                data.DataLimite,
                data.QuantidadeDiasPrazoApuracaoFrequencia,
                data.QuantidadeMinutosPrazoAlteracaoFrequencia,
                data.CargaHoraria,
                data.DescricaoOrigemAvaliacao,
                UsuarioAutenticado = SMCContext.User.Identity.Name,
                Aulas = data.Aulas.Select(s => new
                {
                    SeqEventoAula = SMCEncryptedLong.GetStringValue(s.SeqEventoAula),
                    s.DescricaoFormatada,
                    s.Sigla,
                    SituacaoApuracaoFrequencia = s.SituacaoApuracaoFrequencia.SMCGetDescription(),
                    s.Data,
                    HoraInicio = s.HoraInicio.ToString(@"hh\:mm"),
                    HoraFim = s.HoraFim.ToString(@"hh\:mm"),
                    s.DataPrimeiraApuracaoFrequencia,
                    s.DataLimiteApuracaoFrequencia,
                    s.UsuarioPrimeiraApuracaoFrequencia
                }).ToList(),
                Alunos = data.Alunos.Select(s => new
                {
                    SeqAlunoHistoricoCicloLetivo = SMCEncryptedLong.GetStringValue(s.SeqAlunoHistoricoCicloLetivo),
                    NumeroRegistroAcademico = s.NumeroRegistroAcademico.ToString(),
                    s.Nome,
                    s.AlunoFormado,
                    s.AlunoHistoricoEscolar,
                    Apuracoes = s.Apuracoes.Select(sa => new
                    {
                        Seq = SMCEncryptedLong.GetStringValue(sa.Seq),
                        SeqAlunoHistoricoCicloLetivo = SMCEncryptedLong.GetStringValue(sa.SeqAlunoHistoricoCicloLetivo),
                        SeqEventoAula = SMCEncryptedLong.GetStringValue(sa.SeqEventoAula),
                        sa.Observacao,
                        Frequencia = sa.Frequencia?.SMCGetDescription(),
                        sa.DataObservacao,
                        OcorrenciaFrequencia = sa.OcorrenciaFrequencia?.SMCGetDescription(),
                        sa.DescricaoTipoMensagem
                    }).ToList()
                }).ToList()
            };
            return SMCJsonResultAngular(result);
        }

        [SMCAuthorize(UC_APR_006_02_01.LANÇAMENTO_FREQUENCIA_GRADE)]
        public ActionResult BuscarHorarioLimiteTurno(long seqOrigemAvaliacao)
        {
            var horario = ApuracaoFrequenciaGradeService.BuscarHorarioLimiteTurno(seqOrigemAvaliacao);
            return SMCJsonResultAngular(horario?.ToString(@"hh\:mm"));
        }

        [HttpPost]
        [SMCAuthorize(UC_APR_006_02_01.LANÇAMENTO_FREQUENCIA_GRADE)]
        public ActionResult SalvarLancamentoFrequencia(List<ApuracaoFrequenciaGradeData> data)
        {
            try
            {
                ApuracaoFrequenciaGradeService.SalvarLancamentoFrequencia(data);
            }
            catch (Exception ex)
            {
                return SMCHandleErrorAngular(ex);
            }
            return SMCJsonResultAngular("");
        }

        [SMCAuthorize(UC_APR_006_02_01.LANÇAMENTO_FREQUENCIA_GRADE)]
        public ActionResult BuscarDataHoraServidor()
        {
            DateTime dataHora = DateTime.Now;

            return SMCJsonResultAngular(dataHora);
        }
    }
}