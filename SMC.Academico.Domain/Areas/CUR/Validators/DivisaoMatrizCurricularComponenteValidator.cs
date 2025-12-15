using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Framework;
using SMC.Framework.Validation;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Validators
{
    public class DivisaoMatrizCurricularComponenteValidator : SMCValidator<DivisaoMatrizCurricularComponente>
    {
        protected override void DoValidate(DivisaoMatrizCurricularComponente item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);
            if (item == null)
                return;

            if (item.CriterioAprovacao != null)
            {
                var divisoesComNotaMaxima = item.DivisoesComponente
                    .Where(w => w.NotaMaxima.GetValueOrDefault() > 0);

                var divisoesSemNotaMaximaComEscalaConceito = item.DivisoesComponente
                    .Except(divisoesComNotaMaxima)
                    .Where(a => a.EscalaApuracao?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito);

                var divisoesComEscalaConceito = item.DivisoesComponente
                    .Where(a => a.EscalaApuracao?.TipoEscalaApuracao == TipoEscalaApuracao.Conceito);

                var divisoesComApuracaoFrequencia = item.DivisoesComponente
                    .Where(a => a.ApurarFrequencia).ToList();

                if (item.CriterioAprovacao.ApuracaoNota)
                {
                    if (!divisoesComNotaMaxima.Any() || divisoesSemNotaMaximaComEscalaConceito.Any())
                    {
                        this.AddPropertyError(p => p.DivisaoMatrizCurricular, MessagesResource.MSG_DivisaoMatrizCurricularComponenteApuracaoNota);
                    }
                    if (divisoesComNotaMaxima.Sum(s => s.NotaMaxima ?? 0) != item.CriterioAprovacao.NotaMaxima)
                    {
                        this.AddPropertyError(p => p.DivisaoMatrizCurricular, MessagesResource.MSG_DivisaoMatrizCurricularComponenteNotaMaxima);
                    }
                }
                else if (item.CriterioAprovacao.EscalaApuracao.TipoEscalaApuracao != TipoEscalaApuracao.Conceito && divisoesComEscalaConceito.Any())
                {
                    this.AddPropertyError(p => p.DivisaoMatrizCurricular, MessagesResource.MSG_DivisaoMatrizCurricularComponenteConceito);
                }

                if (item.CriterioAprovacao.ApuracaoFrequencia && !divisoesComApuracaoFrequencia.Any())
                    this.AddPropertyError(p => p.DivisaoMatrizCurricular, MessagesResource.MSG_DivisaoMatrizCurricularComponenteApuracaoFrequencia);
            }

            var compoente = item.GrupoCurricularComponente.ComponenteCurricular;
            if (item.ComponentesCurricularSubstitutos.SMCCount(c => c.CargaHoraria != compoente.CargaHoraria || c.Credito != compoente.Credito) > 0)
            {
                this.AddPropertyError(p => p.ComponentesCurricularSubstitutos, MessagesResource.MSG_DivisaoMatrizCurricularComponenteSubstituto);
            }
        }
    }
}