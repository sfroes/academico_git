using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Security;
using System.Web.Mvc;
using System.Web.Routing;

namespace SMC.SGA.Administrativo.Areas.ORG.Controllers
{
    public class HierarquiaEntidadeController : SMCDynamicControllerBase
    {
        #region Services

        private ITipoHierarquiaEntidadeItemService TipoHierarquiaEntidadeItemService
        {
            get { return this.Create<ITipoHierarquiaEntidadeItemService>(); }
        }

        private IHierarquiaEntidadeService HierarquiaEntidadeService
        {
            get { return this.Create<IHierarquiaEntidadeService>(); }
        }

        #endregion Services

        #region TreeView

        /// <summary>
        /// View para a montagem da árvore
        /// </summary>
        /// <param name="seqHierarquiaEntidade">Sequencial da hierarquia de entidade</param>
        /// <returns></returns>
        [SMCAuthorize(UC_ORG_001_07_03.MONTAR_HIERARQUIA_ENTIDADE)]
        public ActionResult VisaoArvore(SMCEncryptedLong seqHierarquiaEntidade, SMCEncryptedLong seqTipoHierarquiaEntidade)
        {
            TratarSeqTipoHierarquiaEntidade(seqHierarquiaEntidade, ref seqTipoHierarquiaEntidade);

            if (!TipoHierarquiaEntidadeItemService.TipoHierarquiaEntidadePossuiFilhos(seqTipoHierarquiaEntidade))
                return ThrowRedirect<TipoHierarquiaEntidadeNaoPossuiFilhosException>("Index", null, new RouteValueDictionary(new { area = "ORG", controller = "HierarquiaEntidade", }));
            else
                return RedirectToRoute(new RouteValueDictionary(new { area = "ORG", controller = "HierarquiaEntidadeItem", action = "Index", seqHierarquiaEntidade = seqHierarquiaEntidade }));
        }

        private void TratarSeqTipoHierarquiaEntidade(SMCEncryptedLong seqHierarquiaEntidade, ref SMCEncryptedLong seqTipoHierarquiaEntidade)
        {
            if (seqTipoHierarquiaEntidade == null)
            {
                HierarquiaEntidadeData HierarquiaEntidade = HierarquiaEntidadeService.BuscarHierarquiaEntidade(seqHierarquiaEntidade); //.HierarquiaEntidadeDomainService.SearchByKey<HierarquiaEntidade, HierarquiaEntidade>(seqHierarquiaEntidade);
                if (HierarquiaEntidade != null)
                    seqTipoHierarquiaEntidade = new SMCEncryptedLong(HierarquiaEntidade.SeqTipoHierarquiaEntidade);
            }
        }

        #endregion TreeView
    }
}