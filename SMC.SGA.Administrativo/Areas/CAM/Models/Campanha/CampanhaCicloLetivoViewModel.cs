using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaCicloLetivoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public short AnoCicloLetivo { get; set; }

        public short NumeroCicloLetivo { get; set; }
        
        public string Descricao { get; set; }
    }
}