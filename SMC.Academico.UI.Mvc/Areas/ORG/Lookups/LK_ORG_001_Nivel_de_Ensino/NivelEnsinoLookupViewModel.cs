using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class NivelEnsinoLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        [SMCMapProperty("SeqNivelEnsinoSuperior")]
        [SMCHidden]
        public long SeqPai { get; set; }

        [SMCHidden]
        public bool Folha { get; set; }
    }
}
