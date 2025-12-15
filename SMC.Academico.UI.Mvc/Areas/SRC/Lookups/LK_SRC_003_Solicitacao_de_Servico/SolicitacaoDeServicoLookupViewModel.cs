using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Lookups
{
    public class SolicitacaoDeServicoLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCHidden]
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
        public string DescricaoLookupSolicitacaoReduzida { get; set; }
    }
}