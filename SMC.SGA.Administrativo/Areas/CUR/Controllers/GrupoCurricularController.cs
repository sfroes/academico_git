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
    public class GrupoCurricularController : SMCDynamicControllerBase
    {
        #region [ Services ]

        private IGrupoCurricularService GrupoCurricularService
        {
            get { return this.Create<IGrupoCurricularService>(); }
        }

        private ITipoConfiguracaoGrupoCurricularService TipoConfiguracaoGrupoCurricularService
        {
            get { return this.Create<ITipoConfiguracaoGrupoCurricularService>(); }
        }

        #endregion [ Services ]

        [SMCAuthorize(UC_CUR_001_01_03.PESQUISAR_GRUPO_CURRICULAR)]
        public ActionResult CabecalhoGrupoCurricular(SMCEncryptedLong seqCurriculo)
        {
            var model = ExecuteService<GrupoCurricularCabecalhoData, GrupoCurricularCabecalhoViewModel>(
                this.GrupoCurricularService.BuscarGrupoCurricularCabecalho, seqCurriculo);
            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR)]
        public ActionResult CalcularHorasAula(short quantidadeHoraRelogio = 0)
        {
            return new CurriculoController().CalcularHorasAula(quantidadeHoraRelogio);
        }

        [SMCAuthorize(UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR)]
        public ActionResult CalcularHorasRelogio(short quantidadeHoraAula = 0)
        {
            return new CurriculoController().CalcularHorasRelogio(quantidadeHoraAula);
        }

        [SMCAuthorize(UC_CUR_001_01_04.MANTER_GRUPO_CURRICULAR)]
        public ActionResult VerificarTipoConfiguracaoFormatoExigido(SMCEncryptedLong seqTipoConfiguracaoGrupoCurricular)
        {
            var exigeFormato = this.TipoConfiguracaoGrupoCurricularService
                .BuscarTipoConfiguracaoGrupoCurricular(seqTipoConfiguracaoGrupoCurricular)
                .ExigeFormato;
            return Json(exigeFormato);
        }
    }
}