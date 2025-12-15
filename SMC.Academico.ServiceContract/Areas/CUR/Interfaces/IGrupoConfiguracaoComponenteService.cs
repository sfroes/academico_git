using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IGrupoConfiguracaoComponenteService : ISMCService
    {
        /// <summary>
        /// Buscar os grupos de configurações do componente com itens de configuração para listagem
        /// </summary>  
        /// <param name="filtros">Sequencial do componente curricular sendo o unico filtro</param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        SMCPagerData<GrupoConfiguracaoComponenteData> BuscarGruposConfiguracoesComponentes(GrupoConfiguracaoComponenteFiltroData filtros);

        /// <summary>
        /// Salvar o grupo de configuração de componente com seus respectivos itens
        /// </summary>
        /// <param name="grupoConfiguracaoComponenteData">Dados do grupo configuração componente</param>
        /// <returns>Sequencial do grupo configuração componente</returns>
        long SalvarGrupoConfiguracaoComponente(GrupoConfiguracaoComponenteData grupoConfiguracaoComponenteData);

        /// <summary>
        /// Buscar os grupos de configurações do componente compartilhados para um configuração
        /// </summary>  
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente</param>
        /// <returns>Lista de grupos com itens configuração do tipo compartilhado</returns>
        List<GrupoConfiguracaoComponenteData> BuscarGrupoConfiguracaoComponentePorConfiguracaoCompartilhado(long seqConfiguracaoComponente);


        void ExcluirGrupoConfiguracaoComponente(GrupoConfiguracaoComponenteData grupoConfiguracaoComponenteData);
    }
}
