using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic; 
using SMC.Academico.ServiceContract.Areas.FIN.Data;

namespace SMC.Academico.ServiceContract.Areas.FIN.Interfaces
{
    public interface ITermoAdesaoService : ISMCService
    {
        TermoAdesaoCabecalhoData BuscarCabecalhoTermoAdesao(long seqContrato);

        SMCPagerData<TermoAdesaoListarData> ListarTermosAdesao(TermoAdesaoFiltroData filtro);

        TermoAdesaoData BuscarTermoAdesao();

        long SalvarTermoAdesao(TermoAdesaoData termo);
    }
}

 