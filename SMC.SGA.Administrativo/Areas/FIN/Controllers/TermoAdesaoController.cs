using SMC.Academico.Common.Areas.FIN.Constants; 
using SMC.Academico.ServiceContract.Areas.FIN.Data;
using SMC.Academico.ServiceContract.Areas.FIN.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security; 
using SMC.SGA.Administrativo.Areas.FIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using SMC.Framework.Model;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.FIN.Controllers
{

    public class TermoAdesaoController : SMCDynamicControllerBase
    {
        #region Serviços

        private ITermoAdesaoService TermoAdesaoService
        {
            get { return this.Create<ITermoAdesaoService>(); }
        }

        private ITipoVinculoAlunoService TipoVinculoAlunoService
        {
            get { return this.Create<ITipoVinculoAlunoService>(); }
        }


        #endregion

        [SMCAuthorize(UC_FIN_002_02_03.PESQUISAR_TERMO_ADESAO)]
        public ActionResult CabecalhoTermoAdesao(SMCEncryptedLong seqContrato)
        {
            var modelHeader = ExecuteService<TermoAdesaoCabecalhoData, TermoAdesaoCabecalhoViewModel>(TermoAdesaoService.BuscarCabecalhoTermoAdesao, seqContrato);

            return PartialView("_Cabecalho", modelHeader);
        }

        [SMCAuthorize(UC_FIN_002_02_03.PESQUISAR_TERMO_ADESAO)]
        public ActionResult BuscarTiposVinculoAlunoPorServicoSelect(long seqServico)
        {
            var list = TipoVinculoAlunoService.BuscarTiposVinculoAlunoPorServicoSelect(seqServico);

            return Json(list);
        }
    }
}
 
