using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeLookupViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSortable(true)]
        public long Seq { get; set; }

        [SMCOrder(1)]
        [SMCSortable(true, false, "TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        [SMCDescription]
        [SMCOrder(2)]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCOrder(3)]
        public string DescricaoSituacaoAtual { get; set; }
    }
}