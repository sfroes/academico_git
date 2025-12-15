using SMC.Academico.Domain.Areas.DCT.Models;
using SMC.Academico.Domain.Areas.DCT.Resources;
using SMC.Framework;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.DCT.Validators
{
    public class ColaboradorVinculoValidator_old : SMCValidator<ColaboradorVinculo>
    {
        protected override void DoValidate(ColaboradorVinculo item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            item.FormacoesEspecificas.SMCForEach(formacaoEspecifica =>
            {
                if (formacaoEspecifica.DataInicio < item.DataInicio || item.DataFim.HasValue && formacaoEspecifica.DataInicio > item.DataFim)
                {
                    this.AddPropertyError("FormacoesEspecificas.DataInicio", MessagesResource.ERR_ColaboradorVinculoDataFormacaoEspecifica);
                }
                if (formacaoEspecifica.DataFim.HasValue &&
                    (formacaoEspecifica.DataFim < item.DataInicio || item.DataFim.HasValue && formacaoEspecifica.DataFim > item.DataFim))
                {
                    this.AddPropertyError("FormacoesEspecificas.DataFim", MessagesResource.ERR_ColaboradorVinculoDataFormacaoEspecifica);
                }
            });
        }
    }
}