using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class PlanoEstudoItemSemHistoricoData : ISMCMappable
    {
        public long SeqComponenteCurricular { get; set; }

        public string DescricaoComponenteCurricular { get; set; }

        public long? SeqComponenteCurricularAssunto { get; set; }

        public string DescricaoComponenteCurricularAssunto { get; set; }

        public long SeqCicloLetivo { get; set; }

        public string DescricaoCicloLetivo { get; set; }

        public long SeqPlanoEstudoItem { get; set; }
    }
}