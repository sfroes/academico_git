using SMC.Academico.Common.Constants;
using SMC.Academico.ServiceContract.Areas.ALN.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.ServiceModel;

namespace SMC.Academico.ServiceContract.Areas.ALN.Interfaces
{
    [ServiceContract(Namespace = NAMESPACES.SERVICE)]    
    public interface IPessoaAtuacaoTermoIntercambioService : ISMCService
    {
        /// <summary>
        /// Lista o(s) período(s) de Intercâmbio baseado no filtro
        /// </summary>
        SMCPagerData<PessoaAtuacaoTermoIntercambiListaData> BuscarPessoaAtuacaoTermoIntercambio(PessoaAtuacaoTermoIntercambioFiltroData filtros);
        
        /// <summary>
        /// Busca o período de Intercâmbio.
        /// </summary>
        PessoaAtuacaoTermoIntercambioPeriodoData BuscarPeriodoIntercambio(long seq);

        /// <summary>
        /// Salva o período de Intercâmbio.
        /// </summary>
        long SalvarPeriodoIntercambio(PessoaAtuacaoTermoIntercambioSalvarPeriodoData periodo);
    }
} 