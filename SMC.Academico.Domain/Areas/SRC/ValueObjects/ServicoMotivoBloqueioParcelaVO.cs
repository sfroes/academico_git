using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ServicoMotivoBloqueioParcelaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqMotivoBloqueio { get; set; }

        public long SeqServico { get; set; }

        public bool Obrigatorio { get; set; }
    }
}
