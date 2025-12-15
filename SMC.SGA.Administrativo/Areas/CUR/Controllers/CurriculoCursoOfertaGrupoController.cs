using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CUR.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Controllers
{
    public class CurriculoCursoOfertaGrupoController : SMCDynamicControllerBase
    {
        #region [ Service ]

        internal ICurriculoCursoOfertaService CurriculoCursoOfertaService
        {
            get { return this.Create<ICurriculoCursoOfertaService>(); }
        }

        internal ICurriculoCursoOfertaGrupoService CurriculoCursoOfertaGrupoService
        {
            get { return this.Create<ICurriculoCursoOfertaGrupoService>(); }
        }

        #endregion [ Service ]

        [SMCAuthorize(UC_CUR_001_01_05.PESQUISAR_ASSOCIACAO_GRUPO_CURRICULAR_OFERTA_CURSO)]
        public ActionResult CabecalhoCurriculoCursoOferta(SMCEncryptedLong seqCurriculoCursoOferta)
        {
            CurriculoCursoOfertaCabecalhoViewModel model = this.CurriculoCursoOfertaService.BuscarCurriculoCursoOfertaCabecalho(seqCurriculoCursoOferta, true).Transform<CurriculoCursoOfertaCabecalhoViewModel>();
            return PartialView("_Cabecalho", model);
        }

        [SMCAuthorize(UC_CUR_001_01_06.ASSOCIAR_GRUPO_CURRICULAR_OFERTA_CURSO)]
        public ActionResult BuscarCurriculoCursoOfertaGrupoValor(long seq, long seqCurriculoCursoOferta, long seqGrupoCurricular)
        {
            if (seqGrupoCurricular == 0)
                return PartialView("_CurriculoCursoOfertaGrupoValor", new CurriculoCursoOfertaGrupoValorViewModel());

            var model = CurriculoCursoOfertaGrupoService.BuscarValorCurriculoCursoOfertaGrupo(seqCurriculoCursoOferta, seqGrupoCurricular, seq == 0)
                .Transform<CurriculoCursoOfertaGrupoValorViewModel>();
            model.Prefixo = nameof(CurriculoCursoOfertaGrupoDynamicModel.QuantidadesGrupoCurricularSelecionado);
            return PartialView("_CurriculoCursoOfertaGrupoValor", model);
        }
    }
}