using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.PES.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IMensagemService : ISMCService
    {
        [OperationContract]
        SMCPagerData<MensagemListaData> ListarMensagens(MensagemFiltroData filtro);
    }
}
