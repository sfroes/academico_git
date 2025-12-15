using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class TipoHierarquiaEntidadeController : SMCDynamicControllerBase
    {
        #region Servicos

        private IInstituicaoTipoEntidadeService InstituicaoTipoEntidadeService
        {
            get { return this.Create<IInstituicaoTipoEntidadeService>(); }
        }

        #endregion Servicos

        /// <summary>
        /// Verifica se a configuração de tipo de entidade está feita para a instituição de ensino logada
        /// Se estiver, chama a montagem da hierarquia. Se não tiver, dispara erro
        /// </summary>
        /// <param name="seqTipoHierarquiaEntidade">Sequencial do tipo de hierarquia de entidade</param>
        [SMCAuthorize(UC_ORG_001_05_03.MONTAR_HIERARQUIA_TIPO_ENTIDADE)]
        public ActionResult VerificaConfiguracaoTipoEntidade(SMCEncryptedLong seqTipoHierarquiaEntidade)
        {
            // Verifica se existem tipos de entidade configurados para a instituição logada
            var tipos = InstituicaoTipoEntidadeService.BuscarTipoEntidadesDaInstituicaoSelect();
            if (tipos.Count <= 0)
            {
                ThrowRedirect<TipoEntidadeNaoAssociadoInstituicaoException>("Index");
            }

            return RedirectToAction("Index", "TipoHierarquiaEntidadeItem", new { SeqTipoHierarquiaEntidade = seqTipoHierarquiaEntidade });
        }
    }
}