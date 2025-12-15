using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class CicloLetivoTipoEventoService : SMCServiceBase, ICicloLetivoTipoEventoService
    {
        #region Domain Services

        private CicloLetivoTipoEventoDomainService CicloLetivoTipoEventoDomainService
        {
            get { return this.Create<CicloLetivoTipoEventoDomainService>(); }
        }

        #endregion Domain Services

        #region Serviços

        private IInstituicaoTipoEventoService InstituicaoTipoEventoService
        {
            get { return this.Create<IInstituicaoTipoEventoService>(); }
        }

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        #endregion Serviços

        public List<SMCDatasourceItem> BuscarTiposEventosAGDSelect(ServiceContract.Data.TipoEventoFiltroData filtros)
        {
            if (filtros.SeqTipoAgenda == null)
                return new List<SMCDatasourceItem>();

            var data = filtros.Transform<InstituicaoTipoEventoFiltroData>();

            var instituicoesTipoEvento = this.InstituicaoTipoEventoService.BuscarInstituicoesTiposEventos(data);

            if (!instituicoesTipoEvento.Any())
                return new List<SMCDatasourceItem>();

            var dataAGD = new TipoEventoFiltroData() { Seqs = instituicoesTipoEvento.Select(x => x.SeqTipoEventoAgd).ToList() };

            return this.TipoEventoService.BuscarTiposEventosSelect(dataAGD);
        }

        public long SalvarCicloLetivoTipoEvento(CicloLetivoTipoEventoData modelo)
        {
            return this.CicloLetivoTipoEventoDomainService.SalvarCicloLetivoTipoEvento(modelo.Transform<CicloLetivoTipoEventoVO>());
        }

        public SMCPagerData<CicloLetivoTipoEventoListaData> BuscarCiclosLetivosTiposEventos(CicloLetivoTipoEventoFiltroData filtro)
        {
            var result = this.CicloLetivoTipoEventoDomainService.BuscarClclosLetivosTiposEventos(filtro.Transform<CicloLetivoTipoEventoFiltroVO>()).TransformList<CicloLetivoTipoEventoListaData>();

            return new SMCPagerData<CicloLetivoTipoEventoListaData>(result);
        }

        public CicloLetivoTipoEventoData BuscarCicloLetivoTipoEvento(long seq)
        {
            var result = this.CicloLetivoTipoEventoDomainService.BuscarClcloLetivoTipoEvento(seq).Transform<CicloLetivoTipoEventoData>();

            return result;
        }
    }
}