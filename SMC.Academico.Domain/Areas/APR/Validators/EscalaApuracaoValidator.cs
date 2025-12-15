using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Resources;
using SMC.Framework;
using SMC.Framework.Validation;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.Validators
{
    public class EscalaApuracaoValidator : SMCValidator<EscalaApuracao>
    {
        protected override void DoValidate(EscalaApuracao item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            if (item.ApuracaoAvaliacao && item.TipoEscalaApuracao == TipoEscalaApuracao.Conceito)
                this.AddPropertyError(p => p.ApuracaoAvaliacao, MessagesResource.MSG_EscalaApuracaoAvaliacaoConceito);

            if (!item.ApuracaoAvaliacao && !item.ApuracaoFinal)
                this.AddError("AvaliacaoApuracaoObrigatoria", MessagesResource.MSG_EscalaApuracaoAvaliacaoObrigatoria);

            //Não é necessário validar uma vez que o usuário optou (via modal de confirmação) para que a operação pudesse ser efetivada.
            //if (!item.ApuracaoFinal && item.CriteriosAprovacao.Count > 0)
            //    this.AddPropertyError(p => p.ApuracaoAvaliacao, MessagesResource.MSG_EscalaApuracaoUtilizadaPorCriterio);

            if (item.Itens.Count < 2)
                this.AddPropertyError(p => p.Itens, MessagesResource.MSG_EscalaApuracaoItensObrigatorios);

            if (item.Itens.SMCCount(i => i.Aprovado) == 0 || item.Itens.SMCCount(i => !i.Aprovado) == 0)
                this.AddPropertyError(p => p.Itens, MessagesResource.MSG_EscalaApuracaoItensObrigatoriosSimNao);

            if (item.Itens.Count > 0)
            {
                var percentuais = item.Itens.Select(x => new { x.PercentualMinimo, x.PercentualMaximo }).OrderBy(i => i.PercentualMinimo).ToList();

                for (int i = 0; i < percentuais.Count - 1; i++)
                {
                    if (percentuais[i].PercentualMaximo >= percentuais[i + 1].PercentualMinimo)
                        throw new EscalaApuracaoSequenciaIncorretaPercentuaisException();
                }
            }
        }
    }
}