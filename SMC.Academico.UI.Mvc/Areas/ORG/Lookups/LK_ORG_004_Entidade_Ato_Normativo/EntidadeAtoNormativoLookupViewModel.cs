using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeAtoNormativoLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSortable(true)]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCSortable(true)]
        [SMCHidden]
        [SMCMapProperty("TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        [SMCDescription]
        [SMCOrder(2)]
        [SMCSortable(true, true)]
        public string Nome { get; set; }

        [SMCOrder(3)]
        [SMCHidden]
        [SMCMapProperty("SituacaoAtual.Descricao")]
        public string DescricaoSituacaoAtual { get; set; }

        [SMCHidden]
        public long? SeqTipoEntidade { get; set; }

        [SMCHidden]
        [SMCMapProperty("TipoEntidade.Token")]
        public string TokenTipoEntidade { get; set; }
    }
}