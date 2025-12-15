using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Areas.CAM.Exceptions;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using SMC.SGA.Administrativo.Areas.CAM.Views.CicloLetivo.App_LocalResources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class CicloLetivoCopiaController : SMCControllerBase
    {
        #region [ Serviços ]

        private ICicloLetivoService CicloLetivoService
        {
            get { return this.Create<ICicloLetivoService>(); }
        }

        private IRegimeLetivoService RegimeLetivoService
        {
            get { return this.Create<IRegimeLetivoService>(); }
        }

        #endregion [ Serviços ]

        [SMCAuthorize(UC_CAM_002_01_05.COPIAR_CICLO_LETIVO)]
        public ActionResult Index(long[] gridPadrao)
        {
            var model = new CicloLetivoCopiaFiltroViewModel();

            try
            {
                var ciclosLetivos = this.CicloLetivoService.BuscarCiclosLetivosCopia(new CicloLetivoCopiaFiltroData() { SeqsCiclosLetivos = gridPadrao.ToList() });

                model.SeqsCiclosLetivos = ciclosLetivos.Select(x => x.Seq).ToList();
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Default);

                return SMCRedirectToAction("Index", "CicloLetivo");
            }

            return View(model);
        }

        [SMCAuthorize(UC_CAM_002_01_05.COPIAR_CICLO_LETIVO)]
        public ActionResult BuscarCiclosLetivosCopia(CicloLetivoCopiaFiltroViewModel filtros)
        {
            //TODO: Fix verificar por que o page settings não está sendo passado
            if (filtros.PageSettings == null)
                filtros.PageSettings = new SMCPageSetting();

            SMCPagerModel<CicloLetivoCopiaItemViewModel> pager =
                ExecuteService<CicloLetivoCopiaFiltroData,
                               CicloLetivoData,
                               CicloLetivoCopiaFiltroViewModel,
                               CicloLetivoCopiaItemViewModel>(this.CicloLetivoService.BuscarCiclosLetivosCopia, filtros);

            return PartialView("_GridCicloLetivoCopia", pager);
        }

        [SMCAuthorize(UC_CAM_002_01_05.COPIAR_CICLO_LETIVO)]
        public ActionResult CopiarCiclosLetivos(CicloLetivoCopiaFiltroViewModel filtros)
        {
            try
            {
                if (filtros.SeqsCiclosLetivos.SMCCount() == 0)
                    throw new CicloLetivoCopiaNenhumSelecionadoException();

                var filtroDto = filtros.Transform<CicloLetivoCopiaFiltroData>();

                this.CicloLetivoService.CopiarCiclosLetivos(filtroDto);

                string msg = string.Format(UIResource.MSG_CopiaCiclo, "Registro");

                SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Default);
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex.Message, UIResource.MSG_Titulo_Erro, SMCMessagePlaceholders.Default);
            }

            return SMCRedirectToAction("Index", "CicloLetivo");
        }
    }
}