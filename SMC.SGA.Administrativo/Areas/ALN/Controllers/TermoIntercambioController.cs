using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ALN.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ALN.Controllers
{
    public class TermoIntercambioController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private ITermoIntercambioService TermoIntercambioService
        {
            get { return Create<ITermoIntercambioService>(); }
        }

        private ITipoTermoIntercambioService TipoTermoIntercambioService
        {
            get { return Create<ITipoTermoIntercambioService>(); }
        }

        private IInstituicaoNivelTipoTermoIntercambioService InstituicaoNivelTipoTermoIntercambioService
        {
            get { return Create<IInstituicaoNivelTipoTermoIntercambioService>(); }
        }

        private IParceriaIntercambioService ParceriaIntercambioService
        {
            get { return Create<IParceriaIntercambioService>(); }
        }

        #endregion [ Serviços ]

        [SMCAuthorize(UC_ALN_004_01_03.PESQUISAR_TERMO_INTERCAMBIO)]
        public ActionResult CabecalhoTermoIntercambio(SMCEncryptedLong seqParceriaIntercambio)
        {
            var modelHeader = ExecuteService<TermoIntercambioCabecalhoData, TermoIntercambioCabecalhoViewModel>(TermoIntercambioService.BuscarCabecalhoTermoIntercambio, seqParceriaIntercambio);
            return PartialView("_Cabecalho", modelHeader);
        }

        [SMCAuthorize(UC_ALN_004_01_03.PESQUISAR_TERMO_INTERCAMBIO)]
        public JsonResult BuscarTermosPorNivelEnsinoSelect(long? SeqNivelEnsino, long seqParceriaIntercambio, bool? ativo = null)
        {
            List<SMCDatasourceItem> termos = TipoTermoIntercambioService.BuscaTiposTermoIntercabioPorParceriaIntercambioTipoTermoSelect(SeqNivelEnsino, seqParceriaIntercambio, ativo);
            return Json(termos);
        }

        [SMCAuthorize(UC_ALN_004_01_04.MANTER_TERMO_INTERCAMBIO)]
        public JsonResult ValidarTipoTermoIntercambio(long seqParceriaIntercambio, long seqNivelEnsino, long? seqParceriaIntercambioTipoTermo = null)
        {
            if (seqParceriaIntercambioTipoTermo.HasValue)
            {
                ParceriaIntercambioData data = ParceriaIntercambioService.BuscarParceriaIntercambio(seqParceriaIntercambio);
                return Json(InstituicaoNivelTipoTermoIntercambioService.ExigirVigenciaTermoIntercambio(seqParceriaIntercambioTipoTermo.Value, data.SeqInstituicaoEnsino, seqNivelEnsino));
            }

            return Json(false);
        }

        [SMCAuthorize(UC_ALN_004_01_03.PESQUISAR_TERMO_INTERCAMBIO)]
        public ActionResult CpfOuPassaportObrigatorio(string cpf, string passaporte)
        {
            return string.IsNullOrEmpty(cpf) && string.IsNullOrEmpty(passaporte) ? Content("true") : Content("false");
        }
    }
}