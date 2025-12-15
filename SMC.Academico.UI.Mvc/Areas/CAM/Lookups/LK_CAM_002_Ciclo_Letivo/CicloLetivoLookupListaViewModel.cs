using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CAM.Lookups
{
    public class CicloLetivoLookupListaViewModel : SMCViewModelBase, ISMCLookupViewModel
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string DescricaoRegimeLetivo { get; set; }

        [SMCHidden]
        public List<CicloLetivoNivelEnsinoViewModel> NiveisEnsino { get; set; }

        public List<string> DescricaoNiveisEnsino => NiveisEnsino?.Select(s => s.Descricao).ToList();
    }

    public class CicloLetivoNivelEnsinoViewModel
    {
        public string Descricao { get; set; }
    }
}
