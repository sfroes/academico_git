using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.CUR.ValueObjects
{
    public class TotalHoraCreditoVO : ISMCMappable
    {
        public decimal TotalHoras { get; set; }

        public decimal TotalHorasAula { get; set; }

        public decimal TotalCreditos { get; set; }

        public decimal TotalCreditosPorHora { get; set; }

        public decimal TotalHorasPorCredito { get; set; }
    }
}