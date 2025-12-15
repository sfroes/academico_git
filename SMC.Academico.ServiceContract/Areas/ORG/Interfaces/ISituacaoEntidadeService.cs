using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface ISituacaoEntidadeService : ISMCService
    {
        SituacaoEntidadeData BuscarSituacaoEntidade(long SeqSituacaoEntidade);
    }
}