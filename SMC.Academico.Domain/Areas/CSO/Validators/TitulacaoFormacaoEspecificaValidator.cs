using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CSO.Validators
{
    public class TitulacaoFormacaoEspecificaValidator : SMCValidator<CursoFormacaoEspecificaTitulacao>
    {
        protected override void DoValidate(CursoFormacaoEspecificaTitulacao cursoFormacaoEspecificaTitulacao, SMCValidationResults validationResults)
        {
            base.DoValidate(cursoFormacaoEspecificaTitulacao, validationResults);

            if (cursoFormacaoEspecificaTitulacao == null)
                return;
        }
    }
}