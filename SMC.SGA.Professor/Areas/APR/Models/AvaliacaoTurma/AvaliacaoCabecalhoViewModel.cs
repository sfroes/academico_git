using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class AvaliacaoCabecalhoViewModel : SMCViewModelBase
    {
        public string OrigemAvaliacao { get; set; }

        public long SeqOrigemAvaliacao { get; set; }
    }
}