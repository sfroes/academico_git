using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class CursoOfertaController : SMCDynamicControllerBase
    {
        #region Serviços

        private ICursoService CursoService
        {
            get { return this.Create<ICursoService>(); }
        }

        private IGrauAcademicoService GrauAcademicoService
        {
            get { return this.Create<IGrauAcademicoService>(); }
        }

        private ICursoOfertaService CursoOfertaService
        {
            get { return this.Create<ICursoOfertaService>(); }
        }

        private IFormacaoEspecificaService FormacaoEspecificaService
        {
            get { return this.Create<IFormacaoEspecificaService>(); }
        }

        private ICursoFormacaoEspecificaService CursoFormacaoEspecificaService
        {
            get { return this.Create<ICursoFormacaoEspecificaService>(); }
        }

        #endregion Serviços

        public ActionResult CabecalhoCursoOferta(CursoOfertaDynamicModel model)
        {
            // Busca as informações do curso para o cabeçalho
            CursoOfertaCabecalhoViewModel modelHeader = ExecuteService<CursoData, CursoOfertaCabecalhoViewModel>(CursoService.BuscarCurso, model.SeqCurso);

            return PartialView("_Cabecalho", modelHeader);
        }

        [SMCAuthorize(UC_CSO_001_01_03.MANTER_OFERTA_CURSO)]
        public ActionResult RecuperarNomeCursoOferta(SMCEncryptedLong seqFormacaoEspecifica, SMCEncryptedLong seqCurso)
        {
            var descricaoFormacao = this.CursoOfertaService.RecuperarMascaraCursoOferta(seqFormacaoEspecifica, seqCurso);
            return Json(descricaoFormacao);
        }
    }
}