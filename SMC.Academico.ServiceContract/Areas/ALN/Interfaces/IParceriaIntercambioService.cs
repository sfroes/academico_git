using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]
    public interface IParceriaIntercambioService : ISMCService
    {
        SMCPagerData<ParceriaIntercambioListarData> ListarParceriaIntercambio(ParceriaIntercambioFiltroData filtro);

        ParceriaIntercambioData AlterarParceriaIntercambio(long seq);

        long SalvarParceriaIntercambio(ParceriaIntercambioData modelo);

        ParceriaIntercambioData BuscarParceriaIntercambio(long seqParceriaIntercambio);

        /// <summary>
        /// Exlcuir parceria de intercambio
        /// </summary>
        /// <param name="seq">Sequencial da parceria de intercambio</param>
        void ExcluirParceria(long seq);
    }
}

 