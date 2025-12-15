using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Academico.ServiceContract.Areas.MAT.Interfaces;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Academico.UI.Mvc.Areas.MAT.Views.Chancela.App_LocalResources;
using SMC.Academico.UI.Mvc.Areas.SRC.Controllers;
using SMC.Academico.UI.Mvc.Areas.SRC.Models;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using System;
using System.Web.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Controllers
{
    public class ChancelaController : SolicitacaoServicoFluxoBaseController
    {
        #region [ Services ]

        private IPessoaAtuacaoService PessoaAtuacaoService => Create<IPessoaAtuacaoService>(); 

        private ISolicitacaoMatriculaService SolicitacaoMatriculaService => Create<ISolicitacaoMatriculaService>();

        private ISolicitacaoMatriculaItemService SolicitacaoMatriculaItemService => Create<ISolicitacaoMatriculaItemService>();

        #endregion [ Services ]
             
        public override ActionResult EntrarEtapa(SMCEncryptedLong seqSolicitacaoServico, SMCEncryptedLong seqConfiguracaoEtapa, string tokenRet)
        {
            try
            {
                /*
                     Se selecionada a opção SIM a solicitação deverá ser atribuída para o usuário logado.
                */

                // Chama a rotina para atribuir o usuário atual como responsável pelo atendimento da solicitação
                this.SolicitacaoServicoService.AtualizarUsuarioResponsavelAtendimento(seqSolicitacaoServico);

                return base.EntrarEtapa(seqSolicitacaoServico, seqConfiguracaoEtapa, tokenRet);
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "Chancela");
            }
        }

        public override ActionResult SelecaoChancelaPlanoEstudo(SolicitacaoServicoPaginaFiltroViewModel filtro)
        {
            try
            {
                filtro.Orientador = true;

                return base.SelecaoChancelaPlanoEstudo(filtro);
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "Chancela");
            }
        }

        [SMCAllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = UIResource.Chancela_Index_Title;
            ChancelaFiltroViewModel filtro = new ChancelaFiltroViewModel();

            var view = GetExternalView(AcademicoExternalViews.CHANCELA_PROFESSOR_INDEX);

            return View(view,filtro);
        }

        //[SMCAuthorize(UC_MAT_003_22_02.PESQUISA_SOLICITACAO_ORIENTADOR)]
        [SMCAllowAnonymous]
        public ActionResult Listar(ChancelaFiltroViewModel filtro)
        {
            var pager = ExecuteService<ChancelaFiltroData,
                               ChancelaItemListaData,
                               ChancelaFiltroViewModel,
                               ChancelaItemListaViewModel>(SolicitacaoMatriculaService.BuscarChancelas, filtro);

            var view = GetExternalView(AcademicoExternalViews.CHANCELA_PROFESSOR_LISTAR);

            return PartialView(view, pager);
        }

        //[SMCAuthorize(UC_MAT_003_22_01.CHANCELAR_PLANO_ESTUDO)]
        [SMCAllowAnonymous]
        public ActionResult ReabrirChancela(SMCEncryptedLong seqSolicitacaoServico)
        {
            try
            {
                long seqConfiguracaoEtapa = SolicitacaoMatriculaService.ReabrirChancelaMatricula(seqSolicitacaoServico);

                SetSuccessMessage("Chancela reaberta com sucesso!", "Sucesso", SMCMessagePlaceholders.Centro);
                return SMCRedirectToUrl(Url.Action("EntrarEtapa", new { seqSolicitacaoServico = seqSolicitacaoServico, seqConfiguracaoEtapa = new SMCEncryptedLong(seqConfiguracaoEtapa) }));
            }
            catch (Exception e)
            {
                return ThrowRedirect(e, "Index", "Chancela");
            }
        }
               
        //[SMCAuthorize(UC_MAT_003_22_02.PESQUISA_SOLICITACAO_ORIENTADOR)]
        [SMCAllowAnonymous]
        public ActionResult LimparProcessosFiltroChancela()
        {
            return SMCRedirectToAction("Index");
        }

        //[SMCAuthorize(UC_MAT_003_22_02.PESQUISA_SOLICITACAO_ORIENTADOR)]
        [SMCAllowAnonymous]
        public ActionResult BuscarProcessosFiltroChancela(bool apenasProcessoVigente)
        {
            return Json(this.SolicitacaoMatriculaService.BuscarProcessosFiltroChancela(apenasProcessoVigente));
        }
    }
}
