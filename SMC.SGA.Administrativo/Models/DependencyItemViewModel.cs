using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Models
{
    public class DependencyItemViewModel : SMCViewModelBase
    {
        [SMCDataSource]
        public List<SMCDatasourceItem> Primario { get; set; }

        [SMCDataSource]
        public List<SMCDatasourceItem> Secundario { get; set; }

        [SMCSelect(nameof(Primario), UseCustomSelect = true)]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24 )]
        public long? SeqPrimario { get; set; }

        [SMCSelect(nameof(Secundario))]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public long? SeqSecundario { get; set; }

        [SMCDependency(nameof(SeqPrimario), "TesteDependency", "Home", true, nameof(SeqSecundario))]
        [SMCDependency(nameof(SeqSecundario), "TesteDependency", "Home", true, nameof(SeqPrimario))]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public string PropA { get; set; }

        [SMCDependency(nameof(SeqPrimario), "TesteDependencyMultiplo", "Home", true, nameof(PropC), nameof(SeqSecundario))]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public string PropB { get; set; }

        [SMCDependency(nameof(SeqPrimario), "TesteDependencyMultiplo", "Home", true, nameof(PropB), nameof(SeqSecundario))]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public string PropC { get; set; }

        [SMCDependency(nameof(DependencyViewModel.SeqFora), "TesteDependencyFora", "Home", true, nameof(PropB), nameof(SeqSecundario))]
        [SMCDependency(nameof(SeqPrimario), "TesteDependencyFora", "Home", true, nameof(PropB), nameof(SeqSecundario))]
        [SMCRequired]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        public string PropD { get; set; }
    }
}