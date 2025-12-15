using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;
using SMC.Framework.Specification;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class SituacaoEntidadeService : SMCServiceBase, ISituacaoEntidadeService
    {
        #region Domain Service

        private SituacaoEntidadeDomainService SituacaoEntidadeDomainService
        {
            get { return this.Create<SituacaoEntidadeDomainService>(); }
        }

        #endregion Domain Service

        public SituacaoEntidadeData BuscarSituacaoEntidade(long seqSituacaoEntidade)
        {
            return this.SituacaoEntidadeDomainService.SearchByKey(new SMCSeqSpecification<SituacaoEntidade>(seqSituacaoEntidade)).Transform<SituacaoEntidadeData>();
        }
    }
}