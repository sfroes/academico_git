using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface ISolicitacaoServicoBoletoTituloService : ISMCService
    {
        List<TaxasSolicitacaoData> BuscarTaxasTitulosPorSolicitacao(long seqSolicitacaoServico);

        List<TitulosSolicitacaoData> BuscarTitulosPorSolicitacao(long seqSolicitacaoServico);
    }
}
