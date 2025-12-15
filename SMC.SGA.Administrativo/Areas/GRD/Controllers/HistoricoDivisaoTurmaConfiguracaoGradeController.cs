using SMC.Academico.Common.Areas.GRD.Constants;
using SMC.Academico.ServiceContract.Areas.GRD.Data;
using SMC.Academico.ServiceContract.Areas.GRD.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.GRD.Models;
using System;
using System.Web.Mvc;
using SMC.SGA.Administrativo.Areas.GRD.Views.HistoricoDivisaoTurmaConfiguracaoGrade.App_LocalResources;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.GRD.Controllers
{
    public class HistoricoDivisaoTurmaConfiguracaoGradeController : SMCControllerBase
    {
        #region [ Services ]   

        private IHistoricoDivisaoTurmaConfiguracaoGradeService HistoricoDivisaoTurmaConfiguracaoGradeService
        {
            get { return this.Create<IHistoricoDivisaoTurmaConfiguracaoGradeService>(); }
        }

        private IDivisaoTurmaService DivisaoTurmaService
        {
            get { return this.Create<IDivisaoTurmaService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ActionResult Index(HistoricoDivisaoTurmaConfiguracaoGradeFiltroViewModel filtro)
        {
            var dadosDivisao = DivisaoTurmaService.BuscarDivisaoTurmaDetalhes(filtro.SeqDivisaoTurma).FirstOrDefault(d => d.Seq == filtro.SeqDivisaoTurma);
            filtro.DescricaoTurma = dadosDivisao.TurmaDescricaoFormatado;
            filtro.DescricoesCursoOfertaLocalidadeTurno = dadosDivisao.DescricoesCursoOfertaLocalidadeTurno;

            //filtro.CodificacaoTurmaDescricaoConfiguracaoComponente = this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarCabecalhoConfiguracaoGrade(filtro.SeqDivisaoTurma);

            return View(filtro);
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ActionResult ListarHistoricosDivisaoConfiguracaoGrade(HistoricoDivisaoTurmaConfiguracaoGradeFiltroViewModel filtro)
        {
            SMCPagerModel<HistoricoDivisaoTurmaConfiguracaoGradeListarViewModel> model = ExecuteService<HistoricoDivisaoTurmaConfiguracaoGradeFiltroData, HistoricoDivisaoTurmaConfiguracaoGradeListarData,
                                                                                         HistoricoDivisaoTurmaConfiguracaoGradeFiltroViewModel, HistoricoDivisaoTurmaConfiguracaoGradeListarViewModel>
                                                                                         (HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarHistoricosDivisaoConfiguracaoGrade, filtro);

            return PartialView("_DetailList", model);
        }

        [SMCAuthorize(UC_GRD_001_02_01.ALTERAR_CONFIGURACAO_GRADE)]
        public ActionResult Incluir(long seqDivisaoTurma)
        {
            var modelo = this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarNovaConfiguracaoGrade(seqDivisaoTurma).Transform<HistoricoDivisaoTurmaConfiguracaoGradeViewModel>();

            if (modelo.TipoDistribuicaoAula != TipoDistribuicaoAula.Nenhum)
            {
                modelo.TiposPagamento = this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarTiposPagamentoSelect(modelo.TipoDistribuicaoAula);
                modelo.TiposPulaFeriado = this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarTiposPulaFeriadoSelect(modelo.TipoDistribuicaoAula);
            }

            return PartialView("_DadosHistoricoDivisaoTurmaConfiguracaoGrade", modelo);
        }

        [SMCAuthorize(UC_GRD_001_02_01.ALTERAR_CONFIGURACAO_GRADE)]
        public ActionResult Editar(long seq)
        {
            var modelo = this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarHistoricoDivisaoConfiguracaoGrade(seq).Transform<HistoricoDivisaoTurmaConfiguracaoGradeViewModel>();
            modelo.TiposPagamento = this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarTiposPagamentoSelect(modelo.TipoDistribuicaoAula);
            modelo.TiposPulaFeriado = this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarTiposPulaFeriadoSelect(modelo.TipoDistribuicaoAula);

            return PartialView("_DadosHistoricoDivisaoTurmaConfiguracaoGrade", modelo);
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ActionResult BuscarTiposPagamentoSelect(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            return Json(this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarTiposPagamentoSelect(tipoDistribuicaoAula));
        }

        [SMCAuthorize(UC_GRD_001_01_01.PESQUISAR_EVENTO_AULA)]
        public ActionResult BuscarTiposPulaFeriadoSelect(TipoDistribuicaoAula tipoDistribuicaoAula)
        {
            return Json(this.HistoricoDivisaoTurmaConfiguracaoGradeService.BuscarTiposPulaFeriadoSelect(tipoDistribuicaoAula));
        }

        [SMCAuthorize(UC_GRD_001_02_01.ALTERAR_CONFIGURACAO_GRADE)]
        public ActionResult Salvar(HistoricoDivisaoTurmaConfiguracaoGradeViewModel modelo)
        {
            long retorno = this.HistoricoDivisaoTurmaConfiguracaoGradeService.Salvar(modelo.Transform<HistoricoDivisaoTurmaConfiguracaoGradeData>());

            SetSuccessMessage(UIResource.Mensagem_Sucesso_Configuracao_Grade, target: SMCMessagePlaceholders.Centro);

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqDivisaoTurma = new SMCEncryptedLong(modelo.SeqDivisaoTurma) });
        }

        [SMCAuthorize(UC_GRD_001_01_04.EXCLUIR_EVENTO_AULA)]
        public ActionResult Excluir(long seq, long seqDivisaoTurma)
        {
            try
            {
                this.HistoricoDivisaoTurmaConfiguracaoGradeService.Excluir(seq);

                SetSuccessMessage(UIResource.Mensagem_Sucesso_Exclusao_Configuracao_Grade, target: SMCMessagePlaceholders.Centro);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, target: SMCMessagePlaceholders.Centro);
            }

            return SMCRedirectToAction(nameof(Index), routeValues: new { seqDivisaoTurma = new SMCEncryptedLong(seqDivisaoTurma) });
        }
    }
}