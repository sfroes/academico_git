using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class TipoAtoNormativoService : SMCServiceBase, ITipoAtoNormativoService
    {
        #region [Domain]

        private TipoAtoNormativoDomainService TipoAtoNormativoDomainService => this.Create<TipoAtoNormativoDomainService>(); 
        
        #endregion

        /// <summary>
        /// Buscar os tipos de atos normativos
        /// </summary>
        /// <param name="filtro">Filtros</param>
        /// <returns>Tipos atos normativos select</returns>
        public List<SMCDatasourceItem> BuscarTiposAtoNormativoSelect(TipoAtoNormativoFiltroData filtro)
        {
            return TipoAtoNormativoDomainService.BuscarTiposAtoNormativoSelect(filtro.Transform<TipoAtoNormativoFilterSpecification>());
        }

        /// <summary>
        /// Buscar tipo ato normativo
        /// </summary>
        /// <param name="seqTipoAtoNormativo">Sequencial tipo ato normativo</param>
        /// <returns>Tipo ato normativo</returns>
        public TipoAtoNormativoData BuscarTipoAtoNormativo(long seqTipoAtoNormativo)
        {
            return this.TipoAtoNormativoDomainService.BuscarTipoAtoNormativo(seqTipoAtoNormativo).Transform<TipoAtoNormativoData>();
        }
    }

}