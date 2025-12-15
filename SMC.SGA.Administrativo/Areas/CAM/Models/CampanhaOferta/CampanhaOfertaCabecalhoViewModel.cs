using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaOfertaCabecalhoViewModel : SMCPagerViewModel
    {
        public long SeqCampanha { get; set; }

        public string Campanha { get; set; }

        public List<string> CiclosLetivos { get; set; }
    }
}