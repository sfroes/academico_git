using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class InstituicaoLocalidadeAgendaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidadeLocalidade { get; set; }

        public long SeqTipoAgenda { get; set; }

        public long SeqAgendaAgd { get; set; }
    }
}