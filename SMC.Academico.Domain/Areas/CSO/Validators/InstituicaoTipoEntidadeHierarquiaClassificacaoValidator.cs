using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CSO.Validators
{
    public class InstituicaoTipoEntidadeHierarquiaClassificacaoValidator : SMCValidator<InstituicaoTipoEntidadeHierarquiaClassificacao>
    {
        protected override void DoValidate(InstituicaoTipoEntidadeHierarquiaClassificacao item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item.QuantidadeMinima < 1)
                this.AddPropertyError(p => p.QuantidadeMinima, string.Format(MessagesResource.MSG_ValorMinimoMaiorQueZero));

            if ((item.QuantidadeMaxima.HasValue) && (item.QuantidadeMaxima < item.QuantidadeMinima))
                this.AddPropertyError(p => p.QuantidadeMaxima, string.Format(MessagesResource.MSG_ValorMaximoMenorQueMinimo));
        }
    }
}