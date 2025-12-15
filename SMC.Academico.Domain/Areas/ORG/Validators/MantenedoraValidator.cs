using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Validation;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.Validators
{
    public class MantenedoraValidator : SMCValidator<Mantenedora>
    {
        /// <summary>
        /// Validações:
        /// •	Um endereço marcado como de correspondencia é obrigatório
        /// •	Pelo menos um telefone obrigatório – Tipo Comercial.
        /// •	Endereços eletrônicos do tipo Email e Web Site obrigatórios. 
        /// •	Rejeitar duplicidade de tipos de telefone
        /// •	Rejeitar duplicidade de tipos de endereço
        /// •	Rejeitar duplicidade de tipos de endereço eletrônico
        /// *   Verifica se os emails informados são válidos
        /// *   Verifica se os website informados são válidos
        /// </summary>
        /// <param name="item">Item a ser validado</param>
        /// <param name="validationResults">Resultado da validação</param>
        protected override void DoValidate(Mantenedora item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            // Verifica duplicidade de endereços com mesmo tipo
            if (item.Enderecos.Count() > 1 &&
                item.Enderecos.GroupBy(t => t.TipoEndereco).Any(g => g.Count() > 1))
            {
                this.AddPropertyError(x => x.Enderecos, String.Format(MessagesResource.MSG_EnderecoDuplicado, MessagesResource.TIPO_ENTIDADE_Mantenedora));
            }

            // Verifica se informou obrigatoriamente 1 endereço de correspondencia
            var endCorrespondencia = item.Enderecos.Count(f => f.Correspondencia.HasValue && f.Correspondencia.Value);
            if (endCorrespondencia == 0)
            {
                this.AddPropertyError(x => x.Enderecos, MessagesResource.MSG_EnderecoCorrespondenciaObrigatorio);
            }
            else if (endCorrespondencia > 1)
            {
                this.AddPropertyError(x => x.Enderecos, MessagesResource.MSG_EnderecoCorrespondenciaMultiplo);
            }

            // Verifica duplicidade de telefones com o mesmo tipo
            if (item.Telefones.Count() > 1 &&
                item.Telefones.GroupBy(t => t.TipoTelefone).Any(g => g.Count() > 1))
            {
                this.AddPropertyError(x => x.Telefones, String.Format(MessagesResource.MSG_TelefoneDuplicado, MessagesResource.TIPO_ENTIDADE_Mantenedora));
            }

            // Verifica se informou um telefone comercial
            if (item.Telefones.Count() == 0 ||
                !item.Telefones.Any(e => e.TipoTelefone == TipoTelefone.Comercial))
            {
                this.AddPropertyError(x => x.Telefones, String.Format(MessagesResource.MSG_TipoTelefoneObrigatorio, TipoTelefone.Comercial.SMCGetDescription()));
            }

            // Verifica duplicidade de endereços eletrônicos com o mesmo tipo
            if (item.EnderecosEletronicos.Count() > 1 &&
                item.EnderecosEletronicos.GroupBy(e => e.TipoEnderecoEletronico).Any(g => g.Count() > 1))
            {
                this.AddPropertyError(x => x.EnderecosEletronicos, String.Format(MessagesResource.MSG_EnderecoEletronicoDuplicado, MessagesResource.TIPO_ENTIDADE_Mantenedora));
            }

            // Verifica se infomrou o email e webmail
            if (item.EnderecosEletronicos.Count < 2 ||
                !item.EnderecosEletronicos.Any(e => e.TipoEnderecoEletronico == TipoEnderecoEletronico.Email) ||
                !item.EnderecosEletronicos.Any(e => e.TipoEnderecoEletronico == TipoEnderecoEletronico.Website))
            {
                this.AddPropertyError(x => x.EnderecosEletronicos, MessagesResource.MSG_EmailEWebSiteObrigatorio);
            }

            // Valida os emails informados
            foreach (var email in item.EnderecosEletronicos.Where(e => e.TipoEnderecoEletronico == TipoEnderecoEletronico.Email))
            {
                if (!SMCValidationHelper.ValidateEmail(email.Descricao))
                {
                    this.AddPropertyError(x => x.EnderecosEletronicos, MessagesResource.MSG_EmailInvalido);
                }
            }

            // Valida os websites informados
            foreach (var web in item.EnderecosEletronicos.Where(e => e.TipoEnderecoEletronico == TipoEnderecoEletronico.Website))
            {
                if (!SMCValidationHelper.ValidateUrl(web.Descricao))
                {
                    this.AddPropertyError(x => x.EnderecosEletronicos, MessagesResource.MSG_WebsiteInvalido);
                }
            }
        }

        /// <summary>
        /// Configura:
        /// - Tamanho máximo do Nome = 100
        /// - Tamanho máximo da Sigla = 15
        /// </summary>
        public override void Configure()
        {
            this.Property(p => p.Nome).HasMaxLength(100);
            this.Property(p => p.Sigla).HasMaxLength(15);
            base.Configure();
        }
    }
}
