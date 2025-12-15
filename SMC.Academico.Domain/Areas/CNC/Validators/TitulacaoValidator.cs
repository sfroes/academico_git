using SMC.Academico.Domain.Areas.CNC.Models;
using SMC.Academico.Domain.Areas.CNC.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CNC.Validators
{
    public class TitulacaoValidator : SMCValidator<Titulacao>
    {
        protected override void DoValidate(Titulacao item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

             if (item.SeqCurso.HasValue && item.SeqGrauAcademico.HasValue)
            {
                this.AddError("AssociacaoErro_Ambiguidade", MessagesResource.MSG_Ambiguidade_CursoOuGrauAcademico);
            }
        }
    }
}