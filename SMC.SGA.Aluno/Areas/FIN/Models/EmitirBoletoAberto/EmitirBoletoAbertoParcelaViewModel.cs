using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class EmitirBoletoAbertoParcelaViewModel : SMCViewModelBase
    {
        public int NumeroParcela { get; set; }

        public int SemestreParcela { get; set; }
        
        public int AnoParcela { get; set; }

        public string SemestreAno { get { return $"{SemestreParcela}/{AnoParcela}"; } }

        public DateTime DataLimitePagamento { get; set; }
        
        public DateTime DataVencimentoTitulo { get; set; }

        public List<EmitirBoletoAbertoBoletoViewModel> Boletos { get; set; }

    }
}