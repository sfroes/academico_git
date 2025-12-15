using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class EventoLetivoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCCssClass("smc-size-md-1 smc-size-xs-1 smc-size-sm-1 smc-size-lg-1")]
        public override long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoEvento { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string TipoEvento { get; set; }

        [SMCDescription]
        [SMCCssClass("smc-size-md-9 smc-size-xs-7 smc-size-sm-7 smc-size-lg-9")]
        public string Descricao { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public DateTime DataInicio { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public DateTime DataFim { get; set; }

        [SMCCssClass("smc-size-md-2 smc-size-xs-2 smc-size-sm-2 smc-size-lg-2")]
        public string CicloLetivo { get; set; }

        [SMCCssClass("smc-size-md-6 smc-size-xs-8 smc-size-sm-8 smc-size-lg-6")]
        public List<string> NivelEnsino { get; set; }
    }
}