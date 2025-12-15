using SMC.EstruturaOrganizacional.UI.Mvc.Areas.ESO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class EntidadeAtoNormativoLookupListaViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        [SMCOrder(0)]
        [SMCSortable(true, false)]
        public long? Seq { get; set; }

        [SMCOrder(1)]
        [SMCSortable(true, false, "TipoEntidade.Descricao")]
        public string DescricaoTipoEntidade { get; set; }

        [SMCDescription]
        [SMCOrder(2)]
        [SMCSortable(true, false)]
        public string Nome { get; set; }

        //Ajuste na exibição do código seo (por favor não apagar o comentário)
        //[SMCOrder(3)]
        //[UnidadeLookup]
        //public UnidadeSeoListarLookupViewModel CodigoUnidadeSeo { get; set; }

        [SMCOrder(3)]
        [SMCValueEmpty("-")]
        public int? CodigoUnidadeSeo { get; set; }

        [SMCOrder(4)]
        public string DescricaoSituacaoAtual { get; set; }

        [SMCHidden]
        public long? SeqTipoEntidade { get; set; }

        [SMCHidden]
        [SMCMapProperty("TipoEntidade.Token")]
        public string TokenTipoEntidade { get; set; }
    }
}