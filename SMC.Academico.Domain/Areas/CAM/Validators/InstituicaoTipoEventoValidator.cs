using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CAM.Validators
{
    public class InstituicaoTipoEventoValidator : SMCValidator<InstituicaoTipoEvento>
    {
        protected override void DoValidate(InstituicaoTipoEvento item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;
        }
    }
}