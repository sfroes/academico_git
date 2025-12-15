using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class TipoHierarquiaEntidadeItemService : SMCServiceBase, ITipoHierarquiaEntidadeItemService
    {
        #region Services

        private TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        #endregion Services

        public bool TipoHierarquiaEntidadePossuiFilhos(long SeqTipoHierarquiaEntidade)
        {
            TipoHierarquiaEntidadeItemVerificarFilhosSpecification spec = new TipoHierarquiaEntidadeItemVerificarFilhosSpecification(SeqTipoHierarquiaEntidade);
            return TipoHierarquiaEntidadeItemDomainService.Count(spec) > 0;
        }
    }
}