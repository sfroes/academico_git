using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class InstituicaoLocalidadeAgendaService : SMCServiceBase, IInstituicaoLocalidadeAgendaService
    {
        #region Domain Service

        private InstituicaoLocalidadeAgendaDomainService InstituicaoLocalidadeAgendaDomainService
        {
            get { return this.Create<InstituicaoLocalidadeAgendaDomainService>(); }
        }

        #endregion Domain Service

        public void ExcluirInstituicaoLocalidadeAgenda(long seqInstituicaoLocalidadeAganda)
        {
            this.InstituicaoLocalidadeAgendaDomainService.ExcluirInstituicaoLocalidadeAgenda(seqInstituicaoLocalidadeAganda);
        }

        public long SalvarInstituicaoLocalidadeAgenda(InstituicaoLocalidadeAgendaData modelo)
        {
            return this.InstituicaoLocalidadeAgendaDomainService.SalvarInstituicaoLocalidadeAgenda(modelo.Transform<InstituicaoLocalidadeAgendaVO>());
        }
    }
}