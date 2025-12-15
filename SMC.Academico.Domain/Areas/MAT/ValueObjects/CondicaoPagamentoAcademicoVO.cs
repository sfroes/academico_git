using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class CondicaoPagamentoAcademicoVO : ISMCMappable
    {
        public int SeqCondicaoPagamento { get; set; }

        public string DescricaoCondicaoPagamento { get; set; }

        public decimal ValorTotalCurso { get; set; }

        public int QuantidadeParcelas { get; set; }

        public decimal ValorParcelas { get; set; }

        public int DiaVencimentoParcelas { get; set; }

        public DateTime DataVencimentoParcelas { get; set; }

        public int NumeroParcela { get; set; }
    }
}