using System;
using System.Collections.Generic;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.ServiceContract.Areas.PES.Data;
using SMC.Academico.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;


namespace SMC.Academico.Service.Areas.PES.Services
{
    public class TipoFuncionarioService : SMCServiceBase, ITipoFuncionarioService
    {
        #region [ DomainServices ]

        private TipoFuncionarioDomainService TipoFuncionarioDomainService => this.Create<TipoFuncionarioDomainService>();

        #endregion [ DomainServices ]

        /// <summary>
        /// Buscar todos os tipos de funcionários para select
        /// </summary>
        /// <returns>Lista de todos os funcionários</returns>
        public List<SMCDatasourceItem> BuscarTiposFuncionarioSelect()
        {
            return this.TipoFuncionarioDomainService.BuscarTiposFuncionarioSelect();
        }

    }
}