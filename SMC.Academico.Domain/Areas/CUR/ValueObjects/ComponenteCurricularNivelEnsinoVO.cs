using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class ComponenteCurricularNivelEnsinoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqComponenteCurricular { get; set; }

        public long SeqNivelEnsino { get; set; }

        public bool Responsavel { get; set; }

        [SMCMapProperty("NivelEnsino.Descricao")]
        public string NivelEnsinoDescricao { get; set; }
    }
}
