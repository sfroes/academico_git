using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Security.Util;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.APR.Controllers
{
    public class SituacaoAulaLoteController : SMCControllerBase
	{
		#region [Services]

		IDivisaoTurmaService DivisaoTurmaService => Create<IDivisaoTurmaService>();
		IEventoAulaService EventoAulaService => Create<IEventoAulaService>();

		#endregion

		[SMCAuthorize(UC_APR_006_04_01.SITUACAO_AULA_EM_LOTE)]
		public ContentResult BuscarDivisoesTurma(long seqTurma)
        {
            List<DivisaoTurmaDetalhesData> divisoes = DivisaoTurmaService.BuscarDivisaoTurmaLista(seqTurma).ToList();

            var retorno = divisoes.Select(s => new
            {
                Value = s.Seq,
                Label = $"{s.GrupoFormatado} - {s.TipoDivisaoDescricao} - {s.CargaHoraria} Hora(s)"
            });

            return SMCJsonResultAngular(retorno);
        }

		[HttpPost]
        [SMCAllowAnonymous]
        public ContentResult BuscarAulas(EventoAulaFiltroData filtro)
        {
            List<EventoAulaLoteData> retorno = EventoAulaService.BuscarEventosAulaLote(filtro);
			return SMCJsonResultAngular(retorno);
        }

        [SMCAllowAnonymous]
        public ContentResult BuscarTokensSeguranca()
        {
            var retorno = new[]
            {
                new {nome = "situacaoAulaLote", permitido = SMCSecurityHelper.Authorize(UC_APR_006_04_01.SITUACAO_AULA_EM_LOTE)},
            };

            return SMCJsonResultAngular(retorno);
        }

        [SMCAllowAnonymous]
        public ContentResult LiberarEventosAulaApuracao(List<long> seqsEventoAula)
        {
            EventoAulaService.LiberarEventosAulaApuracao(seqsEventoAula);
            return SMCJsonResultAngular("OK");
        }

        [SMCAllowAnonymous]
        public ContentResult LiberarEventosAulaCorrecao(List<long> seqsEventoAula)
        {
            EventoAulaService.LiberarEventosAulaCorrecao(seqsEventoAula);
            return SMCJsonResultAngular("OK");
        }

        [SMCAllowAnonymous]
        public ContentResult AlterarEventosAulasNaoExecutadaOuNaoApurada(List<long> seqsEventoAula, SituacaoApuracaoFrequencia situacaoApuracaoFrequencia)
        {
            EventoAulaService.AlterarEventosAulasNaoExecutadaOuNaoApurada(seqsEventoAula, situacaoApuracaoFrequencia);
            return SMCJsonResultAngular("OK");
        }
    }
}