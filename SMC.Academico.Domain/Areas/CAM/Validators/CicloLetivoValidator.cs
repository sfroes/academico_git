using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Resources;
using SMC.Framework;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CAM.Validators
{
    public class CicloLetivoValidator : SMCValidator<CicloLetivo>
    {
        protected override void DoValidate(CicloLetivo item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            if ((item?.RegimeLetivo?.NumeroItensAno ?? 0) < item.Numero)
            {
                this.AddPropertyError(x => x.Numero, MessagesResource.MSG_NumeroCiclo);
            }

            if (item.NiveisEnsino.SMCCount() < 1)
            {
                this.AddPropertyError(x => x.NiveisEnsino, MessagesResource.MSG_ObrigatoriedadeNivelEnsino);
            }
        }
    }
}
