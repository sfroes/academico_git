using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IArquivoSecaoPaginaService : ISMCService
    {
        bool ValidaConfigurarArquivoSecaoReadOnly(long seqConfiguracaoEtapaPagina);

        List<ArquivoSecaoPaginaData> BuscarArquivosSecaoPagina(ArquivoSecaoPaginaFiltroData filtro);

        void Salvar(long seqConfiguracaoEtapaPagina, long seqSecaoSGF, List<ArquivoSecaoPaginaData> secaoArquivos);
    }
}
