using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Service;
using SMC.Framework.Extensions;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Framework.Specification;
using SMC.Framework.Mapper;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class TipoHierarquiaEntidadeService : SMCServiceBase, ITipoHierarquiaEntidadeService
    {
        #region Serviço de Dominio

        internal TipoHierarquiaEntidadeDomainService TipoHierarquiaEntidadeDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeDomainService>(); }
        }

        internal TipoHierarquiaEntidadeItemDomainService TipoHierarquiaEntidadeItemDomainService
        {
            get { return this.Create<TipoHierarquiaEntidadeItemDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca os dados de um tipo de hierarquia de entidade
        /// </summary>
        /// <param name="seq">Sequencial do tipo de hierarquia de entidade</param>
        /// <returns>Informações do tipo de hierarquia de entidade</returns>
        public TipoHierarquiaEntidadeData BuscarTipoHierarquiaEntidade(long seq)
        {
            return TipoHierarquiaEntidadeDomainService.SearchByKey<TipoHierarquiaEntidade, TipoHierarquiaEntidadeData>(seq);
        }

        /// <summary>
        /// Busca o item de hierarquia de entidade a partir do sequencial
        /// </summary>
        /// <param name="seqTipoHierarquiaEntidadeItem">Sequencial da TipoHierarquiaEntidadeItem a ser retornado</param>
        /// <returns>TipoHierarquiaEntidadeItems que atendem ao filtro</returns>
        public TipoHierarquiaEntidadeItemData BuscarTipoHierarquiaEntidadeItem(long seqTipoHierarquiaEntidadeItem)
        {
            IncludesTipoHierarquiaEntidadeItem includes = IncludesTipoHierarquiaEntidadeItem.TipoEntidade;
            var specItem = new SMCSeqSpecification<TipoHierarquiaEntidadeItem>(seqTipoHierarquiaEntidadeItem);
            TipoHierarquiaEntidadeItem item = TipoHierarquiaEntidadeItemDomainService.SearchByKey(specItem, includes);
            return SMCMapperHelper.Create<TipoHierarquiaEntidadeItemData>(item);
        }
    }
}
