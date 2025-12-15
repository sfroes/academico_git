using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.CUR.Data
{
    public class GrupoCurricularInformacaoData : ISMCMappable
    {
        public long SeqGrupoCurricular { get; set; }

        public long? SeqBeneficio { get; set; }

        public string DescricaoBeneficio { get; set; }

        public long? SeqCondicaoObrigatoriedade { get; set; }

        public string DescricaoCondicaoObrigatoriedade { get; set; }       
    }
}
