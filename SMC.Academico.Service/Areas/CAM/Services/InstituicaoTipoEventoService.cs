using SMC.Academico.Common.Areas.CAM.Includes;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Academico.ServiceContract.Data;
using SMC.Academico.ServiceContract.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class InstituicaoTipoEventoService : SMCServiceBase, IInstituicaoTipoEventoService
    {
        #region Domain Service

        private InstituicaoTipoEventoDomainService InstituicaoTipoEventoDomainService
        {
            get { return this.Create<InstituicaoTipoEventoDomainService>(); }
        }

        private TipoAgendaDomainService TipoAgendaDomainService
        {
            get { return this.Create<TipoAgendaDomainService>(); }
        }

        #endregion Domain Service

        #region Services

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        #endregion Services

        public List<SMCDatasourceItem> BuscarTiposEventosAGDSelect(TipoEventoFiltroData filtros)
        {
            if (filtros.SeqTipoAgenda.HasValue && filtros.SeqTipoAgenda.Value > 0)
            {
                var tipoAgenda = this.TipoAgendaDomainService.SearchByKey(new SMCSeqSpecification<TipoAgenda>(filtros.SeqTipoAgenda.Value), IncludesTipoAgenda.InstituicaoEnsino);

                if (tipoAgenda.InstituicaoEnsino.SeqUnidadeResponsavelAgd != null)
                    return this.TipoEventoService.BuscarTiposEventosAGDSelect(new Calendarios.ServiceContract.Areas.CLD.Data.TipoEventoFiltroData() { SeqUnidadeResponsavel = tipoAgenda.InstituicaoEnsino.SeqUnidadeResponsavelAgd, Ativo = filtros.Ativo });
            }

            return new List<SMCDatasourceItem>();
        }

        public SMCPagerData<InstituicaoTipoEventoListaData> BuscarInstituicoesTiposEventos(InstituicaoTipoEventoFiltroData filtros)
        {
            int total = 0;

            var spec = filtros.Transform<InstituicaoTipoEventoFilterSpecification>();

            var result = this.InstituicaoTipoEventoDomainService.SearchBySpecification(spec, out total,
                                             IncludesInstituicaoTipoEvento.TipoAgenda |
                                             IncludesInstituicaoTipoEvento.InstituicaoEnsino |
                                             IncludesInstituicaoTipoEvento.Parametros)
                                            .TransformList<InstituicaoTipoEventoListaData>();

            var tiposEventosAGD = this.TipoEventoService.BuscarTiposEventosAGD(new Calendarios.ServiceContract.Areas.CLD.Data.TipoEventoFiltroData() { Seqs = result.Select(s => s.SeqTipoEventoAgd).ToList() });

            result.SMCForEach(x =>
            {
                x.DescricaoTipoEvento = tiposEventosAGD.FirstOrDefault(w => w.Seq == x.SeqTipoEventoAgd).Descricao;
            });

            return new SMCPagerData<InstituicaoTipoEventoListaData>(result, total);
        }

        public InstituicaoTipoEventoData BuscarInstituicaoTipoEvento(InstituicaoTipoEventoFiltroData filtros)
        {
            var spec = filtros.Transform<InstituicaoTipoEventoFilterSpecification>();

            return InstituicaoTipoEventoDomainService.SearchByKey(spec, IncludesInstituicaoTipoEvento.Parametros).Transform<InstituicaoTipoEventoData>();
        }

        public List<SMCDatasourceItem> BuscarParametrosInstituicaoTipoEventoSelect(InstituicaoTipoEventoFiltroData filtros)
        {
            var list = new List<SMCDatasourceItem>();

            var result = BuscarInstituicaoTipoEvento(filtros);

            foreach (var parametro in result.Parametros)
            {
                list.Add(new SMCDatasourceItem() { Seq = parametro.Seq, Descricao = SMCEnumHelper.GetDescription(parametro.TipoParametroEvento) });
            }

            return list;
        }

        public long SalvarInstituicaoTipoEvento(InstituicaoTipoEventoData modelo)
        {
            return this.InstituicaoTipoEventoDomainService.SalvarInstituicaoTipoEvento(modelo.Transform<InstituicaoTipoEventoVO>());
        }
    }
}