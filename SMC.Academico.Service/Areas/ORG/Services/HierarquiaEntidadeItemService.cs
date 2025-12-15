using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class HierarquiaEntidadeItemService : SMCServiceBase, IHierarquiaEntidadeItemService
    {
        #region [ DomainService ]

        private HierarquiaEntidadeDomainService HierarquiaEntidadeDomainService
        {
            get { return this.Create<HierarquiaEntidadeDomainService>(); }
        }

        private HierarquiaEntidadeItemDomainService HierarquiaEntidadeItemDomainService
        {
            get { return this.Create<HierarquiaEntidadeItemDomainService>(); }
        }

        private TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        private EntidadeDomainService EntidadeDomainService
        {
            get { return this.Create<EntidadeDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Salva um item de uma árvore de hierarquia entidade na base
        /// </summary>
        /// <param name="hierarquiaEntidadeItemData">Hierarquia entidade item a ser salvo</param>
        /// <returns>Sequencial da hierarquia entidade item salvo</returns>
        public long SalvarHierarquiaEntidadeItem(HierarquiaEntidadeItemData hierarquiaEntidadeItemData)
        {
            // Item a ser inserido
            HierarquiaEntidadeItem hierarquiaEntidadeItem = SMCMapperHelper.Create<HierarquiaEntidadeItem>(hierarquiaEntidadeItemData);
            return HierarquiaEntidadeItemDomainService.SalvarHierarquiaEntidadeItem(hierarquiaEntidadeItem);
        }

        /// <summary>
        /// Monta o select a partir da seqHierarquiaEntidade e seqTipoHierarquiaEntidadeItem informados
        /// </summary>
        /// <param name="hierarquiaEntidadeItem">Hierarquia entidade item node</param>
        /// <returns>Lista de tipo hierarquia entidade item para o combo</returns>
        public List<SMCDatasourceItem> BuscarTipoHierarquiaEntidadeItemSelect(HierarquiaEntidadeItemNodeData hierarquiaEntidadeItem)
        {
            //Alterar - no método alterar o seqTipoHierarquiaEntidadeItem do registro corrente é trocado de acordo com o seu pai, pois, o tipo de item a ser editado
            //deverá refernciar o tipo no qual o pai permite e não o filho
            if (hierarquiaEntidadeItem.Seq > 0)
                hierarquiaEntidadeItem.SeqTipoHierarquiaEntidadeItem = hierarquiaEntidadeItem.SeqTipoHierarquiaEntidadeItemPai == 0 ? (long?)null : hierarquiaEntidadeItem.SeqTipoHierarquiaEntidadeItemPai;

            return HierarquiaEntidadeItemDomainService.BuscarTipoHierarquiaEntidadeItemSelect(hierarquiaEntidadeItem.SeqHierarquiaEntidade, hierarquiaEntidadeItem.SeqTipoHierarquiaEntidadeItem);
        }

        /// <summary>
        /// Busca a árvore de acordo com o SeqHierarquiaEntidade
        /// </summary>
        /// <param name="seqHierarquiaEntidade">Sequencial da hierarquia de entidade</param>
        /// <returns>Dados dos nós da hierarquia</returns>
        public HierarquiaEntidadeItemNodeData[] BuscarHierarquiaEntidadeItens(long seqHierarquiaEntidade)
        {
            return this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItens(seqHierarquiaEntidade).TransformToArray<HierarquiaEntidadeItemNodeData>();
        }

        /// <summary>
        /// Busca a árvore de acordo com o filtro para o lookup
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns>Array de hierarquia entidade item para árvore</returns>
        public HierarquiaEntidadeItemNodeData[] BuscarHierarquiaEntidadeItemLookup(HierarquiaEntidadeItemFiltroData filtro)
        {
            var retorno = HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItemLookup(filtro.Transform<HierarquiaEntidadeItemFilterSpecification>()).TransformToArray<HierarquiaEntidadeItemNodeData>();
            return retorno;
        }

        /// <summary>
        /// Excluí um item de hierarquia e seus filhos caso este não seja pai de itens externalizados conforme a regra RN_ORG_012.
        /// </summary>
        /// <param name="seq">Sequencial do item a ser excluído</param>
        /// <exception cref="HierarquiaEntidadeItemExclusaoExternalizadaException">Caso o item ou algum de seus filhos seja externalizado</exception>
        public void ExcluirHierarquiaEntidadeItem(long seq)
        {
            this.HierarquiaEntidadeItemDomainService.ExcluirHierarquiaEntidadeItem(seq);
        }

        /// <summary>
        /// Busca um item de hierarquia sem dependências pela chave
        /// </summary>
        /// <param name="seq">Sequencial do item</param>
        /// <returns>Dados do item</returns>
        public HierarquiaEntidadeItemData BuscarHierarquiaEntidadeItem(long seq)
        {
            return this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadeItem(seq).Transform<HierarquiaEntidadeItemData>();
        }

        /// <summary>
        /// Buscar filhos da entidade na visão organizacional sem recursividade, sendo somente o primeiro nivel
        /// </summary>
        /// <param name="visao">Tipo da visão</param>
        /// <param name="seqEntidade">Sequencial da ENTIDADE</param>
        /// <returns>Sequenciais das entidades filhas</returns>
        public List<long> BuscarHierarquiaEntidadesItensFilhas(long seqEntidade)
        {
            return this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadesItensFilhas(TipoVisao.VisaoOrganizacional, seqEntidade);
        }

        /// <summary>
        /// Buscar sequencial da hierarquia item por entidade por uma visão
        /// </summary>
        /// <param name="seqEntidade">Sequencial da ENTIDADE</param>
        /// <returns>Sequenciais da hierarquia item</returns>
        public long BuscarHierarquiaEntidadesItemPorEntidade(long seqEntidade)
        {
            return this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadesItemPorEntidade(TipoVisao.VisaoOrganizacional, seqEntidade);
        }

        /// <summary>
        /// Busca as entidades filhas da entidade responsavel na visa organizacional
        /// </summary>
        /// <param name="seqEntidadeVinculo">Sequencial entidade responsavel</param>
        /// <returns>
        /// Caso a entidade seja um grupo de programa retorna hierarquia item das filhas da entidade
        /// Caso contrario retorna a hierarquia item dela mesma
        /// </returns>
        public List<long> BuscarHierarquiaEntidadesItemFilhasEntidadeVinculo(long seqEntidadeVinculo)
        {
            return this.HierarquiaEntidadeItemDomainService.BuscarHierarquiaEntidadesItemFilhasEntidadeVinculo(seqEntidadeVinculo);
        }
    }
}