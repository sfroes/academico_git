using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class ClassificacaoLookupViewModel : SMCViewModelBase, ISMCLookupData, ISMCLookupViewModel
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqHierarquiaClassificacao { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        [SMCHidden]
        public long? SeqClassificacaoSuperior { get; set; }

        /// <summary>
        /// Tem que ter a propriedade que relaciona com o Nivel Superior e o SeqPai
        /// </summary>
        [SMCHidden]
        public long? SeqPai { get => SeqClassificacaoSuperior; set => SeqClassificacaoSuperior = value; }

        /// <summary>
        /// Setado quando o nó deve ser selecionável no lookup
        /// </summary>
        [SMCHidden]
        public bool? TipoClassificacaoSelecionavel { get; set; }
    }
}