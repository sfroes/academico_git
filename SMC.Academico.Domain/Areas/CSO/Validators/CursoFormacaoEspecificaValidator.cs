using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Resources;
using SMC.Framework.Validation;

namespace SMC.Academico.Domain.Areas.CSO.Validators
{
    public class CursoFormacaoEspecificaValidator : SMCValidator<CursoFormacaoEspecifica>
    {
        protected override void DoValidate(CursoFormacaoEspecifica cursoFormacaoEspecifica, SMCValidationResults validationResults)
        {
            base.DoValidate(cursoFormacaoEspecifica, validationResults);

            if (cursoFormacaoEspecifica == null)
                return;

            if (cursoFormacaoEspecifica.DataInicioVigencia > cursoFormacaoEspecifica.DataFimVigencia)
            {
                this.AddPropertyError(p => p.DataFimVigencia, MessagesResource.MSG_CampoDataFimVigenciaIncorreta);
                return;
            }
        }
    }
}