using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CAM.DomainServices
{
    public class CampanhaCicloLetivoDomainService : AcademicoContextDomain<CampanhaCicloLetivo>
    {
        public List<SMCDatasourceItem> BuscarCampanhasCicloLetivoSelect()
        {
            return SearchAll(a => a.CicloLetivo.Descricao, a => a.CicloLetivo).Select(p => new SMCDatasourceItem() { Seq = p.Seq, Descricao = p.CicloLetivo.Descricao }).ToList();
        }

        /// <summary>
        /// Busca um campanha ciclo letivo pelos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Campanha ciclo letivo encontrada</returns>
        public CampanhaCicloLetivo BuscarCampanhaCicloLetivo(CampanhaCicloLetivoFilterSpecification filtros)
        {
            return this.SearchByKey(filtros);
        }
    }
}