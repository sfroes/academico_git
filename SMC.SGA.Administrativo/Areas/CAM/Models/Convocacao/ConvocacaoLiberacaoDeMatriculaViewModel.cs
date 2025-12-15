using SMC.Framework.UI.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoLiberacaoDeMatriculaViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Ingressante { get; set; } 
    }
}
 