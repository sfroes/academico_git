using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface IHierarquiaEntidadeItemService : ISMCService
    {
        /// <summary>
        /// Salva um item de uma árvore de hierarquia entidade na base
        /// </summary>
        /// <param name="hierarquiaEntidadeItemData">Hierarquia entidade item a ser salvo</param>
        /// <returns>Sequencial da hierarquia entidade item salvo</returns>
        long SalvarHierarquiaEntidadeItem(HierarquiaEntidadeItemData hierarquiaEntidadeItemData);

        /// <summary>
        /// Monta o select a partir da seqHierarquiaEntidade e seqTipoHierarquiaEntidadeItem informados
        /// </summary>
        /// <param name="hierarquiaEntidadeItem">Hierarquia entidade item node</param>
        /// <returns>Lista de tipo hierarquia entidade item para o combo</returns>
        List<SMCDatasourceItem> BuscarTipoHierarquiaEntidadeItemSelect(HierarquiaEntidadeItemNodeData hierarquiaEntidadeItem);

        /// <summary>
        /// Busca a árvore de acordo com o SeqHierarquiaEntidade
        /// </summary>
        /// <param name="SeqHierarquiaEntidade">Sequencial da hierarquia de entidade</param>
        /// <returns>Dados dos nós da hierarquia</returns>
        HierarquiaEntidadeItemNodeData[] BuscarHierarquiaEntidadeItens(long seqHierarquiaEntidade);

        /// <summary>
        /// Busca a árvore de acordo com o filtro para o lookup
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Array de hierarquia entidade item para árvore</returns>
        HierarquiaEntidadeItemNodeData[] BuscarHierarquiaEntidadeItemLookup(HierarquiaEntidadeItemFiltroData filtro);

        /// <summary>
        /// Excluí um item de hierarquia e seus filhos caso este não seja pai de itens externalizados conforme a regra RN_ORG_012.
        /// </summary>
        /// <param name="seq">Sequencial do item a ser excluído</param>
        /// <exception cref="HierarquiaEntidadeItemExclusaoExternalizadaException">Caso o item ou algum de seus filhos seja externalizado</exception>
        void ExcluirHierarquiaEntidadeItem(long seq);

        /// <summary>
        /// Busca um item de hierarquia sem dependências pela chave
        /// </summary>
        /// <param name="seq">Sequencial do item</param>
        /// <returns>Dados do item</returns>
        HierarquiaEntidadeItemData BuscarHierarquiaEntidadeItem(long seq);

        /// <summary>
        /// Buscar filhos da entidade na visão organizacional sem recursividade, sendo somente o primeiro nivel
        /// </summary>
        /// <param name="visao">Tipo da visão</param>
        /// <param name="seqEntidade">Sequencial da ENTIDADE</param>
        /// <returns>Sequenciais das entidades filhas</returns>
        List<long> BuscarHierarquiaEntidadesItensFilhas(long seqEntidade);

        /// <summary>
        /// Buscar sequencial da hierarquia item por entidade por uma visão
        /// </summary>
        /// <param name="seqEntidade">Sequencial da ENTIDADE</param>
        /// <returns>Sequenciais da hierarquia item</returns>
        long BuscarHierarquiaEntidadesItemPorEntidade(long seqEntidade);

        /// <summary>
        /// Busca as entidades filhas da entidade responsavel na visa organizacional
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial entidade responsavel</param>
        /// <returns>
        /// Caso a entidade seja um grupo de programa retorna hierarquia item das filhas da entidade
        /// Caso contrario retorna a hierarquia item dela mesma
        /// </returns>
        List<long> BuscarHierarquiaEntidadesItemFilhasEntidadeVinculo(long seqEntidadeVinculo);
    }
}