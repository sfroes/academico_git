using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoUnidadeResponsavelDetalheVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqEntidadeResponsavel { get; set; }

        [SMCMapProperty("EntidadeResponsavel.Nome")]
        public string NomeEntidadeResponsavel { get; set; }

        public long SeqProcesso { get; set; }

        public TipoUnidadeResponsavel TipoUnidadeResponsavel { get; set; }
    }
}