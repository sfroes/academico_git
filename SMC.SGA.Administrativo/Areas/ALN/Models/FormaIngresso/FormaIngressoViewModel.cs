using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class FormaIngressoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCMaxLength(100)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid7_24)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24)]
        [SMCSortable(true, true)]
        public string DescricaoXSD { get; set; }

        [SMCRequired]
        [SMCMinLength(3)]
        [SMCMaxLength(255)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCRegularExpression(REGEX.TOKEN)]
        public string Token { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid5_24)]
        [SMCSelect]
        public TipoFormaIngresso TipoFormaIngresso { get; set; }
    }
}