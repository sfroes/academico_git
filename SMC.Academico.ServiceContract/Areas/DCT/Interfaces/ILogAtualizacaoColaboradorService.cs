using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface ILogAtualizacaoColaboradorService : ISMCService
    {
        List<RelatorioLogAtualizacaoColaboradorListaData> BuscarLogsAtualizacoesColaboradoresRelatorio(RelatorioLogAtualizacaoColaboradorFiltroData filtro);
    }
}