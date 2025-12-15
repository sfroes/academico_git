using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.EstruturaOrganizacional.ServiceContract.Areas.ESO.Interfaces;
using SMC.Framework.Domain.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using SMC.Framework.Validation;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Pessoas.ServiceContract.Areas.PES.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class MantenedoraDomainService : AcademicoContextDomain<Mantenedora>
    {
        #region Services

        public IEstruturaOrganizacionalService EstruturaOrganizacionalService => Create<IEstruturaOrganizacionalService>();
        private IPessoaService PessoaService => Create<IPessoaService>();
        private ILocalidadeService LocalidadeService => this.Create<ILocalidadeService>();

        #endregion Services

        public MantenedoraVO BuscarMantenedora(long seq)
        {
            var mantenedora = this.SearchByKey(new SMCSeqSpecification<Mantenedora>(seq),
                  IncludesMantenedora.ArquivoLogotipo
                | IncludesMantenedora.Enderecos
                | IncludesMantenedora.EnderecosEletronicos
                | IncludesMantenedora.Telefones);

            var retorno = mantenedora.Transform<MantenedoraVO>();

            if (mantenedora.SeqArquivoLogotipo.HasValue)
                retorno.ArquivoLogotipo.GuidFile = mantenedora.ArquivoLogotipo.UidArquivo.ToString();

            return retorno;
        }

        /// <summary>
        /// Salvar Mantenedora
        /// </summary>
        /// <param name="mantenedora">Dados da mantenedora</param>
        /// <returns>Sequencial da mantenedora</returns>
        public long SalvarMantenedora(MantenedoraVO mantenedora)
        {
            // Transforma o VO em model
            Mantenedora modelo = mantenedora.Transform<Mantenedora>();

            // Valida o modelo
            var validator = new MantenedoraValidator();
            var results = validator.Validate(modelo);
            if (!results.IsValid)
            {
                var errorList = new List<SMCValidationResults>();
                errorList.Add(results);
                throw new SMCInvalidEntityException(errorList);
            }

            // Se o arquivo do logotipo não foi alterado, atualiza com conteúdo com o que está no banco
            this.EnsureFileIntegrity(modelo, x => x.SeqArquivoLogotipo, x => x.ArquivoLogotipo);

            // Salva a mantenedora
            SaveEntity(modelo);
            return modelo.Seq;
        }

        /// <summary>
        /// Busca dados da mantenedora com endereço - RN_CNC_054 
        /// </summary>
        public DadosMantenedoraVO BuscarMantenedoraParaDocumentoAcademico(long seq)
        {
            var mantenedora = this.SearchByKey(new SMCSeqSpecification<Mantenedora>(seq), x => x.Enderecos);

            if (mantenedora == null)
                throw new MantenedoraInformacaoNaoEncontradaException("mantenedora");

            var retorno = new DadosMantenedoraVO
            {
                RazaoSocial = FormatarString.Truncate(mantenedora.Nome, 255),
                Endereco = new CNC.ValueObjects.EnderecoVO()
            };

            PreencherCnpj(retorno, mantenedora);
            PreencherEnderecoComercial(retorno, mantenedora);

            return retorno;
        }

        private void PreencherCnpj(DadosMantenedoraVO retorno, Mantenedora mantenedora)
        {
            if (!mantenedora.CodigoUnidadeSeo.HasValue)
                throw new MantenedoraInformacaoNaoEncontradaException("código unidade SEO");

            var unidade = EstruturaOrganizacionalService.BuscarUnidadePorId(mantenedora.CodigoUnidadeSeo.Value);
            if (unidade == null)
                throw new MantenedoraInformacaoNaoEncontradaException("unidade SEO");

            var pessoaJuridica = PessoaService.BuscarPessoaJuridica(unidade.CodigoPessoaEmpresa);
            if (pessoaJuridica == null || string.IsNullOrEmpty(pessoaJuridica.Cnpj))
                throw new MantenedoraInformacaoNaoEncontradaException("pessoa jurídica referente à unidade SEO");

            var cnpj = FormatarString.ObterSomenteNumeros(pessoaJuridica.Cnpj);
            retorno.Cnpj = FormatarString.Truncate(cnpj, 14);
        }

        private void PreencherEnderecoComercial(DadosMantenedoraVO retorno, Mantenedora mantenedora)
        {
            var enderecoComercial = mantenedora.Enderecos.FirstOrDefault(e => e.TipoEndereco == TipoEndereco.Comercial);
            if (enderecoComercial == null)
                throw new MantenedoraInformacaoNaoEncontradaException("endereço comercial");

            retorno.Endereco.Logradouro = FormatarString.Truncate(enderecoComercial.Logradouro, 255);

            if (!string.IsNullOrEmpty(enderecoComercial.Numero))
                retorno.Endereco.Numero = FormatarString.Truncate(enderecoComercial.Numero, 60);

            if (!string.IsNullOrEmpty(enderecoComercial.Complemento))
                retorno.Endereco.Complemento = FormatarString.Truncate(enderecoComercial.Complemento, 60);

            if (!string.IsNullOrEmpty(enderecoComercial.Bairro))
                retorno.Endereco.Bairro = FormatarString.Truncate(enderecoComercial.Bairro, 60);
            else
                throw new MantenedoraInformacaoNaoEncontradaException("bairro do endereço");

            if (!string.IsNullOrEmpty(enderecoComercial.Cep))
            {
                var cep = FormatarString.ObterSomenteNumeros(enderecoComercial.Cep);
                retorno.Endereco.Cep = FormatarString.Truncate(cep, 8);
            }
            else
            {
                throw new MantenedoraInformacaoNaoEncontradaException("cep do endereço");
            }

            if (!string.IsNullOrEmpty(enderecoComercial.SiglaUf))
            {
                retorno.Endereco.Uf = FormatarString.Truncate(enderecoComercial.SiglaUf, 2);

                if (enderecoComercial.CodigoCidade.GetValueOrDefault() > 0)
                {
                    var cidade = this.LocalidadeService.BuscarCidade(enderecoComercial.CodigoCidade.Value, enderecoComercial.SiglaUf);
                    if (cidade != null)
                    {
                        retorno.Endereco.NomeMunicipio = FormatarString.Truncate(cidade.Nome, 255);

                        if (cidade.CodigoMunicipioIBGE.HasValue)
                            retorno.Endereco.CodigoMunicipio = FormatarString.Truncate(cidade.CodigoMunicipioIBGE.ToString(), 7);
                        else
                            throw new MantenedoraInformacaoNaoEncontradaException("código do município IBGE do endereço");
                    }
                    else
                        throw new MantenedoraInformacaoNaoEncontradaException("cidade do endereço");
                }
                else
                    throw new MantenedoraInformacaoNaoEncontradaException("código da cidade do endereço");
            }
            else
                throw new MantenedoraInformacaoNaoEncontradaException("sigla uf do endereço");
        }

        
    }
}
