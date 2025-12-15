using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Resources;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.Validation;
using System;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.Validators
{
    public class EntidadeValidator : SMCValidator<Entidade>
    {
        /// <summary>
        /// Validações:
        /// •	Rejeitar duplicidade de tipos de telefone
        /// •	Rejeitar duplicidade de tipos de endereço
        /// •	Rejeitar duplicidade de tipos de endereço eletrônico
        /// *   Verifica se os emails informados são válidos
        /// *   Verifica se os website informados são válidos
        /// *   Verifica consistência de Data de inicio e fim
        /// </summary>
        /// <param name="item">Item a ser validado</param>
        /// <param name="validationResults">Resultado da validação</param>
        protected override void DoValidate(Entidade item, SMCValidationResults validationResults)
        {
            base.DoValidate(item, validationResults);

            if (item == null)
                return;

            // Alteração unidade seo = 0 para NULL
            item.CodigoUnidadeSeo = item.CodigoUnidadeSeo == 0 ? null : item.CodigoUnidadeSeo;

            // Valida a data de inicio e fim
            if (item.DataInicioSituacaoAtual > item.DataFimSituacaoAtual)
                this.AddPropertyError(x => x.DataInicioSituacaoAtual, string.Format(MessagesResource.MSG_DataVigenciaInicialMaiorQueFinal, MessagesResource.TIPO_ENTIDADE_Entidade));

            // Validações de endereço
            if (item.Enderecos != null)
            {
                // Duplicidade de endereços
                if (item.Enderecos.Count() > 1 && item.Enderecos.GroupBy(e => e.TipoEndereco).Any(g => g.Count() > 1))
                {
                    this.AddPropertyError(x => x.Enderecos, string.Format(MessagesResource.MSG_EnderecoDuplicado, MessagesResource.TIPO_ENTIDADE_Entidade));
                }

                // Mais de um endereço de correspondência
                if (item.Enderecos.Count() > 1 && item.Enderecos.SMCCount(f => f.Correspondencia.HasValue && f.Correspondencia.Value) > 1)
                {
                    this.AddPropertyError(x => x.Enderecos, string.Format(MessagesResource.MSG_EnderecoCorrespondenciaMultiplo, MessagesResource.TIPO_ENTIDADE_Entidade));
                }
            }

            // Duplicidade de endereço eletronico
            if (item.EnderecosEletronicos != null && item.EnderecosEletronicos.Count() > 1 &&
                item.EnderecosEletronicos.GroupBy(e => new { e.TipoEnderecoEletronico, e.CategoriaEnderecoEletronico }).Any(g => g.Count() > 1))
            {
                this.AddPropertyError(x => x.EnderecosEletronicos, String.Format(MessagesResource.MSG_EnderecoEletronicoDuplicado, MessagesResource.TIPO_ENTIDADE_Entidade));
            }

            // Duplicidade de telefone
            if (item.Telefones != null && item.Telefones.Count() > 1 &&
                item.Telefones.GroupBy(t => new { t.TipoTelefone, t.CategoriaTelefone }).Any(g => g.Count() > 1))
            {
                this.AddPropertyError(x => x.Telefones, String.Format(MessagesResource.MSG_TelefoneDuplicado, MessagesResource.TIPO_ENTIDADE_Entidade));
            }

            if (item.EnderecosEletronicos != null)
            {
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
        }

        /// <summary>
        /// Configura:
        /// - Tamanho máximo da Sigla = 15
        /// </summary>
        public override void Configure()
        {
            this.Property(x => x.Sigla).HasMaxLength(15);
            base.Configure();
        }
    }
}