using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TermoIntercambioTipoMobilidadeViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public virtual long Seq { get; set; }

        [SMCHidden]
        public long SeqTermoIntercambio { get; set; }

        [SMCConditionalReadonly(nameof(TermoIntercambioDynamicModel.PossuiPessoaAtuacao), SMCConditionalOperation.Equals, true, PersistentValue = true, RuleName = "TMR1")]
        [SMCConditionalReadonly(nameof(SeqTermoIntercambio), SMCConditionalOperation.NotEqual, 0, PersistentValue = true, RuleName = "TMR2")]
        [SMCConditionalRule("TMR1 && TMR2")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid16_24, SMCSize.Grid12_24)]
        [SMCSelect]
        public TipoMobilidade TipoMobilidade { get; set; }

        [SMCRequired]
        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid4_24)]
        [SMCMaxValue(9999)]
        public short? QuantidadeVagas { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [SMCDetail(min: 0)]
        public SMCMasterDetailList<TermoIntercambioPessoaViewModel> Pessoas { get; set; }
    }
}