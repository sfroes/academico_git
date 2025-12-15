using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TotaisComponentesCreditosData : ISMCMappable
    {
        public long Seq { get; set; }

        public string Situacao { get; set; }

        public long QuantidadeComponente { get; set; }

        public int TotalCargaHoraria { get; set; }

        public int TotalCredito { get; set; }

        public decimal MediaNota { get; set; }

        public decimal MediaNotaCreditoMaiorZero { get; set; }
    }
}
