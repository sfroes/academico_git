using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularEntidadeResponsavelVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqEntidade { get; set; }

        [SMCMapProperty("Entidade.Nome")]
        public string NomeEntidade { get; set; }
    }
}
