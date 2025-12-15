using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.Resources;
using SMC.Framework.Validation;
using System.Linq;

namespace SMC.Academico.Domain.Areas.CUR.Validators
{
    public class DivisaoCurricularValidator : SMCValidator<DivisaoCurricular>
    {
        protected override void DoValidate(DivisaoCurricular item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            //Recupera a lista de números para validações
            var numeros = item.Itens.Select(x => (int)x.Numero);

            //Valida se não existe itens em duplicidade
            if (numeros.GroupBy(n => n).Any(c => c.Count() > 1))
                this.AddPropertyError(p => p.Itens, MessagesResource.MSG_DivisaoCurricularItemDuplicado);

            //Valida se os id´s dos itens estão em sequencia
            var min = numeros.Min();
            var max = numeros.Max();
            var listaSequencial = Enumerable.Range(min, max - min + 1);

            if (!numeros.SequenceEqual(listaSequencial))
                this.AddPropertyError(p => p.Itens, MessagesResource.MSG_DivisaoCurricularItemSequencial);

            //Valida itens com a mesma descrição
            if (item.Itens.GroupBy(x => x.Descricao.ToLower().Trim()).Where(x => x.Count() > 1).Any())
                this.AddPropertyError(p => p.Itens, MessagesResource.MSG_DivisaoCurricularItemNomeDuplicado1);
        }
    }
}