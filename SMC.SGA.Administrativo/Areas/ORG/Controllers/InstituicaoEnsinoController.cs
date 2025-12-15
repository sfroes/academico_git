using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.DataFilters;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Extensions;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class InstituicaoEnsinoController : SMCDynamicControllerBase
    {
        #region Serviços

        private IInstituicaoEnsinoService InstituicaoEnsinoService
        {
            get { return this.Create<IInstituicaoEnsinoService>(); }
        }

        #endregion Serviços

        #region Seleção de Instituição Ensino Logada

        /// <summary>
        /// Modal para seleção de instituição de ension
        /// </summary>
        [ChildActionOnly]
        public ActionResult ModalSelecionarInstituicao()
        {
            var model = new InstituicaoEnsinoSeletorViewModel();

            // Se não tem instituição logada, busca as possíveis para apresentar na modal.
            var instituicaoLogada = this.GetInstituicaoEnsinoLogada();
            if (instituicaoLogada == null || instituicaoLogada.Seq <= 0)
            {
                // Desabilita o filtro global para selecionar as instituição de ensino que o usuário pode selecionar
                this.EnabledFilterGlobal(false);
                model.InstituicoesEnsino = InstituicaoEnsinoService.BuscarInstituicoesEnsinoSelect();

                // Seleciona a instituição automaticamente caso tenha somente uma
                if (model.InstituicoesEnsino.Count == 1)
                {
                    SelecionarInstituicao(model.InstituicoesEnsino.FirstOrDefault().Seq);
                    model.HabilitarSelecao = false;
                }
                else
                    model.HabilitarSelecao = true;
            }
            else
            {
                model.HabilitarSelecao = false;
            }

            return PartialView("_ModalSelecionarInstituicaoEnsino", model);
        }

        /// <summary>
        /// POST que recebe a instituição selecionada pelo usuário e armazena no cookie
        /// </summary>
        /// <param name="seqInstituicaoEnsino">Instituição de ensino selecionada</param>
        [HttpPost]
        [SMCAllowAnonymous]
        public ActionResult SelecionarInstituicao(long seqInstituicaoEnsino = 0)
        {
            // Busca os dados da instituição selecionada
            // Fix: CAROL - Está recuperando a instituição de ensino completa, sendo que precisa apenas dos dados da instituição e do logotipo
            var instituicao = ExecuteService<InstituicaoEnsinoData, InstituicaoEnsinoDynamicModel>(InstituicaoEnsinoService.BuscarInstituicaoEnsino, seqInstituicaoEnsino);

            try
            {
                // Cria o cookie
                this.SetCookie(new SMCEntityFilterGlobalModel()
                {
                    FilterKey = FILTER.INSTITUICAO_ENSINO,
                    Value = instituicao.Seq,
                    Description = instituicao.Sigla,
                    SeqArquivoLogo = instituicao.SeqArquivoLogotipo
                });
            }
            finally
            {
                // Habilita o filtro global
                this.EnabledFilterGlobal(true);
            }

            return SMCRedirectToAction("Index", "Home", new { area = "" });
        }

        /// <summary>
        /// Apresenta os dados da instituição logada no topo da página
        /// </summary>
        [ChildActionOnly]
        public ActionResult MarcaInstituicao()
        {
            this.EnabledFilterGlobal(true);
            var instituicaoSelecionada = this.HttpContext.GetInstituicaoEnsinoLogada();
            return PartialView("_MarcaInstituicao", instituicaoSelecionada);
        }

        /// <summary>
        /// Apresenta a modal para trocar a instituição de ensino logada
        /// </summary>
        [HttpGet]
        [SMCAllowAnonymous]
        public ActionResult ModalTrocarInstituicao()
        {
            SMCDataFilterHelper.ClearGlobalFilter();
            this.ClearCookie();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        #endregion Seleção de Instituição Ensino Logada
    }
}