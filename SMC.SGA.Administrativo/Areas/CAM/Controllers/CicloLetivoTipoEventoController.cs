using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CAM.Models;
using SMC.SGA.Administrativo.Areas.CAM.Views.CicloLetivoTipoEvento.App_LocalResources;
using SMC.SGA.Administrativo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Controllers
{
    public class CicloLetivoTipoEventoController : SMCDynamicControllerBase
    {
        #region Serviços

        private ICicloLetivoService CicloLetivoService
        {
            get { return this.Create<ICicloLetivoService>(); }
        }

        private ICicloLetivoTipoEventoService CicloLetivoTipoEventoService
        {
            get { return this.Create<ICicloLetivoTipoEventoService>(); }
        }

        private IInstituicaoTipoEventoService InstituicaoTipoEventoService
        {
            get { return this.Create<IInstituicaoTipoEventoService>(); }
        }

        #endregion Serviços

        [SMCAuthorize(UC_CAM_002_01_01.PESQUISAR_CICLO_LETIVO)]
        public ActionResult CabecalhoCicloLetivo(CicloLetivoTipoEventoDynamicModel model)
        {
            CicloLetivoCabecalhoViewModel modelHeader = ExecuteService<CicloLetivoData, CicloLetivoCabecalhoViewModel>(CicloLetivoService.BuscarCicloLetivo, model.SeqCicloLetivo);

            return PartialView("_Cabecalho", modelHeader);
        }

        [SMCAuthorize(UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)]
        public JsonResult BuscarTiposEventosAGDSelect(TipoEventoFiltroViewModel filtros)
        {
            List<SMCDatasourceItem> tiposEvento = this.InstituicaoTipoEventoService.BuscarTiposEventosAGDSelect(filtros.Transform<TipoEventoFiltroData>());
            return Json(tiposEvento);
        }

        [SMCAuthorize(UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)]
        public ActionResult BuscarParametrosInstituicaoTipoEventoSelect(InstituicaoTipoEventoFiltroData filtros)
        {
            List<SMCDatasourceItem> parametros = this.InstituicaoTipoEventoService.BuscarParametrosInstituicaoTipoEventoSelect(filtros);
            return Json(parametros);
        }

        [SMCAuthorize(UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)]
        public string PreencherMensagemCicloLetivoTipoEvento(CicloLetivoTipoEventoDynamicModel model)
        {
            var result = string.Empty;

            var instituicaoTipoEvento = BuscarInstituicaoTipoEvento(model);

            if (instituicaoTipoEvento != null)
            {
                switch (instituicaoTipoEvento.AbrangenciaEvento)
                {
                    case AbrangenciaEvento.Geral:

                        if (instituicaoTipoEvento.Parametros.Any())
                            result = UIResource.MSG_GeralComParametro;
                        else
                            result = UIResource.MSG_GeralSemParametro;

                        break;

                    case AbrangenciaEvento.PorNivelEnsino:

                        if (!instituicaoTipoEvento.Parametros.Any())
                            result = UIResource.MSG_PorNivelEnsinoSemParametro;

                        break;
                }
            }
            return result;
        }

        [SMCAuthorize(UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)]
        public ActionResult CarregarDados(CicloLetivoTipoEventoDynamicModel model)
        {
            this.ConfigureDynamic(model);

            model.ExibirParametro = ExibirParametroCicloLetivoTipoEvento(model);

            model.ExibirNivelEnsino = ExibirNivelEnsinoCicloLetivoTipoEvento(model);

            model.ExibirMensagem = ExibirMensagemCicloLetivoTipoEvento(model);

            model.Mensagem = PreencherMensagemCicloLetivoTipoEvento(model);

            return SMCViewWizard(model, null);
        }

        [SMCAuthorize(UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)]
        private bool ExibirNivelEnsinoCicloLetivoTipoEvento(CicloLetivoTipoEventoDynamicModel model)
        {
            var result = false;

            var instituicaoTipoEvento = BuscarInstituicaoTipoEvento(model);

            if (instituicaoTipoEvento != null)
            {
                result = instituicaoTipoEvento.AbrangenciaEvento == AbrangenciaEvento.PorNivelEnsino && instituicaoTipoEvento.Parametros.Any();
            }
            return result;
        }

        [SMCAuthorize(UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)]
        private bool ExibirParametroCicloLetivoTipoEvento(CicloLetivoTipoEventoDynamicModel model)
        {
            var result = false;

            var instituicaoTipoEvento = BuscarInstituicaoTipoEvento(model);

            if (instituicaoTipoEvento != null)
            {
                result = instituicaoTipoEvento.Parametros.Any();
            }
            return result;
        }

        [SMCAuthorize(UC_CAM_002_01_04.MANTER_PARAMETRIZACAO_TIPO_EVENTO_LETIVO)]
        private bool ExibirMensagemCicloLetivoTipoEvento(CicloLetivoTipoEventoDynamicModel model)
        {
            var result = false; ;

            var instituicaoTipoEvento = BuscarInstituicaoTipoEvento(model);

            if (instituicaoTipoEvento != null)
            {
                result = instituicaoTipoEvento.AbrangenciaEvento == AbrangenciaEvento.Geral ||
                        (instituicaoTipoEvento.AbrangenciaEvento == AbrangenciaEvento.PorNivelEnsino && !instituicaoTipoEvento.Parametros.Any());
            }

            return result;
        }

        private InstituicaoTipoEventoData BuscarInstituicaoTipoEvento(CicloLetivoTipoEventoDynamicModel model)
        {
            if (!model.SeqInstituicaoEnsino.HasValue || !model.SeqTipoAgenda.HasValue || !model.SeqTipoEventoAgd.HasValue)
                return null;

            return this.InstituicaoTipoEventoService.BuscarInstituicaoTipoEvento(model.Transform<InstituicaoTipoEventoFiltroData>());
        }
    }
}