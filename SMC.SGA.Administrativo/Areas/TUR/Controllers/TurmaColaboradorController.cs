using SMC.Academico.Common.Areas.TUR.Constants;
using SMC.Academico.ServiceContract.Areas.TUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.TUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Controllers
{
    public class TurmaColaboradorController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private ITurmaService TurmaService
        {
            get { return this.Create<ITurmaService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_TUR_001_02_03.ASSOCIAR_PROFESSOR_RESPOSAVEL_TURMA)]
        public ActionResult CabecalhoTurma(long seq)
        {
            var cabecalho = TurmaService.BuscarTurmaCabecalho(seq).Transform<TurmaCabecalhoViewModel>();
            return PartialView("_Cabecalho", cabecalho);
        }
    }
}