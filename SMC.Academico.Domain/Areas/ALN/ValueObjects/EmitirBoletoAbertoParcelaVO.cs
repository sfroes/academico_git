using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class EmitirBoletoAbertoParcelaVO
    {
        public int NumeroParcela { get; set; }

        public int? SemestreParcela { get; set; }

        public int? AnoParcela { get; set; }

        public DateTime DataLimitePagamento { get; set; }

        public DateTime DataVencimentoTitulo { get; set; }

        public List<EmitirBoletoAbertoBoletoVO> Boletos { get; set; }
    }
}
