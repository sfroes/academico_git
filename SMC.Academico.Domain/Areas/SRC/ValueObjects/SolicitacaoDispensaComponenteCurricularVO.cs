using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class SolicitacaoDispensaComponenteCurricularVO : ISMCMappable
    {
        public long? SeqGrupoCurricularComponente { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

    }
}