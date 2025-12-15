using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class EventoLetivoService : SMCServiceBase, IEventoLetivoService
    {
        #region Domain Service

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private EventoLetivoDomainService EventoLetivoDomainService
        {
            get { return this.Create<EventoLetivoDomainService>(); }
        }

        #endregion Domain Service

        #region Services

        private IInstituicaoTipoEventoService InstituicaoTipoEventoService
        {
            get { return this.Create<IInstituicaoTipoEventoService>(); }
        }

        private ITipoEventoService TipoEventoService
        {
            get { return this.Create<ITipoEventoService>(); }
        }

        #endregion Services

        public EventoLetivoData BuscarEventoLetivo(long seq)
        {
            return this.EventoLetivoDomainService.BuscarEventoLetivo(seq).Transform<EventoLetivoData>();
        }

        public SMCPagerData<EventoLetivoListaData> BuscarEventosLetivos(EventoLetivoFiltroData filtros)
        {
            var result = this.EventoLetivoDomainService.BuscarEventosLetivos(filtros.Transform<EventoLetivoFiltroVO>());

            return new SMCPagerData<EventoLetivoListaData>(result.TransformList<EventoLetivoListaData>());
        }

        public long SalvarEventoLetivo(EventoLetivoData modelo)
        {
            return this.EventoLetivoDomainService.SalvarEventoLetivo(modelo.Transform<EventoLetivoVO>());
        }

        public DatasEventoLetivoData BuscarEventoLetivoAtual(long seqAluno)
        {
            return ConfiguracaoEventoLetivoDomainService.BuscarEventoLetivoAtual(seqAluno).Transform<DatasEventoLetivoData>();
        }

        public DatasEventoLetivoData BuscarDatasEventoLetivo(long seqCicloLetivo, long seqAluno, string tokenTipoEvento)
        {
            return ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo, seqAluno, tokenTipoEvento).Transform<DatasEventoLetivoData>();
        }
    }
}