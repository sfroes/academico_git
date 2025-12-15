using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Helpers;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class MatrizCurricularController : SMCDynamicControllerBase
    {
        #region [ Service ]

        internal ICurriculoCursoOfertaService CurriculoCursoOfertaService
        {
            get { return this.Create<ICurriculoCursoOfertaService>(); }
        }

        internal ICursoOfertaLocalidadeService CursoOfertaLocalidadeService
        {
            get { return this.Create<ICursoOfertaLocalidadeService>(); }
        }

        internal IMatrizCurricularService MatrizCurricularService
        {
            get { return this.Create<IMatrizCurricularService>(); }
        }

        internal ITurnoService TurnoService
        {
            get { return this.Create<ITurnoService>(); }
        }

        #endregion [ Service ]

        [SMCAuthorize(UC_CUR_001_05_01.PESQUISAR_MATRIZ_CURRICULAR)]
        public ActionResult CabecalhoCurriculoCursoOferta(SMCEncryptedLong seqCurriculoCursoOferta)
        {
            CurriculoCursoOfertaCabecalhoViewModel model = this.CurriculoCursoOfertaService.BuscarCurriculoCursoOfertaCabecalho(seqCurriculoCursoOferta, false).Transform<CurriculoCursoOfertaCabecalhoViewModel>();
            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_CUR_001_05_02.MANTER_MATRIZ_CURRICULAR)]
        public ActionResult PreencherDescricaoMatrizCurricular(SMCEncryptedLong seqDivisaoCurricular, SMCEncryptedLong seqModalidade)
        {
            if (seqDivisaoCurricular == null)
                return Json(string.Empty);

            if (seqModalidade == null)
                return Json(string.Empty);

            string descricao = MatrizCurricularService.BuscarConfiguracaoDescricaoMatrizCurricular(seqDivisaoCurricular, seqModalidade);
            return Json(descricao);
        }

        [SMCAuthorize(UC_CUR_001_05_02.MANTER_MATRIZ_CURRICULAR)]
        public ActionResult PreencherCodigoMatrizCurricularItem(SMCEncryptedLong seqCursoOfertaLocalidade, string codigoMatrizCurricular)
        {
            string descricao = MatrizCurricularService.BuscarConfiguracaoLocalidadeMatrizCurricular(seqCursoOfertaLocalidade, codigoMatrizCurricular);
            return Json(descricao);
        }

        [SMCAuthorize(UC_CUR_001_05_02.MANTER_MATRIZ_CURRICULAR)]
        public ActionResult BuscarTurnosPorCursoOfertaLocalidadeSelect(SMCEncryptedLong seqCursoOfertaLocalidade)
        {
            var turnos = this.TurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(seqCursoOfertaLocalidade);
            return Json(turnos);
        }

        /// <summary>
        /// Passo 2 - Mestre detalhe a opção descritiva do turno na listagem
        /// </summary>
        /// <param name="model">Modelo de matriz curricular</param>
        /// <returns>View do wizard</returns>
        [SMCAuthorize(UC_CUR_001_05_02.MANTER_MATRIZ_CURRICULAR)]
        public ActionResult OfertasMatriz(MatrizCurricularDynamicModel model)
        {
            this.ConfigureDynamic(model);

            model.Ofertas.SMCForEach(f =>
            {
                f.Turnos = this.TurnoService.BuscarTurnosPorCursoOfertaLocalidadeSelect(f.SeqCursoOfertaLocalidade);
                f.DescricaoTurno = f.Turnos.Where(w => w.Seq == f.SeqCursoOfertaTurno).Select(s => s.Descricao).FirstOrDefault();
            });
           
            // Retorna o passo do wizard
            return SMCViewWizard(model, null);
        }
    }
}