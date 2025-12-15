using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Specifications;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class GrupoConfiguracaoComponenteService : SMCServiceBase, IGrupoConfiguracaoComponenteService
    {
        #region [ DomainService ]

        private GrupoConfiguracaoComponenteDomainService GrupoConfiguracaoComponenteDomainService
        {
            get { return this.Create<GrupoConfiguracaoComponenteDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Buscar os grupos de configurações do componente com itens de configuração para listagem
        /// </summary>  
        /// <param name="filtros">Sequencial do componente curricular sendo o unico filtro</param>
        /// <returns>SMCPagerData da lista de configuração de componente</returns>
        public SMCPagerData<GrupoConfiguracaoComponenteData> BuscarGruposConfiguracoesComponentes(GrupoConfiguracaoComponenteFiltroData filtros)
        {
            var lista = GrupoConfiguracaoComponenteDomainService.BuscarGruposConfiguracoesComponentes(filtros.Transform<GrupoConfiguracaoComponenteFilterSpecification>());
            return lista.Transform<SMCPagerData<GrupoConfiguracaoComponenteData>>();
        }

        /// <summary>
        /// Salvar o grupo de configuração de componente com seus respectivos itens
        /// </summary>
        /// <param name="grupoConfiguracaoComponenteData">Dados do grupo configuração componente</param>
        /// <returns>Sequencial do grupo configuração componente</returns>
        public long SalvarGrupoConfiguracaoComponente(GrupoConfiguracaoComponenteData grupoConfiguracaoComponenteData)
        {
            return  GrupoConfiguracaoComponenteDomainService.SalvarGrupoConfiguracaoComponente(grupoConfiguracaoComponenteData.Transform<GrupoConfiguracaoComponente>());
        }

        /// <summary>
        /// Buscar os grupos de configurações do componente compartilhados para um configuração
        /// </summary>  
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente</param>
        /// <returns>Lista de grupos com itens configuração do tipo compartilhado</returns>
        public List<GrupoConfiguracaoComponenteData> BuscarGrupoConfiguracaoComponentePorConfiguracaoCompartilhado(long seqConfiguracaoComponente)
        {
            var lista = GrupoConfiguracaoComponenteDomainService.BuscarGrupoConfiguracaoComponentePorConfiguracaoCompartilhado(seqConfiguracaoComponente);
            return lista.TransformList<GrupoConfiguracaoComponenteData>();
        }

        public void ExcluirGrupoConfiguracaoComponente(GrupoConfiguracaoComponenteData grupoConfiguracaoComponenteData)
        {
             GrupoConfiguracaoComponenteDomainService.ExcluirGrupoConfiguracaoComponente(grupoConfiguracaoComponenteData.Transform<GrupoConfiguracaoComponente>());
        }
    }
}
