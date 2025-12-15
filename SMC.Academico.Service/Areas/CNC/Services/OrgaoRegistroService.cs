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
    public class OrgaoRegistroService : SMCServiceBase, IOrgaoRegistroService
    {
        #region [ DomainService ]

        private OrgaoRegistroDomainService OrgaoRegistroDomainService => Create<OrgaoRegistroDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca a lista de grupos de registros da instituição de ensino logada para popular um Select
        /// </summary>
        /// <returns>Lista de grupos de registros/returns>
        public List<SMCDatasourceItem> BuscarOrgaosRegistroSelect()
        {
            return OrgaoRegistroDomainService.BuscarOrgaosRegistroSelect();
        }

    }
}
