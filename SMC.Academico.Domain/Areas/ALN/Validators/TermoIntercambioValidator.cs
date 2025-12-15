using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.ALN.Validators
{
    public class TermoIntercambioValidator : SMCValidator<TermoIntercambio>
    {
        protected override void DoValidate(TermoIntercambio item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;
        }
    }
}
