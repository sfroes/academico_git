using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TipoTermoIntercambioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCSize(SMCSize.Grid3_24)]
        public long Seq { get; set; }

        [SMCFilter]
        [SMCMaxLength(50)]
        [SMCSize(SMCSize.Grid7_24)]        
        public string Descricao { get; set; }

        [SMCFilter]
        [SMCSelect]
        [SMCSize(SMCSize.Grid5_24)]
        public bool? PermiteAssociarAluno { get; set; }
    }
}