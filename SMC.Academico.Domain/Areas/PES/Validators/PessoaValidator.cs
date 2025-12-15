using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Resources;
using SMC.Framework;
using SMC.Framework.Validation;
using System.Text.RegularExpressions;

namespace SMC.Academico.Domain.Areas.PES.Validators
{
    public class PessoaValidator : SMCValidator<Pessoa>
    {
        protected override void DoValidate(Pessoa item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            var dadosPessoaisAtuais = item?.RecuperarDadosPessoaisAtuais();

            if (dadosPessoaisAtuais == null)
                return;

            // Valida o nome, nome social e nomes dos filados segundo a regra RN_PES_007
            var regexNome = new Regex(REGEX.NOME);
            if (!regexNome.IsMatch(dadosPessoaisAtuais.Nome))
            {
                this.AddPropertyError(nameof(dadosPessoaisAtuais.Nome), string.Format(MessagesResource.MSG_NOME_INVALIDO, MessagesResource.CAMPO_NOME));
            }
            if (!string.IsNullOrEmpty(dadosPessoaisAtuais.NomeSocial) && !regexNome.IsMatch(dadosPessoaisAtuais.NomeSocial))
            {
                this.AddPropertyError(nameof(dadosPessoaisAtuais.NomeSocial), string.Format(MessagesResource.MSG_NOME_INVALIDO, MessagesResource.CAMPO_NOME_SOCIAL));
            }
            if (item.Filiacao.SMCCount(c => !regexNome.IsMatch(c.Nome)) > 0)
            {
                this.AddPropertyError(nameof(item.Filiacao), string.Format(MessagesResource.MSG_NOME_INVALIDO, MessagesResource.CAMPO_NOME_FILIADO));
            }
        }
    }
}