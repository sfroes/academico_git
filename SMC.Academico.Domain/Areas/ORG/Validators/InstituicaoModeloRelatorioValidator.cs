using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.ORG.Validators
{
    public class InstituicaoModeloRelatorioValidator : SMCValidator<InstituicaoModeloRelatorio>
    {
        protected override void DoValidate(InstituicaoModeloRelatorio item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            if (item.ArquivoModelo != null && !item.ArquivoModelo.Nome.EndsWith(".dotx"))
                this.AddPropertyError(p => p.ArquivoModelo, string.Format(MessagesResource.MSG_ArquivoInvalido, "dotx"));
        }
    }
}
