using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class PeriodicoCapesLookupListaViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        public string CodigoISSN { get; set; }

        public string Descricao { get; set; }

        public string DescricaoAreaAvaliacao { get; set; }

        public QualisCapes QualisCapes { get; set; }
    }
}
