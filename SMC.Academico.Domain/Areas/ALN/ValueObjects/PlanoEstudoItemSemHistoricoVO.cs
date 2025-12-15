using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class PlanoEstudoItemSemHistoricoVO : ISMCMappable
    {
        public long SeqComponenteCurricular { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string DescricaoComponenteCurricularAssunto { get; set; }

        public long SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public long SeqPlanoEstudo { get; set; }

        public long SeqPlanoEstudoItem { get; set; }
    }
}