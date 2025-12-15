using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class HistoricoNavegacaoItemViewModel : SMCViewModelBase
    {
        [SMCCssClass("smc-size-md-16 smc-size-xs-16 smc-size-sm-16 smc-size-lg-16")]
        public string Pagina { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public DateTime DataEntrada { get; set; }

        [SMCDateTimeMode(SMCDateTimeMode.DateTime)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public DateTime? DataSaida { get; set; }
    }
}