using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class SolicitacaoDispensaComponenteCurricularData : ISMCMappable
    {
        public long? SeqGrupoCurricularComponente { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }
    }
}