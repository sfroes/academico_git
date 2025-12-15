using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class PeriodicoCapesLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCHidden]
        public bool? ClassificacaoPeriodicoAtual { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        public string CodigoISSN { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid8_24)]
        public string Descricao { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string DescAreaAvaliacao { get; set; }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect]
        public QualisCapes? QualisCapes { get; set; }
    }
}
