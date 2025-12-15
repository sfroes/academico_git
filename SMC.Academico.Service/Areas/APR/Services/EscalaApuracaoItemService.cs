using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class EscalaApuracaoItemService : SMCServiceBase, IEscalaApuracaoItemService
    {
        #region [ DomainService ]

        private EscalaApuracaoDomainService EscalaApuracaoDomainService => Create<EscalaApuracaoDomainService>();

        private EscalaApuracaoItemDomainService EscalaApuracaoItemDomainService => Create<EscalaApuracaoItemDomainService>();

        #endregion [ DomainService ]

        public List<EscalaApuracaoItemData> BuscarEscalaApuracaoItens(long seqEscalaApuracao)
        {
            return EscalaApuracaoDomainService.SearchProjectionByKey(seqEscalaApuracao, 
                    x => x.Itens.Select(i => new EscalaApuracaoItemData
                    {   
                        Descricao = i.Descricao,
                        PercentualMinimo = i.PercentualMinimo,
                        PercentualMaximo = i.PercentualMaximo
                    })).ToList();
        }

        /// <summary>
        /// Busca os itens de uma escala de apuração
        /// </summary>
        /// <param name="filtroData">Dados de filtro</param>
        /// <returns>Dados dos itens da escala de apuração</returns>
        public List<SMCDatasourceItem> BuscarEscalaApuracaoItensSelect(EscalaApuracaoItemFiltroData filtroData)
        {
            return EscalaApuracaoItemDomainService.BuscarEscalaApuracaoItensSelect(filtroData.Transform<EscalaApuracaoItemFilterSpecification>());
        }
    }
}