using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Domain.Areas.CSO.Models;
using SMC.Academico.Domain.Areas.CSO.Resources;
using SMC.Framework;
using SMC.Framework.Validation;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CSO.Validators
{
    public class ProgramaValidator : SMCValidator<Programa>
    {
        protected override void DoValidate(Programa programa, SMCValidationResults validationResults)
        {
            base.DoValidate(programa, validationResults);

            if (programa == null)
                return;

            if (programa.DadosOutrosIdiomas.SMCCount() > 1 && programa.DadosOutrosIdiomas.GroupBy(g => ((int)g.Idioma) * 10 + ((int)g.CampoIdioma)).Any(a => a.Count() > 1))
            {
                this.AddPropertyError(p => p.DadosOutrosIdiomas, MessagesResource.MSG_CampoIdiomaDuplicado);
            }

            if (programa.HistoricoNotas.SMCCount() > 1 && !ValidacaoData.ValidarSobreposicaoPeriodos(programa.HistoricoNotas.ToList(), nameof(ProgramaHistoricoNota.DataInicio), nameof(ProgramaHistoricoNota.DataFim)))
            {
                this.AddPropertyError(p => p.HistoricoNotas, MessagesResource.MSG_CampoNotaCapesDuplicado);
            }
        }
    }
}