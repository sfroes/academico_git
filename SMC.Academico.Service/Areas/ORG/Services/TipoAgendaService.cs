using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.ORG.Services
{
    public class TipoAgendaService : SMCServiceBase, ITipoAgendaService
    {
        #region Domain Service

        private TipoAgendaDomainService TipoAgendaDomainService
        {
            get { return this.Create<TipoAgendaDomainService>(); }
        }

        #endregion Domain Service

        public List<SMCDatasourceItem> BuscarTiposAgendaSelect(TipoAgendaFiltroData filtros)
        {
            var spec = filtros.Transform<TipoAgendaFilterSpecification>();

            var result = this.TipoAgendaDomainService.SearchProjectionBySpecification(spec, x => new SMCDatasourceItem() { Seq = x.Seq, Descricao = x.Descricao });

            return result.ToList();
        }

        public long SalvarTipoAgenda(TipoAgendaData modelo)
        {
            return this.TipoAgendaDomainService.SalvarTipoAgenda(modelo.Transform<TipoAgendaVO>());
        }

        public TipoAgendaData BuscarTipoAgenda(long seqTipoAgenda)
        {
            return this.TipoAgendaDomainService.SearchByKey(new SMCSeqSpecification<TipoAgenda>(seqTipoAgenda)).Transform<TipoAgendaData>();
        }
    }
}