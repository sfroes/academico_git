using SMC.Academico.Common.Constants;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ORT.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IORTDynamicService : ISMCService
    {
    }
}