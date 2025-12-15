using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class HistoricoEscolarAprovadoFiltroVO : ISMCMappable
    {
        public long? SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public long SeqTurma { get; set; }
    }
}