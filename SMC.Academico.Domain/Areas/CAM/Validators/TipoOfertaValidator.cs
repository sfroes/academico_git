using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CAM.Validators
{
    public class TipoOfertaValidator : SMCValidator<TipoOferta>
    {
        protected override void DoValidate(TipoOferta item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;
        }
    }
}
