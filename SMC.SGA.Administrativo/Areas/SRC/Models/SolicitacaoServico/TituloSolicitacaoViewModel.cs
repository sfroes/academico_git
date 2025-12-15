using SMC.Financeiro.Common.Areas.GRA.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TituloSolicitacaoViewModel : SMCViewModelBase
    {
        public int SeqTituloGra { get; set; }

        public decimal ValorTitulo { get; set; }

        public SituacaoTitulo SituacaoTitulo { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        public DateTime DataGeracaoTitulo { get; set; }

        public DateTime DataVencimento { get; set; }

        [SMCValueEmpty("-")]
        public DateTime? DataPagamento { get; set; }
    }
}