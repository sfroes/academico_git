using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class MatrizCurricularConfiguracaoComponenteController : SMCDynamicControllerBase
    {
        #region [ Service ]

        private ICurriculoCursoOfertaGrupoService CurriculoCursoOfertaGrupoService
        {
            get { return this.Create<ICurriculoCursoOfertaGrupoService>(); }
        }

        private IMatrizCurricularService MatrizCurricularService
        {
            get { return this.Create<IMatrizCurricularService>(); }
        }

        #endregion [ Service ]

        public ActionResult MatrizCurricularConfiguracaoComponenteCabecalho(SMCEncryptedLong seqMatrizCurricular)
        {
            var model = ExecuteService<MatrizCurricularCabecalhoData, MatrizCurricularCabecalhoViewModel>(MatrizCurricularService.BuscarMatrizCurricularCabecalho, seqMatrizCurricular);
            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_CUR_001_05_05.MANTER_CONFIGURACAO_GRUPO_MATRIZ)]
        public ActionResult BuscarDescricaoTipoConfiguracaoGrupoCurricular(SMCEncryptedLong seqCurriculoCursoOfertaGrupo)
        {
            var registro = CurriculoCursoOfertaGrupoService.BuscarCurriculoCursoOfertaGrupoComTipoConfiguracao(seqCurriculoCursoOfertaGrupo);
            return Json(registro.DescricaoTipoConfiguracaoGrupoCurricular);
        }

        [SMCAuthorize(UC_CUR_001_05_05.MANTER_CONFIGURACAO_GRUPO_MATRIZ)]
        public ActionResult BuscarFormatoTipoConfiguracaoGrupoCurricular(SMCEncryptedLong seqCurriculoCursoOfertaGrupo)
        {
            var registro = CurriculoCursoOfertaGrupoService.BuscarCurriculoCursoOfertaGrupoComTipoConfiguracao(seqCurriculoCursoOfertaGrupo);
            return Json((registro.FormatoConfiguracaoGrupoGrupoCurricular ?? FormatoConfiguracaoGrupo.Nenhum).ToString());
        }

        [SMCAuthorize(UC_CUR_001_05_05.MANTER_CONFIGURACAO_GRUPO_MATRIZ)]
        public ActionResult BuscarQuantidadeFormatadaTipoConfiguracaoGrupoCurricular(SMCEncryptedLong seqCurriculoCursoOfertaGrupo)
        {
            var registro = CurriculoCursoOfertaGrupoService.BuscarCurriculoCursoOfertaGrupoComTipoConfiguracao(seqCurriculoCursoOfertaGrupo);
            return Json(registro.QuantidadeFormatada);
        }
    }
}