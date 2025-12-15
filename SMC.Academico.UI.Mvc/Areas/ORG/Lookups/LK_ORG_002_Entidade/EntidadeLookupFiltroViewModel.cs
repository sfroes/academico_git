using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCKey]
        [SMCSize(SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid8_24)]
        public string Nome { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCSelect("TiposEntidade", AutoSelectSingleItem = true)]
        [SMCReadOnly]
        public long? SeqTipoEntidade { get; set; }

        public List<SMCDatasourceItem> TiposEntidade { get; set; }

        [SMCHidden]
        public long SeqTipoHierarquiaEntidadeItem { get; set; }

        [SMCHidden]
        public bool? ApenasAtivos { get; set; }
    }
}
