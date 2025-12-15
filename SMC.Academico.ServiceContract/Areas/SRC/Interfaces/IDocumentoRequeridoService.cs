using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IDocumentoRequeridoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarTiposDocumentoSelect();
        
        List<SMCDatasourceItem> BuscarTiposDocumentoPorServicoSelect(long seqServico);

        DocumentoRequeridoData BuscarDocumentoRequerido(long seqDocumentoRequerido);

        SMCPagerData<DocumentoRequeridoListarData> BuscarDocumentosRequeridos(DocumentoRequeridoFiltroData filtro);

        long Salvar(DocumentoRequeridoData modelo);

        void ValidarModeloSalvar(DocumentoRequeridoData modelo);

        void Excluir(long seq);

        DocumentoRequeridoData BuscarDescricaoDocumentoRequeridoPermiteVarios(long seqDocumentoRequerido);
    }
}
