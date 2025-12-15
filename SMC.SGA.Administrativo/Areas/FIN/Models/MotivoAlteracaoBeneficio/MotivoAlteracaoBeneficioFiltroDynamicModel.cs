using SMC.Academico.Common.Areas.FIN.Constants;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class MotivoAlteracaoBeneficioFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCSize(SMCSize.Grid2_24)]
        public long Seq { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid7_24)]
        public string Descricao { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid7_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }
    }
}