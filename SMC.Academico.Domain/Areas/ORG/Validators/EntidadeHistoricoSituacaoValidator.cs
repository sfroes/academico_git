using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.Framework.Validation;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.Validators
{
    public class EntidadeHistoricoSituacaoValidator : SMCValidator<EntidadeHistoricoSituacao>
    {
        protected override void DoValidate(EntidadeHistoricoSituacao item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            //Verifica se nenhuma data de término é menor que a data de inicio.
            //A data de término do último período é atualizada com a data de inicio do novo periodo podendo provocar uma inconsistência.
            if (item.Entidade.HistoricoSituacoes.Count(c => c.DataFim.HasValue && c.DataFim < c.DataInicio) > 0)
            {
                this.AddPropertyError(p => p.DataInicio, MessagesResource.MSG_HistoricoSituacaoEntidadePeriodo);
            }

            //Valida se a data do histórico é menor que a data de inclusão do registro
            // if (item.DataInicio < item.Entidade.DataInclusao)
            //    this.AddPropertyError(p => p.DataInicio, MessagesResource.MSG_HistoricoSituacaoEntidadeDataInicioDataInclusao);

            long seqUltimaSituacao = 0L;
            foreach (var historico in item.Entidade.HistoricoSituacoes.OrderBy(o => o.DataInicio))
            {
                if (historico.SeqSituacaoEntidade == seqUltimaSituacao)
                {
                    this.AddPropertyError(p => p.SeqSituacaoEntidade, MessagesResource.MSG_HistoricoSituacaoEntidadeDuplicada);
                }
                seqUltimaSituacao = historico.SeqSituacaoEntidade;
            }
        }
    }
}