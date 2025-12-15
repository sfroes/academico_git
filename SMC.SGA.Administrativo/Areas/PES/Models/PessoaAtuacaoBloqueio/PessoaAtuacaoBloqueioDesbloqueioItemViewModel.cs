using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class PessoaAtuacaoBloqueioDesbloqueioItemViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid20_24)]
        [SMCMaxLength(255)]
        [SMCReadOnly]
        [SMCOrder(0)]
        public string Descricao { get; set; }

        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid4_24)]
        [SMCSelect]
        [SMCRequired]
        [SMCConditionalReadonly(nameof(BloquearSituacao), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "Rule5")]
        [SMCConditionalReadonly(nameof(TipoDesbloqueioDetalhe), SMCConditionalOperation.Equals, TipoDesbloqueio.Efetivo, PersistentValue = true, RuleName = "Rule6")]
        [SMCConditionalRule("Rule5 && Rule6")]
        public SituacaoBloqueio SituacaoBloqueio { get; set; }

        [SMCHidden]
        public bool BloquearSituacao { get; set; }

        [SMCHidden]
        public TipoDesbloqueio TipoDesbloqueioDetalhe { get; set; }
    }
}