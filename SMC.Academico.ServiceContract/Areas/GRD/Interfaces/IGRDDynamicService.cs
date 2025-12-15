using SMC.Academico.Common.Constants;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.GRD.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IGRDDynamicService : ISMCService
    {
    }
}
