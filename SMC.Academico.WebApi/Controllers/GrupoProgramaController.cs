using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using System.Collections.Generic;
using System.Web.Http;

namespace SMC.Academico.WebApi.Controllers
{
    public class GrupoProgramaController : SMCApiControllerBase
    {
        #region Services
        private IEntidadeService EntidadeService
        {
            get { return Create<IEntidadeService>(); }
        }

        private IInstituicaoNivelService InstituicaoNivelService
        {
            get { return Create<IInstituicaoNivelService>(); }
        }

        #endregion

        public List<SMCDatasourceItem> Get()
        {
            return EntidadeService.BuscarGruposProgramasSelect();
        }

        [HttpGet]
        public List<SMCDatasourceItem> NiveisEnsino(long seqGrupoPrograma)
        {
            return InstituicaoNivelService.BuscarNiveisEnsinoPorEntidadeSelect(seqGrupoPrograma);
        }

    }
}