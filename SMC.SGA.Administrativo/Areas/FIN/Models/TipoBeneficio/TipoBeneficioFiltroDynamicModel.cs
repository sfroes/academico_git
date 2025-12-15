using SMC.Academico.Common.Areas.FIN.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class TipoBeneficioFiltroDynamicModel : SMCDynamicFilterViewModel
    {

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public long Seq { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid18_24, SMCSize.Grid12_24)]
        public string Descricao { get; set; }

        [SMCSelect(autoSelectSingleItem: true)]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid9_24, SMCSize.Grid8_24)]
        public ClassificacaoBeneficio ClassificacaoBeneficio { get; set; }
    }
}