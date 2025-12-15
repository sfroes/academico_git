using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Resources;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.Validation;
using System.Linq;
using System.Text.RegularExpressions;

namespace SMC.Academico.Domain.Areas.PES.Validators
{
    public class ReferenciaFamiliarValidator : SMCValidator<ReferenciaFamiliar>
    {
        protected override void DoValidate(ReferenciaFamiliar item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);
                       
            // Valida o nome do parente segundo a regra RN_PES_007
            var regexNome = new Regex(REGEX.NOME);
            if (!regexNome.IsMatch(item.NomeParente))
            {
                this.AddPropertyError(nameof(item.NomeParente), string.Format(MessagesResource.MSG_NOME_INVALIDO, MessagesResource.CAMPO_NOME_PARENTE));
            }

            //Recupera dados da pessoa atuação para verificação de endereços eletrônicos e telefones duplicados
            var pessoaAtuacao = new PessoaAtuacaoDomainService().BuscarPessoaAtuacao(item.SeqPessoaAtuacao);

            //RN_PES_017 Consistência telefone referência familiar
            if (item.Telefones != null && pessoaAtuacao.Telefones.Select(s=> s.Numero).Intersect(item.Telefones.Select(s=>s.Numero)).Any())
            {
                this.AddPropertyError(nameof(item.Telefones), MessagesResource.MSG_TELEFONE_UTILIZADO);
            }

            //RN_PES_018 Consistência e-mail referência familiar
            if (item.EnderecosEletronicos == null || !item.EnderecosEletronicos.Any(a => a.TipoEnderecoEletronico == TipoEnderecoEletronico.Email))
            {
                this.AddPropertyError(nameof(item.EnderecosEletronicos), MessagesResource.MSG_EMAIL_OBRIGATORIO);
            }

            var listaEnderecoEletronico = pessoaAtuacao.EnderecosEletronicos.Select(s => new {Descricao = s.DescricaoEnderecoEletronico , TipoEnderecoEletronico = s.TipoEnderecoEletronico });
            
            //RN_PES_018 Consistência e-mail referência familiar
            if (listaEnderecoEletronico.Intersect(item.EnderecosEletronicos.Select(s => new { s.Descricao, s.TipoEnderecoEletronico })).Any())
            {
                this.AddPropertyError(nameof(item.EnderecosEletronicos), MessagesResource.MSG_EMAIL_UTILIZADO);
            }
        }
    }
}
