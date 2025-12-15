using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IConfiguracaoEtapaPaginaService : ISMCService
    {
        ConfiguracaoEtapaPaginaData BuscarConfiguracaoEtapaPrimeiraPagina(long seqConfiguracaoEtapa);
      
        bool VerificarConfiguracaoPossuiPaginas(long seqConfiguracaoEtapa);

        bool VerificarConfiguracaoPossuiPaginaAnexarDocumento(long seqConfiguracaoEtapa);

        bool VerificarConfiguracaoPossuiPaginaRegistrarDocumento(long seqConfiguracaoEtapa);

        bool VerificarConfiguracaoPossuiPaginaConfiguracaoDocumentoUpload(long seqConfiguracaoEtapa);

        bool VerificarConfiguracaoPossuiPaginaConfiguracaoDocumentoRegistro(long seqConfiguracaoEtapa);

        ConfiguracaoEtapaPaginaData BuscarConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina);

        List<SMCDatasourceItem> BuscarPaginasPorEtapa(long seqEtapaSgf);

        List<SMCDatasourceItem> BuscarFormularios();

        List<SMCDatasourceItem> BuscarVisoesPorFormularioSelect(long seqTipoFormulario);

        List<NoArvoreConfiguracaoEtapaData> BuscarArvoreConfiguracaoEtapa(ConfiguracaoEtapaPaginaFiltroData filtro);

        void DuplicarConfiguracaoEtapaPagina(long seqConfiguracaoEtapaPagina);

        void AdicionarPaginas(long seqConfiguracaoEtapa, List<(long, ConfiguracaoDocumento?)> seqsPaginasEConfiguracoesDocumento); //IEnumerable<long> seqPaginasSGF);

        long Salvar(ConfiguracaoEtapaPaginaData modelo);

        void Excluir(long seq);

        void ValidarModeloExcluir(long seq);
        List<ConfiguracaoEtapaPaginaData> BuscarConfiguracaoEtapaPaginaPorSeqProcessoEtapa(long seqProcessoEtapa);
        List<ConfiguracaoEtapaPaginaData> BuscarConfiguracoesEtapaPaginaPorSeqConfiguracaoEtapa(long seqConfiguracaoEtapa);


    }
}