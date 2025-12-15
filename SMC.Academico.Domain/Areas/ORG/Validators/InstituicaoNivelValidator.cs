using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.ORG.Validators
{
    public class InstituicaoNivelValidator : SMCValidator<InstituicaoNivel>
    {
        protected override void DoValidate(InstituicaoNivel item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            if (item.QuantidadeMinutosMaximoAula <= item.QuantidadeMinutosMinimoAula)
                this.AddPropertyError(p => p.QuantidadeMinutosMaximoAula, MessagesResource.MSG_QuantidadeMinutosMaximoAulaInvalido);
        }
    }
}