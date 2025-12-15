using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelTipoTermoIntercambioConfirmacaoViewModel
    {
        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string TipoTermoIntercambioConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string ConcedeFormacaoTipoTermoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string ExigePeriodoIntercambioTermoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string PermiteIngressoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string PermiteSaidaIntercambioConfirmacao { get; set; }
    }
}