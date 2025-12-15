using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class TipoAgendaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqInstituicaoEnsino { get; set; }

        public string Descricao { get; set; }

        public bool? EventoLetivo { get; set; }
        public bool? DiaUtil { get; set; }

        public bool? ReplicarLancamentoPorLocalidade { get; set; }
    }
}