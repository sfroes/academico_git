using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ProcessoUnidadeResponsavelVO : ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqProcesso { get; set; }

        [SMCMapProperty("EntidadeResponsavel.Nome")]
        public string Nome { get; set; }

        [SMCMapProperty("EntidadeResponsavel.NomeReduzido")]
        public string NomeReduzido { get; set; }
    }
}