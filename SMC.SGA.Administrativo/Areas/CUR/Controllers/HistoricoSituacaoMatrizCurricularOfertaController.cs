using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class HistoricoSituacaoMatrizCurricularOfertaController : SMCDynamicControllerBase
    {
        #region [ Services ]

        internal IMatrizCurricularService MatrizCurricularService
        {
            get { return this.Create<IMatrizCurricularService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_CUR_001_05_03.MANTER_HISTORICO_SITUACAO_MATRIZ_CURRICULAR)]
        public ActionResult CabecalhoMatrizCurricularOferta(SMCEncryptedLong SeqMatrizCurricularOferta)
        {
            MatrizCurricularOfertaCabecalhoViewModel model = ExecuteService<MatrizCurricularOfertaCabecalhoData, MatrizCurricularOfertaCabecalhoViewModel>(MatrizCurricularService.BuscarMatrizCurricularOfertaCabecalho, SeqMatrizCurricularOferta);
            return PartialView("_Cabecalho", model);
        }
    }
}