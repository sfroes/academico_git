using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.APR.Validators
{
    public class CriterioAprovacaoValidator : SMCValidator<CriterioAprovacao>
    {
        protected override void DoValidate(CriterioAprovacao item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            if (item.ApuracaoNota && item.EscalaApuracao != null && item.EscalaApuracao.TipoEscalaApuracao != TipoEscalaApuracao.Conceito)
                this.AddPropertyError(p => p.SeqEscalaApuracao, MessagesResource.MSG_CriterioAprovacaoComApuracaoSemConceito);

            if (!item.ApuracaoNota && item.EscalaApuracao.TipoEscalaApuracao == TipoEscalaApuracao.Conceito)
                this.AddPropertyError(p => p.SeqEscalaApuracao, MessagesResource.MSG_CriterioAprovacaoSemApuracaoComConceito);
        }
    }
}
