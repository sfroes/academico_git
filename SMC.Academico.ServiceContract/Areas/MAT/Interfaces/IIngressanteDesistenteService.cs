using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.MAT.Data;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.MAT.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IIngressanteDesistenteService : ISMCService
    {
        void BuscarIngressantesDesistentes(IngressanteDesistenteSATData modelo);
    }
}
 