using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.MAT.Data
{
    public class EfetivacaoParcelaData : ISMCMappable
    {
        public string Numero { get; set; }

        public string Vencimento { get; set; }

        public string Limite { get; set; }
    }
}
