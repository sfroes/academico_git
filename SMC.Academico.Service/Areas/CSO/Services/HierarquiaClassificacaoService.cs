using SMC.Academico.Domain.Areas.CSO.DomainServices;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Specifications;
using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Academico.ServiceContract.Areas.CSO.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CSO.Services
{
    public class HierarquiaClassificacaoService : SMCServiceBase, IHierarquiaClassificacaoService
    {
        #region Serviço de Dominio

        internal HierarquiaClassificacaoDomainService HierarquiaClassificacaoDomainService
        {
            get { return this.Create<HierarquiaClassificacaoDomainService>(); }
        }

        #endregion

        /// <summary>
        /// Busca os dados de hierarquia de classificação
        /// </summary>
        /// <param name="seq">Sequencial de hierarquia de classificação</param>
        /// <returns>Informações de hierarquia de classificação</returns>
        public HierarquiaClassificacaoData BuscarHierarquiaClassificacao(long seq)
        {
            return HierarquiaClassificacaoDomainService.SearchByKey<HierarquiaClassificacao, HierarquiaClassificacaoData>(seq);
        }

        /// <summary>
        /// Retorna o Select de Hierarquia de classificação
        /// </summary>
        /// <param name="seqHierarquiaClassificacao">seqHierarquiaClassificacao pais</param>
        /// <returns></returns>
        public List<SMCDatasourceItem> BuscarHierarquiaClassificacaoSelect(long seqTipoHierarquiaClassificacao)
        {
            HierarquiaClassificacaoFilterSpecification spec = new HierarquiaClassificacaoFilterSpecification()
            {
                SeqTipoHierarquiaClassificacao = seqTipoHierarquiaClassificacao
            };
            return HierarquiaClassificacaoDomainService.SearchBySpecification(spec).TransformList<SMCDatasourceItem>();
        }
    }
}
