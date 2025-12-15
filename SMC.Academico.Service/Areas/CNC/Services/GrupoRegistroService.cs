using SMC.Academico.Domain.Areas.CNC.Data;
using SMC.Academico.Domain.Areas.CNC.DomainServices;
using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CNC.Data;
using SMC.Academico.ServiceContract.Areas.CNC.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CNC.Services
{
    public class GrupoRegistroService : SMCServiceBase, IGrupoRegistroService
    {
        #region [ DomainService ]

        private GrupoRegistroDomainService GrupoRegistroDomainService => Create<GrupoRegistroDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca as grupors de registro que atendam os filtros informados
        /// </summary>
        /// <param name="filtros">Filtros da listagem dos grupos de registro</param>
        /// <returns>Lista de grupos de registros</returns>
        public SMCPagerData<GrupoRegistroData> BuscarGruposRegistros(GrupoRegistroFiltroData filtros)
        {
            return GrupoRegistroDomainService.BuscarGruposRegistros(filtros.Transform<GrupoRegistroFiltroVO>()).Transform<SMCPagerData<GrupoRegistroData>>();
        }

        /// <summary>
        /// Busca a lista de grupos de registros da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de grupos de registros/returns>
        public List<SMCDatasourceItem> BuscarGruposRegistroSelect()
        {
            return GrupoRegistroDomainService.BuscarGruposRegistroSelect();
        }

    }
}
