using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IGrupoDocumentoRequeridoService : ISMCService
    {
        GrupoDocumentoRequeridoData BuscarGrupoDocumentoRequerido(long seqGrupoDocumentoRequerido);

        SMCPagerData<GrupoDocumentoRequeridoListarData> BuscarGruposDocumentosRequeridos(GrupoDocumentoRequeridoFiltroData filtro);

        List<SMCDatasourceItem> BuscarDocumentosRequeridosSelect(bool uploadObrigatorio, long seqConfiguracaoEtapa);

        long Salvar(GrupoDocumentoRequeridoData modelo);

        void ValidarModeloSalvar(GrupoDocumentoRequeridoData modelo);

        void Excluir(long seq);
    }
}
