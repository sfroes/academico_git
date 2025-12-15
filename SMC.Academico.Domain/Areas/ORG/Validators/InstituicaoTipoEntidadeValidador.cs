using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.Framework.Validation;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.Validators
{
    public class InstituicaoTipoEntidadeValidador : SMCValidator<InstituicaoTipoEntidade>
    {
        protected override void DoValidate(InstituicaoTipoEntidade item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            // Não permite cadastrar situações que sejam da mesma categoria.
            if (item.Situacoes.Count > 1 &&
                item.Situacoes.GroupBy(s => s.SituacaoEntidade.CategoriaAtividade).Any(g => g.Count() > 1))
            {
                this.AddPropertyError(x => x.Situacoes, string.Format(MessagesResource.MSG_SituacaoCategoriaDuplicada));
            }
        }
    }
}