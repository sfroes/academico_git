using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoCondicaoObrigatoriedadeViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoaAtuacao { get; set; }

        [SMCHidden]
        public long SeqCondicaoObrigatoriedade { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid18_24)]
        public string DescricaoCondicaoObrigatoriedade { get; set; }

        /// <summary>
        /// Radio com o valor da condição para a pessoa atuação.
        /// O label deste campo deve ser o valor do campo DescricaoCondicaoObrigatoriedade
        /// </summary>
        [SMCConditionalReadonly(nameof(PessoaAtuacaoCondicaoObrigatoriedadeDynamicModel.PossuiSituacaoImpeditivaIngressante), true, PersistentValue = true)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid6_24)]
        public bool Ativo { get; set; }
    }
}