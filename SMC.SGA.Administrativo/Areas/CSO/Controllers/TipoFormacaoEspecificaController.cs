using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class TipoFormacaoEspecificaController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private ITipoFormacaoEspecificaService TipoFormacaoEspecificaService
        {
            get { return this.Create<ITipoFormacaoEspecificaService>(); }
        }

        #endregion [ Service ]

        /// <summary>
        /// Busca utilizada no Lookup também sendo assim torna-se necessario a utilização anonima
        /// para que qualquer usuario logado tenha a possibilidade de utilizar o lookup.
        /// </summary>
        /// <param name="seqNivelEnsino">Sequenciais do nivel de ensino</param>
        /// <returns>Lista de formações especificas</returns>
        [SMCAllowAnonymous]
        public JsonResult BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(List<long> seqNivelEnsino)
        {
            var filtro = new TipoFormacaoEspecificaPorNivelEnsinoFiltroData() { SeqNivelEnsino = seqNivelEnsino, Ativo = true };

            List<SMCDatasourceItem> retorno = TipoFormacaoEspecificaService.BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(filtro);

            return Json(retorno);
        }

        [SMCAuthorize(UC_CSO_001_08_01.MANTER_TIPO_FORMACAO_ESPECIFICA)]
        public JsonResult SetaValorGrauDescricaoFormacao(bool? ExigeGrau)
        {
            if ((bool)!ExigeGrau)
                return Json("False");

            return Json(null);
        }

        [SMCAuthorize(UC_CSO_001_08_01.MANTER_TIPO_FORMACAO_ESPECIFICA)]
        public JsonResult SetaValorPermiteTitulacao(bool? ExigeGrau)
        {
            if ((bool)ExigeGrau)
                return Json("True");

            return Json(null);
        }
    }
}