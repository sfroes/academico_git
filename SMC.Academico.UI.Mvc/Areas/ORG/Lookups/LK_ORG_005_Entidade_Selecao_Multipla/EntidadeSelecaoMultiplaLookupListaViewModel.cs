using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeSelecaoMultiplaLookupListaViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public long Seq { get; set; }

        [SMCSortable(true, false)]
        [SMCCssClass("smc-size-md-4 smc-size-xs-4 smc-size-sm-4 smc-size-lg-4")]
        public string DescricaoTipoEntidade { get; set; }

        [SMCSortable(true, true)]
        [SMCCssClass("smc-size-md-10 smc-size-xs-10 smc-size-sm-10 smc-size-lg-10")]
        public string Nome { get; set; }

        [SMCSortable(true, false)]
        [SMCCssClass("smc-size-md-6 smc-size-xs-6 smc-size-sm-6 smc-size-lg-6")]
        public CategoriaAtividade CategoriaAtividadeSituacaoAtual { get; set; }
    }
}