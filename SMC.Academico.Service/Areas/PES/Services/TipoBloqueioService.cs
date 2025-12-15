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
    public class TipoBloqueioService : SMCServiceBase, ITipoBloqueioService
    {
        #region [ DomainServices ]

        private TipoBloqueioDomainService TipoBloqueioDomainService
        {
            get { return this.Create<TipoBloqueioDomainService>(); }
        }

        #endregion [ DomainServices ]

        public List<SMCDatasourceItem> BuscarTiposBloqueiosSelect()
        {
            return this.TipoBloqueioDomainService.SearchAll().TransformList<SMCDatasourceItem>();
        }


    }
}