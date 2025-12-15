using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class ConfiguracaoComponenteLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        [SMCRadioButton]
        public long? Seq { get; set; }

        [SMCDescription]
        public string ConfiguracaoComponenteDescricaoCompleta { get; set; }

        public bool? Ativo { get; set; }

        public long? SeqComponenteCurricular { get; set; }

        public long? SeqNivelEnsino { get; set; }

        public List<ConfiguracaoComponenteDivisaoLookupViewModel> DivisoesComponente { get; set; }
    }
}