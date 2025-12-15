using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class ContratoNiveisEnsinoListarViewModel : SMCViewModelBase
    { 
        [SMCSize(SMCSize.Grid10_24)]  
        public string DescricaoNivelEnsino { get; set; }
    }
}