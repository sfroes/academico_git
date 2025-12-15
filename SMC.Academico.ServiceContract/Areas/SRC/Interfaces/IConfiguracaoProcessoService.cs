using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IConfiguracaoProcessoService : ISMCService
    {
        ConfiguracaoProcessoData BuscarConfiguracaoProcesso(long seqConfiguracaoProcesso);

        SMCPagerData<ConfiguracaoProcessoListarData> BuscarConfiguracoesProcesso(ConfiguracaoProcessoFiltroData filtro);

        List<SMCDatasourceItem> BuscarConfiguracoesProcessoSelect(ConfiguracaoProcessoFiltroData filtro);

        long Salvar(ConfiguracaoProcessoData modelo);

        void Excluir(long seq);
    }
}
