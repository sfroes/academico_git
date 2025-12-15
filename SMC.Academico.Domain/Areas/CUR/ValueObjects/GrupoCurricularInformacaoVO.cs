using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class GrupoCurricularInformacaoVO : ISMCMappable
    {
        public long SeqGrupoCurricular { get; set; }

        public long? SeqBeneficio { get; set; }

        public string DescricaoBeneficio { get; set; }

        public long? SeqCondicaoObrigatoriedade { get; set; }

        public string DescricaoCondicaoObrigatoriedade { get; set; }                        
    }
}
