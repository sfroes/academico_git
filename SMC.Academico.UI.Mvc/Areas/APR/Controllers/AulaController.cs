using SMC.Academico.Common.Areas.APR.Constants;
using SMC.Academico.Common.Areas.APR.Exceptions.Aula;
using SMC.Academico.ServiceContract.Areas.APR.Data.Aula;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.TUR.Data;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Academico.UI.Mvc.Areas.APR.Models;
using SMC.Academico.UI.Mvc.Areas.APR.Views.Aula.App_LocalResources;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Controllers
{
	public class AulaController : SMCControllerBase
	{
		public IAulaService AulaService => Create<IAulaService>();
		public IDivisaoTurmaService DivisaoTurmaService => Create<IDivisaoTurmaService>();
		public ITurmaService TurmaService => Create<ITurmaService>();

		[SMCAuthorize(UC_APR_006_01_01.PESQUISAR_LANCAMENTO_FREQUENCIA)]
		public ActionResult Index(AulaFiltroViewModel filtro)
		{
			if (filtro.SeqDivisaoTurma <= 0)
				throw new AulaSeqDivisaoTurmaNaoInformadoException();

			// Verifica se o diário da turma está fechado
			var diarioFechado = TurmaService.DiarioTurmaFechado(new TurmaFiltroData { SeqDivisaoTurma = filtro.SeqDivisaoTurma });
			if (diarioFechado.GetValueOrDefault())
				throw new AulaDiarioTurmaFechadoException();

			var dadosDivisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(filtro.SeqDivisaoTurma).FirstOrDefault(d => d.Seq == filtro.SeqDivisaoTurma); ;
			filtro.GeraOrientacao = dadosDivisao.GeraOrientacao;

			var view = GetExternalView(AcademicoExternalViews.AULA_PATH + "Index");
			return View(view, filtro);
		}

		[SMCAuthorize(UC_APR_006_01_01.PESQUISAR_LANCAMENTO_FREQUENCIA)]
		public ActionResult HeaderIndex(AulaFiltroViewModel filter)
		{
			// Recupera o nome da turma
			var dadosDivisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(filter.SeqDivisaoTurma).FirstOrDefault(d => d.Seq == filter.SeqDivisaoTurma);
			filter.DescricaoTurma = dadosDivisao.TurmaDescricaoFormatado;
			filter.DescricaoOfertaTurno = $"{dadosDivisao.NomeCursoOfertaLocalidade} - {dadosDivisao.DescricaoTurno}";
			filter.DescricoesCursoOfertaLocalidadeTurno = dadosDivisao.DescricoesCursoOfertaLocalidadeTurno;

			var view = GetExternalView(AcademicoExternalViews.AULA_PATH + "HeaderIndex");
			return PartialView(view, filter);
		}

		[SMCAuthorize(UC_APR_006_01_01.PESQUISAR_LANCAMENTO_FREQUENCIA)]
		public ActionResult Listar(AulaFiltroViewModel filter)
		{
			var dados = AulaService.BuscarAulasLista(filter.Transform<AulaFiltroData>()).TransformList<AulaListaViewModel>();
			var model = new SMCPagerModel<AulaListaViewModel>(dados, filter.PageSettings, filter);

			ViewBag.SeqDivisaoTurma = filter.SeqDivisaoTurma;
			ViewBag.BackUrl = filter.BackUrl;

			var view = GetExternalView(AcademicoExternalViews.AULA_PATH + "Listar");
			return PartialView(view, model);
		}

		[SMCAuthorize(UC_APR_006_01_02.MANTER_LANCAMENTO_FREQUENCIA)]
		public ActionResult Incluir(AulaFiltroViewModel filtro, List<long> colaboradores)
		{
			var model = new AulaViewModel
			{
				SeqDivisaoTurma = filtro.SeqDivisaoTurma,
				BackUrl = filtro.BackUrl,
				Ofertas = AulaService.BuscarAlunosNovaApuracao(filtro.SeqDivisaoTurma, filtro.AgruparAluosCurso, colaboradores).TransformMasterDetailList<AulaOfertaViewModel>(),
				DataAula = DateTime.Today
			};

			// Recupera o nome da turma
			var dadosDivisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(model.SeqDivisaoTurma).FirstOrDefault(d => d.Seq == model.SeqDivisaoTurma); ;
			model.DescricaoTurma = $"{dadosDivisao.TurmaDescricaoFormatado}";
			model.DescricaoOfertaTurno = $"{dadosDivisao.NomeCursoOfertaLocalidade} - {dadosDivisao.DescricaoTurno}";
			model.DescricoesCursoOfertaLocalidadeTurno = dadosDivisao.DescricoesCursoOfertaLocalidadeTurno;

			var view = GetExternalView(AcademicoExternalViews.AULA_PATH + "Incluir");
			return View(view, model);
		}

		[SMCAuthorize(UC_APR_006_01_02.MANTER_LANCAMENTO_FREQUENCIA)]
		public ActionResult Editar(AulaFiltroViewModel filtro, List<long> colaboradores)
		{
			var model = AulaService.BuscarAula(filtro.Seq.Value, filtro.AgruparAluosCurso, colaboradores).Transform<AulaViewModel>();

			// Recupera o nome da turma
			var dadosDivisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(model.SeqDivisaoTurma).FirstOrDefault(d => d.Seq == model.SeqDivisaoTurma); ;
			model.DescricaoTurma = $"{dadosDivisao.TurmaDescricaoFormatado}";
			model.DescricaoOfertaTurno = $"{dadosDivisao.NomeCursoOfertaLocalidade} - {dadosDivisao.DescricaoTurno}";
			model.DescricoesCursoOfertaLocalidadeTurno = dadosDivisao.DescricoesCursoOfertaLocalidadeTurno;

			model.BackUrl = filtro.BackUrl;

			var view = GetExternalView(AcademicoExternalViews.AULA_PATH + "Editar");
			return View(view, model);
		}

		[SMCAuthorize(UC_APR_006_01_02.MANTER_LANCAMENTO_FREQUENCIA)]
		public ActionResult FitrarAlunos(SMCEncryptedLong seq, SMCEncryptedLong seqDivisaoTurma, string backUrl)
		{
			var dadosDivisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(seqDivisaoTurma, preservarColaboradores: true).FirstOrDefault(d => d.Seq == seqDivisaoTurma);

			var model = new AulaFiltroViewModel()
			{
				AgruparAluosCurso = true,
				BackUrl = backUrl,
				Seq = seq,
				SeqDivisaoTurma = seqDivisaoTurma,
				Colaboradores = dadosDivisao.GeraOrientacao ?
					new SMCPagerModel<ListaFrequenciaColaboradorViewModel>(
						 dadosDivisao.Colaboradores.Select(c =>
						 new ListaFrequenciaColaboradorViewModel
						 {
							 SeqColaborador = c.SeqColaborador,
							 NomeColaborador = c.NomeFormatado
						 })) :
					null
			};

			var view = GetExternalView(AcademicoExternalViews.AULA_PATH + "FiltarAlunos");
			return PartialView(view, model);
		}

		[SMCAuthorize(UC_APR_006_01_02.MANTER_LANCAMENTO_FREQUENCIA)]
		public ActionResult Salvar(AulaViewModel model)
		{
			bool incluindo = false;
			if (model.Seq == 0)
				incluindo = true;

			// Caso tenha algum registro que já tenha histórico escolar, alertar
			var existeHistoricoEscolar = model.Ofertas.Any(a => a.ApuracoesFrequencia.Any(af => af.AlunoComHistorico));

			Assert(model, UIResource.Confirmacao_Aluno_Com_Historico, () => existeHistoricoEscolar);

			AulaService.SalvarAula(model.Transform<AulaData>());

			if (incluindo)
				SetSuccessMessage("Aula incluída com sucesso!", target: SMCMessagePlaceholders.Centro);
			else
				SetSuccessMessage("Aula alterada com sucesso!", target: SMCMessagePlaceholders.Centro);

			return SMCRedirectToAction("Index", null, new { SeqDivisaoTurma = new SMCEncryptedLong(model.SeqDivisaoTurma), BackUrl = model.BackUrl });
		}

		[SMCAuthorize(UC_APR_006_01_02.MANTER_LANCAMENTO_FREQUENCIA)]
		public ActionResult Excluir(SMCEncryptedLong seq, SMCEncryptedLong seqDivisaoTurma, string backUrl)
		{
			AulaService.Excluir(seq);

			SetSuccessMessage("Aula excluída com sucesso!", target: SMCMessagePlaceholders.Centro);

			return SMCRedirectToAction("Index", null, new { SeqDivisaoTurma = new SMCEncryptedLong(seqDivisaoTurma), backUrl });
		}
	}
}