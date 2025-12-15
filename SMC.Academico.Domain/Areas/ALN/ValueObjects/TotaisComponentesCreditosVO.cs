using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TotaisComponentesCreditosVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Situacao { get; set; }

        public long QuantidadeComponente { get; set; }

        public int TotalCargaHoraria { get; set; }

        public int TotalCredito { get; set; }

        public decimal MediaNota { get; set; }

        public long QuantidadeComponenteCreditoMaiorZero { get; set; }

        public int TotalCargaHorariaCreditoMaiorZero { get; set; }

        public int TotalCreditoCreditoMaiorZero { get; set; }

        public decimal MediaNotaCreditoMaiorZero { get; set; }
    }
}
