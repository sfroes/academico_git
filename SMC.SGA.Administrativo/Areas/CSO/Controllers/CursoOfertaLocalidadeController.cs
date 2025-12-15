using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CSO.Models;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class CursoOfertaLocalidadeController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService
        {
            get { return this.Create<ICursoOfertaLocalidadeService>(); }
        }

        private ICursoOfertaService CursoOfertaService
        {
            get { return this.Create<ICursoOfertaService>(); }
        }

        private IInstituicaoNivelService InstituicaoNivelService
        {
            get { return this.Create<IInstituicaoNivelService>(); }
        }

        #endregion [ Serviços ]

        public ActionResult CabecalhoCursoOfertaLocalidade(SMCEncryptedLong seqEntidade)
        {
            var headerModel = this.CursoOfertaLocalidadeService.BurcarCursoOfertaLocalidadeCabecalhoPorCursoUnidade(seqEntidade, true).Transform<CursoOfertaLocalidadeCabecalhoViewModel>();
            return PartialView("_Cabecalho", headerModel);
        }

        /// <summary>
        /// Recupera a mascara de curso oferta localidade segundo a regra RN_CSO_027
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial da oferta de curso</param>
        /// <param name="seqLocalidade">Sequencial o item de hierarquia da localidade</param>
        /// <param name="nome">Nome atual</param>
        /// <returns>Caso sejam informadas a oferta e a localidade retorna a mascara segundo a regra RN_CSO_027 caso contrário o nome atual</returns>
        [SMCAuthorize(UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE)]
        public ActionResult RecuperarMascaraCursoOfertaLocalidade(long? seqCursoOferta, long? seqLocalidade)
        {
            var mascaraNome = this.CursoOfertaLocalidadeService.RecuperarMascaraCursoOfertaLocalidade(seqCursoOferta, seqLocalidade);
            return Json(mascaraNome);
        }

        [SMCAuthorize(UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE)]
        public ActionResult RecuperarFormacaoEspecificaCursoOferta(long? seqcursoOferta)
        {
            var seqFormacaoEspecifica = this.CursoOfertaService.BuscarCursoOferta(seqcursoOferta ?? 0)?.SeqFormacaoEspecifica;
            return Content(seqFormacaoEspecifica?.ToString());
        }

        [SMCAuthorize(UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE)]
        public ActionResult BuscarLocalidadesPorModalidadeSelect(long? seqModalidade, long? seqInstituicaoNivel, long? seqCursoUnidade, bool desativarfiltrosHierarquia)
        {
            var result = this.CursoOfertaLocalidadeService.BuscarLocalidadesPorModalidadeSelect(seqModalidade, seqInstituicaoNivel, seqCursoUnidade, desativarfiltrosHierarquia);

            return Json(result);
        }

        [SMCAuthorize(UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE)]
        public ActionResult BuscarTipoOrgaoReguladorInstituicaoNivelEnsino(long SeqCurso, long SeqNivelEnsino, long SeqInstituicaoEnsino)
        {
            return Json(this.InstituicaoNivelService.BuscarTipoOrgaoReguladorInstituicaoNivelEnsino(SeqNivelEnsino, SeqInstituicaoEnsino));
        }

        [SMCAuthorize(UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE)]
        public ActionResult TipoOrgaoReguladorMEC(TipoOrgaoRegulador SeqTipoOrgaoRegulador)
        {
            return Json(SeqTipoOrgaoRegulador == TipoOrgaoRegulador.MEC);
        }
    }
}