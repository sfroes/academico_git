using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IInstituicaoLocalidadeAgendaService : ISMCService
    {
        void ExcluirInstituicaoLocalidadeAgenda(long seqInstituicaoLocalidadeAganda);

        long SalvarInstituicaoLocalidadeAgenda(InstituicaoLocalidadeAgendaData modelo);
    }
}