using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class EmitirBoletoAbertoParcelaData : ISMCMappable
    {
        public int NumeroParcela { get; set; }

        public int? SemestreParcela { get; set; }

        public int? AnoParcela { get; set; }

        public DateTime DataLimitePagamento { get; set; }

        public DateTime DataVencimentoTitulo { get; set; }

        public List<EmitirBoletoAbertoBoletoData> Boletos { get; set; }
    }
}
