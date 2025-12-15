using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.App_GlobalResources;
using SMC.SGA.Administrativo.Areas.OFC.Models;
using SMC.SGA.Administrativo.Areas.OFC.Services;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using SMC.SGA.Administrativo.Areas.ORG.Services;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.OFC.Controllers
{
    [SMCAllowAnonymous]
    public class CicloLetivoController : SMCControllerBase
    {
        #region Serviços

        private ICicloLetivoControllerService CicloLetivoControllerService
        {
            get
            {
                return this.Create<ICicloLetivoControllerService>();
            }
        }

        private INivelEnsinoControllerService NivelEnsinoControllerService
        {
            get
            {
                return this.Create<INivelEnsinoControllerService>();
            }
        }

        private IORGDynamicControllerService ORGDynamicControllerService
        {
            get
            {
                return this.Create<IORGDynamicControllerService>();
            }
        }

        private IOFCDynamicControllerService OFCDynamicControllerService
        {
            get
            {
                return this.Create<IOFCDynamicControllerService>();
            }
        }

        #endregion

        #region Listar

        /// <summary>
        /// Exibe tela de listagem de ciclos letivos
        /// </summary>
        /// <param name="filtros">Filtros de pesquisa</param>
        public ActionResult Index(CicloLetivoFiltroViewModel filtros = null)
        {           
            return View(filtros);
        }

        /// <summary>
        /// Renderiza grid de listagem de ciclos letivos
        /// </summary>
        /// <param name="filtros">Filtros de pesquisa</param>
        public ActionResult ListarCicloLetivo(CicloLetivoFiltroViewModel filtros)
        {
            SMCPagerData<CicloLetivoListaViewModel> data = CicloLetivoControllerService.BuscarCiclosLetivos(filtros);

            SMCPagerModel<CicloLetivoListaViewModel> pager = new SMCPagerModel<CicloLetivoListaViewModel>(data, filtros.PageSettings, filtros);

            return PartialView("_ListarCicloLetivo", pager);
        }

        #endregion

        #region Preenchimento de modelos

        /// <summary>
        /// Preenche lista de itens utilizada para renderizar componente de select
        /// </summary>
        /// <param name="modelo">Modelo preenchido</param>
        [NonAction]
        private void PreencherModeloCicloLetivo(CicloLetivoViewModel modelo)
        {
            modelo.RegimesLetivos = OFCDynamicControllerService.BuscarRegimesLetivosSelect();          
        }

        #endregion

        #region Incluir/Editar

        /// <summary>
        /// Exibe a tela de inclusão de um ciclo letivo
        /// </summary>
        [HttpGet]
        public ActionResult Incluir()
        {
            CicloLetivoViewModel modelo = new CicloLetivoViewModel();

            PreencherModeloCicloLetivo(modelo);

            return View(modelo);
        }

        /// <summary>
        /// Exibe tela de edição para um ciclo letivo
        /// </summary>
        /// <param name="seqEntidade">Sequencial do ciclo letivo a ser editado</param>
        [HttpGet]
        public ActionResult Editar(SMCEncryptedLong seqCicloLetivo)
        {
            CicloLetivoViewModel modelo = CicloLetivoControllerService.BuscarCicloLetivo(seqCicloLetivo);

            PreencherModeloCicloLetivo(modelo);

            return View(modelo);
        }

        [HttpPost]
        public ActionResult Salvar(CicloLetivoViewModel modelo)
        {
            long seq;

            try
            {
                if (SalvarCicloLetivo(modelo, out seq))
                    return RedirectToAction("Editar", new { seqCicloLetivo = (SMCEncryptedLong)seq });

                return View(modelo.Seq == 0 ? "Incluir" : "Editar", modelo);
            }
            finally
            {
                PreencherModeloCicloLetivo(modelo);
            }
        }

        [HttpPost]
        public ActionResult SalvarNovo(CicloLetivoViewModel modelo)
        {
            try
            {
                long seq;
                if (SalvarCicloLetivo(modelo, out seq))
                    return RedirectToAction("Incluir");
            }
            finally
            {
                PreencherModeloCicloLetivo(modelo);
            }

            return View(modelo.Seq == 0 ? "Incluir" : "Editar", modelo);
        }

        [HttpPost]
        public ActionResult SalvarSair(CicloLetivoViewModel modelo)
        {
            try
            {
                long seq;
                if (SalvarCicloLetivo(modelo, out seq))
                    return RedirectToAction("Index");
            }
            finally
            {
                PreencherModeloCicloLetivo(modelo);
            }

            return View(modelo.Seq == 0 ? "Incluir" : "Editar", modelo);
        }

        [NonAction]
        public bool SalvarCicloLetivo(CicloLetivoViewModel modelo, out long seq)
        {
            bool sucesso = this.Save(modelo, CicloLetivoControllerService.SalvarCicloLetivo, out seq);
            
            if (sucesso)
                SetSuccessMessage(string.Format(modelo.Seq == 0 ? MessagesResource.Mensagem_Sucesso_Inclusao_Registro :
                                                                    MessagesResource.Mensagem_Sucesso_Edicao_Registro,
                                                MessagesResource.Entidade_CicloLetivo),
                                  MessagesResource.Titulo_Sucesso,
                                  SMCMessagePlaceholders.Centro);

            return sucesso;
        }

        #endregion

        #region Excluir

        /// <summary>
        /// Realiza a exclusão de um ciclo letivo
        /// </summary>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo a ser excluído</param>
        [HttpPost]
        public ActionResult Excluir(SMCEncryptedLong seqCicloLetivo)
        {
            CicloLetivoControllerService.ExcluirCicloLetivo(seqCicloLetivo);

            SetSuccessMessage(string.Format(MessagesResource.Mensagem_Sucesso_Exclusao_Registro,
                                            MessagesResource.Entidade_CicloLetivo),
                                MessagesResource.Titulo_Sucesso,
                                SMCMessagePlaceholders.Centro);

            return RenderAction("ListarCicloLetivo");
        }

        #endregion
    }
}
