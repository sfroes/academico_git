using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CNC.Interfaces
{
    public interface IDocumentoConclusaoApostilamentoService : ISMCService
    {
        DocumentoConclusaoApostilamentoData BuscarDocumentoConclusaoApostilamento(long seq);

        SMCPagerData<DocumentoConclusaoApostilamentoListarData> BuscarDocumentosConclusaoApostilamento(DocumentoConclusaoApostilamentoFiltroData filtro);

        long Salvar(DocumentoConclusaoApostilamentoData modelo);

        void Excluir(long seq);
    }
}
