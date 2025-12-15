using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class TipoAgendaFiltroData : ISMCMappable
    {
        public long? SeqInstituicaoEnsino { get; set; }

        public string Descricao { get; set; }

        public bool? EventoLetivo { get; set; }

        public bool? DiaUtil { get; set; }

        public bool? ReplicarLancamentoPorLocalidade { get; set; }
    }
}