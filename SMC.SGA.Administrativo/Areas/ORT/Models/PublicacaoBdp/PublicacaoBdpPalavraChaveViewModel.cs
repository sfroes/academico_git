using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class PublicacaoBdpPalavraChaveViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid22_24)]
        [SMCRequired]
        public string PalavraChave { get; set; }
    }
}