using SMC.Academico.Common.Areas.SRC.Includes;
using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IConfiguracaoEtapaService : ISMCService
    {
        ConfiguracaoEtapaData BuscarConfiguracaoEtapa(long seqConfiguracaoEtapa, IncludesConfiguracaoEtapa includes);

        string BuscarTokenServicoConfiguracaoEtapa(long seqConfiguracaoEtapa);

        CabecalhoConfiguracaoEtapaData BuscarCabecalhoConfiguracaoEtapa(long seqConfiguracaoEtapa);

        SMCPagerData<ConfiguracaoEtapaListarData> BuscarConfiguracoesEtapa(ConfiguracaoEtapaFiltroData filtro);

        List<SMCDatasourceItem> BuscarPaginasNaoCriadas(long seqConfiguracaoEtapa);

        long Salvar(ConfiguracaoEtapaData modelo);

        void Excluir(long seq);   
    }
}