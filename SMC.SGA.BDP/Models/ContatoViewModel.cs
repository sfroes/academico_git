using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.BDP.Models
{
    public class ContatoViewModel : SMCViewModelBase
    {
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Assunto { get; set; }

        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Nome { get; set; }

        [SMCRequired]
        [SMCEmail]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Email { get; set; }

        [SMCRequired]
        [SMCMultiline(Rows = 6)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string Message { get; set; }
    }
}