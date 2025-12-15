using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.Framework.UI.Mvc.Security;
using SMC.SGA.Administrativo.Areas.CSO.Views.CursoUnidade.App_LocalResources;
using System;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Controllers
{
    public class CursoUnidadeController : SMCDynamicControllerBase
    {
        #region [ Serviços ]

        private ICursoOfertaLocalidadeService CursoOfertaLocalidadeService
        {
            get { return this.Create<ICursoOfertaLocalidadeService>(); }
        }

        private ICursoUnidadeService CursoUnidadeService
        {
            get { return this.Create<ICursoUnidadeService>(); }
        }

        #endregion [ Serviços ]

        [SMCAuthorize(UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE)]
        public ActionResult ExcluirCursoOfertaLocalidade(SMCEncryptedLong seq)
        {
            try
            {
                CursoOfertaLocalidadeService.ExcluirCursoOfertaLocalidade(seq);

                string msg = string.Format(UIResource.MSG_Excluido_Sucesso, "Registro");

                // Seta mensagem de sucesso
                SetSuccessMessage(msg, UIResource.MSG_Titulo_Sucesso, SMCMessagePlaceholders.Default);

                // Renderiza a lista novamente
                return SMCRedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Necessário pois como não tem view a action de excluir, devemos redirecionar para index afim de exibir a mensagem de erro
                ex = ex.SMCLastInnerException();
                this.ThrowRedirect(ex, "index", null);
                throw ex;
            }
        }

        public ActionResult Legenda()
        {
            return PartialView("_Legenda");
        }

        /// <summary>
        /// Recupera a mascara de curso unidade segundo a regra RN_CSO_026
        /// </summary>
        /// <param name="seqCursoHidden">Sequencial do curso</param>
        /// <param name="seqUnidade">Sequencial da undiade</param>
        /// <param name="nome">Nome atual</param>
        /// <returns>Caso sejam informados o curso e a unidade retorna a mascara segundo a regra RN_CSO_026 caso contrário o nome atual</returns>
        [SMCAuthorize(UC_CSO_001_02_03.MANTER_CURSO_OFERTA_LOCALIDADE)]
        public ActionResult RecuperarMascaraCursoUnidade(long? seqCursoHidden, long? seqUnidade, string nome)
        {
            if (!seqCursoHidden.HasValue || !seqUnidade.HasValue)
                return Json(nome ?? "");

            var mascara = this.CursoUnidadeService.RecuperarMascaraCursoUnidade(seqCursoHidden.Value, seqUnidade.Value);
            return Json(mascara);
        }
    }
}