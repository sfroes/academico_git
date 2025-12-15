using SMC.Academico.ServiceContract.Interfaces;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Services
{
    public class TipoEventoService : SMCServiceBase, ITipoEventoService
    {
        #region Serviços

        private Calendarios.ServiceContract.Areas.CLD.Interfaces.ITipoEventoService TipoEventoServiceAGD
        {
            get { return Create<Calendarios.ServiceContract.Areas.CLD.Interfaces.ITipoEventoService>(); }
        }

        #endregion Serviços

        public SMCPagerData<TipoEventoData> BuscarTiposEventosAGD(TipoEventoFiltroData filtros)
        {
            var result = this.TipoEventoServiceAGD.BuscarTiposEventos(filtros);

            return new SMCPagerData<TipoEventoData>(result, result.Count());
        }

        public List<SMCDatasourceItem> BuscarTiposEventosAGDSelect(TipoEventoFiltroData filtros)
        {
            var result = this.TipoEventoServiceAGD.BuscarTiposEventosSelect(filtros);

            return result;
        }

        public List<SMCDatasourceItem> BuscarTiposEventosCalendarioAGD(long seqCalendarioAgd)
        {
            var result = this.TipoEventoServiceAGD.BuscarTiposEventosSelect(new TipoEventoFiltroData() { SeqCalendario = seqCalendarioAgd });
            return result;
        }

        public string BuscarTokenTipoEventoAGD(long seqTipoEventoAgd)
        {
            var token = this.TipoEventoServiceAGD.BuscarTokenTipoEvento(seqTipoEventoAgd);
            return token;
        }
    }
}