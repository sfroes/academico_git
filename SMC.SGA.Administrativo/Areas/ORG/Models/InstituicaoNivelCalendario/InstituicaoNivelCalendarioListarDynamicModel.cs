using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class InstituicaoNivelCalendarioListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCHidden]
        public override long Seq { get; set; }

        public string DscNivelEnsino { get; set; }

        public string NomeCalendario { get; set; }

        public UsoCalendario UsoCalendario { get; set; }

    }
}