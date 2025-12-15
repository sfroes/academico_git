using System;
using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Linq;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class InstituicaoNivelTipoMembroBancaService : SMCServiceBase, IInstituicaoNivelTipoMembroBancaService
    {
        #region [ DomainService ]

        private InstituicaoNivelTipoMembroBancaDomainService InstituicaoNivelTipoMembroBancaDomainService { get => Create<InstituicaoNivelTipoMembroBancaDomainService>(); }

        #endregion [ DomainService ]
        public List<SMCDatasourceItem> BuscarTiposMembroBancaSelect(TipoMembroBancaFiltroData filtros)
        {
            return InstituicaoNivelTipoMembroBancaDomainService.BuscarTiposMembroBancaSelect(filtros.Transform<InstituicaoNivelTipoMembroBancaFilterSpecification>());
        }
    }
}