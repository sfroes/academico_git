using SMC.Academico.Domain.Areas.FIN.Models;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.FIN.Validators
{
    public class ContratoValidator : SMCValidator<Contrato>
    {
        protected override void DoValidate(Contrato item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            if (item.ArquivoAnexado != null && (!item.ArquivoAnexado.Nome.EndsWith(".pdf") || item.ArquivoAnexado.Tipo != "application/pdf"))
                this.AddPropertyError(p => p.ArquivoAnexado, string.Format(MessagesResource.MSG_ArquivoInvalido, "pdf"));
        }
    }
}

 