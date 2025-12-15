using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class AssociacaoEntidadesController : SMCDynamicControllerBase
    {
        #region [Services]

        private ITipoEntidadeService TipoEntidadeService => this.Create<ITipoEntidadeService>();
        private IAtoNormativoService AtoNormativoService => this.Create<IAtoNormativoService>();
        private IGrauAcademicoService GrauAcademicoService => this.Create<IGrauAcademicoService>();
        private IEntidadeService EntidadeService => this.Create<IEntidadeService>();
        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService => this.Create<ICursoOfertaLocalidadeService>();

        #endregion

        public ActionResult CabecalhoAssociacaoEntidades(AssociacaoEntidadesDynamicModel model)
        {
            AssociacaoEntidadesCabecalhoViewModel modelHeader = ExecuteService<AtoNormativoData, AssociacaoEntidadesCabecalhoViewModel>(AtoNormativoService.BuscarAtoNormativo, model.SeqAtoNormativo.Value);
            return PartialView("_Cabecalho", modelHeader);
        }

        [SMCAuthorize(UC_ORG_003_03_03.PESQUISAR_ASSOCIACAO_ENTIDADE)]
        public JsonResult BuscarTokenTipoEntidade(long SeqTipoEntidade)
        {
            var retorno = TipoEntidadeService.BuscarTokenTipoEntidade(SeqTipoEntidade);
            return Json(retorno);
        }

        [SMCAuthorize(UC_ORG_003_03_04.MANTER_ASSOCIACAO_ENTIDADE)]
        public JsonResult BuscarTokenTipoEntidadeSelect(long? LookupEntidade)
        {
            string tokenTipoEntidade = string.Empty;
            if (LookupEntidade.HasValue)
            {
                var filtro = new EntidadeFiltroData() { Seq = LookupEntidade };
                SMCPagerData<EntidadeListaData> retorno = EntidadeService.BuscarEntidades(filtro);
                tokenTipoEntidade = retorno.Itens.Select(s => s.TokenTipoEntidade).FirstOrDefault();
            }
            return Json(tokenTipoEntidade);
        }

        [SMCAuthorize(UC_ORG_003_03_04.MANTER_ASSOCIACAO_ENTIDADE)]
        public JsonResult EntidadeCursoOfertaLocalidadeExigeGrau(long? LookupEntidade)
        {
            var tokenTipoEntidade = false;
            if (LookupEntidade.HasValue)
                tokenTipoEntidade = CursoOfertaLocalidadeService.CursoOfertaLocalidadeExigeGrau(LookupEntidade.Value);
            return Json(tokenTipoEntidade);
        }

        [SMCAuthorize(UC_ORG_003_03_04.MANTER_ASSOCIACAO_ENTIDADE)]
        public JsonResult BuscarGrauAcademicoSelect(string TokenTipoEntidade, long? LookupEntidade, long? SeqAtoNormativo, long Seq)
        {
            var grauAcademico = new List<SMCDatasourceItem>();
            if (TokenTipoEntidade == TOKEN_TIPO_ENTIDADE.CURSO_OFERTA_LOCALIDADE)
                grauAcademico = GrauAcademicoService.BuscarGrauAcademicoPorEntidade(LookupEntidade, SeqAtoNormativo, Seq);
            return Json(grauAcademico);
        }
    }
}