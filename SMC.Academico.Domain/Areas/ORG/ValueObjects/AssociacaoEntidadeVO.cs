using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ORG.ValueObjects
{
    public class AssociacaoEntidadeVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long? SeqAtoNormativo { get; set; }

        public long? SeqEntidade { get; set; }

        public string Nome { get; set; }

        public long? SeqGrauAcademico { get; set; }

        public bool HabilitaCampo { get; set; }
    }
}
