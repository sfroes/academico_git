using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class CampanhaCicloLetivoService : SMCServiceBase, ICampanhaCicloLetivoService
    {
        #region [ Serviços ]

        private CampanhaCicloLetivoDomainService CampanhaCicloLetivoDomainService
        {
            get { return Create<CampanhaCicloLetivoDomainService>(); }
        }

        #endregion [ Serviços ]

        public List<SMCDatasourceItem> BuscarCampanhasCicloLetivoSelect()
        {
            return CampanhaCicloLetivoDomainService.BuscarCampanhasCicloLetivoSelect();
        }

        /// <summary>
        /// Busca um campanha ciclo letivo pelos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Campanha ciclo letivo encontrada</returns>
        public CampanhaCicloLetivoData BuscarCampanhaCicloLetivo(CampanhaCicloLetivoFiltroData filtros)
        {
            return CampanhaCicloLetivoDomainService.BuscarCampanhaCicloLetivo(filtros.Transform<CampanhaCicloLetivoFilterSpecification>()).Transform<CampanhaCicloLetivoData>();
        }
    }
}