using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.ALN.Validators
{
    public class ParceriaIntercambioValidator : SMCValidator<ParceriaIntercambio>
    {
        protected override void DoValidate(ParceriaIntercambio item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            //if ((item?.RegimeLetivo?.NumeroItensAno ?? 0) < item.Numero)
            //{
            //    this.AddPropertyError(x => x.Numero, MessagesResource.MSG_NumeroCiclo);
            //}

            //if (item.NiveisEnsino.SMCCount() < 1)
            //{
            //    this.AddPropertyError(x => x.NiveisEnsino, MessagesResource.MSG_ObrigatoriedadeNivelEnsino);
            //}
        }
    }
}
