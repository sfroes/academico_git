using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.Framework.Util;
using SMC.SGA.Administrativo.Areas.APR.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.APR.Controllers
{
	public class HistoricoEscolarController : SMCDynamicControllerBase
	{
		#region [ Serviços ]

		private IHistoricoEscolarService HistoricoEscolarService { get => Create<IHistoricoEscolarService>(); }

		private IComponenteCurricularService ComponenteCurricularService { get => Create<IComponenteCurricularService>(); }

		private ICurriculoCursoOfertaGrupoService CurriculoCursoOfertaGrupoService { get => Create<ICurriculoCursoOfertaGrupoService>(); }

		private ICriterioAprovacaoService CriterioAprovacaoService { get => Create<ICriterioAprovacaoService>(); }

		private IEscalaApuracaoItemService EscalaApuracaoItemService { get => Create<IEscalaApuracaoItemService>(); }

		private IMatrizCurricularService MatrizCurricularService => Create<IMatrizCurricularService>();

		#endregion [ Serviços ]

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarHistoricoEscolarCabecalho(long seqAluno)
		{
			var model = HistoricoEscolarService.BuscarHistoricoEscolarCabecalho(seqAluno).Transform<HistoricoEscolarCabecalhoViewModel>();
			return PartialView("_Cabecalho", model);
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarComponenteCurricularExigeAssunto(long? seqComponenteCurricular)
		{
			if (!seqComponenteCurricular.HasValue || seqComponenteCurricular == 0)
				return Content(string.Empty);
			return Content(ComponenteCurricularService.BuscarComponenteCurricularSemDependencias(seqComponenteCurricular.Value).ExigeAssuntoComponente ? "True" : "False");
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarComponenteCurricularGestaoExame(long seqComponenteCurricular)
		{
			return Content(ComponenteCurricularService.BuscarComponenteCurricular(seqComponenteCurricular).TipoGestaoDivisoesComponente.All(a => a == TipoGestaoDivisaoComponente.Exame) ? "True" : "False");
		}

		/// <summary>
		/// Verirfica se o componente informado é na matriz do curso do aluno no ciclo letivo informado
		/// </summary>
		/// <param name="seqComponenteCurricular">Sequencial do componetne curricular</param>
		/// <param name="seqAluno">Sequencial do aluno</param>
		/// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
		/// <returns>True ou False conforme a obrigatoriedade do componente na matriz do aluno ou "" caso este não esteja na matriz do aluno contrário.</returns>
		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult VerificarComponenteCurricularOptativoMatriz(long seqComponenteCurricular, long seqAluno, long seqCicloLetivo)
		{
			var configuracaoGrupoComponente = CurriculoCursoOfertaGrupoService
				.BuscarCurriculoCursoOfertaGrupo(new CurriculoCursoOfertaGrupoFiltroData()
				{
					SeqAluno = seqAluno,
					SeqCicloLetivo = seqCicloLetivo,
					SeqComponenteCurricular = seqComponenteCurricular
				});

			if (configuracaoGrupoComponente != null)
			{
				return Content(configuracaoGrupoComponente.Obrigatorio == CurriculoCursoOfertaGrupoComponenteObrigatorio.Optativos ? "True" : "False");
			}
			return Content("");
		}

		/// <summary>
		/// Verifica se o componente informado é opcional na matriz do aluno
		/// </summary>
		/// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
		/// <param name="seqAluno">Sequencial do aluno</param>
		/// <param name="seqCicloLetivo">Sequencial do ciclo letivo</param>
		/// <returns>True apenas se o componente informado for opcional na matriz do aluno, False caso seja obrigatório ou não seja da matriz do aluno</returns>
		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult VerificarComponenteCurricularOptativoAluno(long seqComponenteCurricular, long seqAluno, long seqCicloLetivo)
		{
			var optativoMatriz = (ContentResult)VerificarComponenteCurricularOptativoMatriz(seqComponenteCurricular, seqAluno, seqCicloLetivo);
			return string.IsNullOrEmpty(optativoMatriz.Content) ? Content("False") : optativoMatriz;
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarMatrizCurricular(long seqAluno, long seqCicloLetivo, bool considerarMatriz)
		{
			return Content(MatrizCurricularService.BuscarMatrizCurricularAluno(seqAluno, seqCicloLetivo, considerarMatriz)?.Seq.ToString());
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarCriteriosAprovacao(HistoricoEscolarFiltroViewModel filtro)
		{
			return Json(CriterioAprovacaoService.BuscarCriteriosAprovacaoSelect(filtro.Transform<CriterioAprovacaoFiltroData>()));
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarEscalaApuracaoItemNota(long seqEscalaApuracao, short nota)
		{
			return Json(EscalaApuracaoItemService.BuscarEscalaApuracaoItensSelect(new EscalaApuracaoItemFiltroData() { SeqEscalaApuracao = seqEscalaApuracao, Percentual = nota }));
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarEscalaApuracao(long seqEscalaApuracao)
		{
			return Json(EscalaApuracaoItemService.BuscarEscalaApuracaoItensSelect(new EscalaApuracaoItemFiltroData() { SeqEscalaApuracao = seqEscalaApuracao }));
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult BuscarConfiguracaoNotaFaltaPorCriterioAprovacao(long? seqCriterioAprovacao, long? seqComponenteCurricular)
		{
			if (!seqCriterioAprovacao.HasValue || !seqComponenteCurricular.HasValue)
				return Content(string.Empty);

			var criterio = CriterioAprovacaoService.BuscarCriterioAprovacao(seqCriterioAprovacao.Value);
			var componente = ComponenteCurricularService.BuscarComponenteCurricular(seqComponenteCurricular.Value);
			var apuracaoEscala = !criterio.ApuracaoNota && criterio.SeqEscalaApuracao.HasValue;
			var model = new HistoricoEscolarDynamicModel()
			{
				IndicadorApuracaoNota = criterio.ApuracaoNota,
				NotaMaxima = criterio.NotaMaxima.GetValueOrDefault(),
				IndicadorApuracaoEscala = apuracaoEscala,
				SeqEscalaApuracao = criterio.SeqEscalaApuracao,
				EscalaApuracaoItens = apuracaoEscala ? EscalaApuracaoItemService.BuscarEscalaApuracaoItensSelect(new EscalaApuracaoItemFiltroData() { SeqEscalaApuracao = criterio.SeqEscalaApuracao }) : null,
				IndicadorApuracaoFrequencia = criterio.ApuracaoFrequencia,
				PercentualMinimoFrequencia = criterio.PercentualFrequenciaAprovado,
				PercentualMinimoNota = criterio.PercentualNotaAprovado,
				CargaHoraria = componente.CargaHoraria,
				Credito = componente.Credito,
				TipoArredondamento = criterio.TipoArredondamento,
			};
			return PartialView("_NotaFalta", model);
		}

		[SMCAuthorize(UC_APR_002_08_02.MANTER_HISTORICO_ESCOLAR, UC_APR_002_08_02.PERMITIR_MANTER_TODOS_TIPOS_COMPONENTES)]
		public ActionResult CalcularSituacaoFinal(HistoricoEscolarSituacaoFinalViewModel situacaoFinalViewModel)
		{
			if (situacaoFinalViewModel.IndicadorApuracaoEscala && situacaoFinalViewModel.SeqEscalaApuracaoItem.GetValueOrDefault() == 0 ||
				situacaoFinalViewModel.IndicadorApuracaoFrequencia && !situacaoFinalViewModel.Faltas.HasValue ||
				situacaoFinalViewModel.IndicadorApuracaoNota && !situacaoFinalViewModel.Nota.HasValue)
			{
				return Content("");
			}
			return Content(SMCEnumHelper.GetDescription(HistoricoEscolarService.CalcularSituacaoFinal(situacaoFinalViewModel.Transform<HistoricoEscolarSituacaoFinalData>())));
		}
	}
}