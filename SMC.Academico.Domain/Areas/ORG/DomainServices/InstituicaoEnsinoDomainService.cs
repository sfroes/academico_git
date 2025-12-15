using SMC.Academico.Common.Areas.ORG.Exceptions;
using SMC.Academico.Common.Areas.ORG.Includes;
using SMC.Academico.Common.Areas.Shared.Helpers;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.CNC.ValueObjects;
using SMC.Academico.Domain.Areas.ORG.Models;
using SMC.Academico.Domain.Areas.ORG.Specifications;
using SMC.Academico.Domain.Areas.ORG.Validators;
using SMC.Academico.Domain.Areas.ORG.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.DomainServices;
using SMC.Academico.Domain.Models;
using SMC.EstruturaOrganizacional.ServiceContract.Areas.ESO.Interfaces;
using SMC.Framework;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Localidades.Common.Areas.LOC.Enums;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using SMC.Pessoas.ServiceContract.Areas.PES.Interfaces;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ORG.DomainServices
{
    public class InstituicaoEnsinoDomainService : AcademicoContextDomain<InstituicaoEnsino>
    {
        #region Domain Services

        private ArquivoAnexadoDomainService ArquivoAnexadoDomainService => Create<ArquivoAnexadoDomainService>();

        private EntidadeDomainService EntidadeDomainService => Create<EntidadeDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private TipoEntidadeDomainService TipoEntidadeDomainService => Create<TipoEntidadeDomainService>();

        private AtoNormativoDomainService AtoNormativoDomainService => Create<AtoNormativoDomainService>();

        #endregion Domain Services

        #region Services

        public IEstruturaOrganizacionalService EstruturaOrganizacionalService => Create<IEstruturaOrganizacionalService>();
        private IPessoaService PessoaService => Create<IPessoaService>();
        private ILocalidadeService LocalidadeService => this.Create<ILocalidadeService>();

        #endregion Services

        /// <summary>
        /// Busca uma instituição de ensino com as confirgurações de entidade
        /// </summary>
        /// <param name="seq">Sequencia do programa a ser recuperado</param>
        /// <returns>Dados do Programa e configurações de enditade</returns>
        public InstituicaoEnsinoVO BuscarInstituicaoEnsino(long seq)
        {
            var includes = IncludesInstituicaoEnsino.ArquivoLogotipo
                         | IncludesInstituicaoEnsino.Enderecos
                         | IncludesInstituicaoEnsino.EnderecosEletronicos
                         | IncludesInstituicaoEnsino.Telefones;

            this.DisableFilter(FILTER.INSTITUICAO_ENSINO);
            var instituicaoEnsino = this.SearchByKey(new SMCSeqSpecification<InstituicaoEnsino>(seq), includes);
            this.EnableFilter(FILTER.INSTITUICAO_ENSINO);

            var instituicaoEnsinoVO = instituicaoEnsino.Transform<InstituicaoEnsinoVO>();

            if (instituicaoEnsinoVO.ArquivoLogotipo != null)
                instituicaoEnsinoVO.ArquivoLogotipo.GuidFile = instituicaoEnsino.ArquivoLogotipo.UidArquivo.ToString();

            instituicaoEnsinoVO.AtivaAbaAtoNormativo = TipoEntidadeDomainService.PermiteAtoNormativo(instituicaoEnsino.SeqTipoEntidade, TOKEN_TIPO_ENTIDADE_EXTERNADA.INSTITUICAO_ENSINO);
            if (instituicaoEnsinoVO.AtivaAbaAtoNormativo)
            {
                var retorno = AtoNormativoDomainService.BuscarAtoNormativoPorEntidade(seqEntidade: instituicaoEnsino.Seq, SeqInstituicaoEnsino: instituicaoEnsino.SeqInstituicaoEnsino);

                instituicaoEnsinoVO.HabilitaColunaGrauAcademico = retorno.Where(w => w.DescricaoGrauAcademico != null).Select(s => s.DescricaoGrauAcademico).Any();
                instituicaoEnsinoVO.AtoNormativo = retorno;
            }

            return instituicaoEnsinoVO;
        }

        /// <summary>
        /// Busca uma instituição de ensino pela sigla.
        /// </summary>
        /// <param name="sigla">Sigla da instituição.</param>
        /// <returns>Dados da insitituição encontrada.</returns>
        public InstituicaoEnsinoVO BuscarInstituicaoEnsinoPorSigla(string sigla)
        {
            var spec = new InstituicaoEnsinoFilterSpecification() { Sigla = sigla };
            return SearchByKey(spec).Transform<InstituicaoEnsinoVO>();
        }

        /// <summary>
        /// Busca a instituição de ensino da pessoa atuação logada
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <returns>Dados da instituição de ensino</returns>
        public InstituicaoEnsinoVO BuscarInstituicaoEnsinoPorPessoaAtuacao(long seqPessoaAtuacao)
        {
            var registro = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao), p => new InstituicaoEnsinoVO()
            {
                Seq = p.Pessoa.InstituicaoEnsino.Seq,
                Nome = p.Pessoa.InstituicaoEnsino.Nome,
                SeqArquivoLogotipo = p.Pessoa.InstituicaoEnsino.SeqArquivoLogotipo,
            });

            if (registro.SeqArquivoLogotipo.HasValue)
            {
                var arquivoFoto = ArquivoAnexadoDomainService.SearchByKey(new SMCSeqSpecification<ArquivoAnexado>(registro.SeqArquivoLogotipo.Value));
                registro.ArquivoLogotipo = arquivoFoto.Transform<SMCUploadFile>();
                registro.ArquivoLogotipo.GuidFile = arquivoFoto.UidArquivo.ToString();
            }

            return registro;
        }

        /// <summary>
        /// Salva uma Instituicao de Ensino no banco de dados
        /// Instruções:
        /// Incluir primeiramente a entitade e depois incluir a instituição de ensino, e depois fazer update da entidade com o Seq da instituição de ensino
        /// </summary>
        /// <param name="instituicaoEnsino">Instituição de ensino para salvar</param>
        /// <returns>Sequencial da instituição de ensino salva</returns>
        public long SalvarInstituicaoEnsino(InstituicaoEnsino instituicaoEnsino)
        {
            // Informa o tipo da entidade da instituição de ensino
            TipoEntidadeFilterSpecification specTipo = new TipoEntidadeFilterSpecification()
            {
                Token = TOKEN_TIPO_ENTIDADE_EXTERNADA.INSTITUICAO_ENSINO
            };
            var tipoEntidade = TipoEntidadeDomainService.SearchByKey(specTipo);
            instituicaoEnsino.SeqTipoEntidade = tipoEntidade.Seq;

            // Se o arquivo do logotipo não foi alterado, atualiza com conteúdo com o que está no banco
            this.EnsureFileIntegrity(instituicaoEnsino, i => i.SeqArquivoLogotipo, i => i.ArquivoLogotipo);

            // Valida a instituição
            this.EntidadeDomainService.ValidarNomeEntidadeInstituicaoEnsino(instituicaoEnsino, tipoEntidade.Descricao);
            this.EntidadeDomainService.ValidarSiglaEntidadeInstituicaoEnsino(instituicaoEnsino, tipoEntidade.Descricao);

            // Gravação dentro de uma transação para garantir atualização do SeqInstituicaoEnsino atômica
            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                // Apenas na primeira gravação realiza um processo alternativo para preencher o auto relacionamento com instituição de ensino
                if (instituicaoEnsino.IsNew())
                {
                    // Limpa a fk com valor incorreto (0)
                    instituicaoEnsino.SeqInstituicaoEnsino = null;
                    // Grava o grafo completo para gerar o Seq
                    this.SaveEntity(instituicaoEnsino, new InstituicaoEnsinoValidator());

                    // Atualiza o campo de SeqInstituicaoEnsino para auto relacionamento
                    this.UpdateFields(new InstituicaoEnsino { Seq = instituicaoEnsino.Seq, SeqInstituicaoEnsino = instituicaoEnsino.Seq }, x => x.SeqInstituicaoEnsino);
                }
                // Nas atualizações utiliza o processo normal de gravação
                else
                {
                    this.SaveEntity(instituicaoEnsino, new InstituicaoEnsinoValidator());
                }

                transacao.Commit();
            }

            return instituicaoEnsino.Seq;
        }

        /// <summary>
        /// Busca dados da instituição de ensino MEC com endereço - RN_CNC_055 / RN_CNC_052 
        /// </summary>
        public DadosInstituicaoEnsinoVO BuscarDadosInstituicaoEnsinoParaDocumentoAcademico(long seq)
        {
            var instituicaoEnsino = this.SearchByKey(new SMCSeqSpecification<InstituicaoEnsino>(seq), x => x.Enderecos);

            if (instituicaoEnsino == null)
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("curso oferta localidade");

            var retorno = new DadosInstituicaoEnsinoVO
            {
                Nome = FormatarString.Truncate(instituicaoEnsino.Nome, 255),
                CodigoMEC = 338,
                SeqMantenedora = instituicaoEnsino.SeqMantenedora,
                Endereco = new EnderecoVO()
            };

            PreencherCnpj(retorno, instituicaoEnsino);
            PreencherEnderecoComercial(retorno, instituicaoEnsino);

            return retorno;
        }

        private void PreencherCnpj(DadosInstituicaoEnsinoVO retorno, InstituicaoEnsino instituicaoEnsino)
        {
            if (!instituicaoEnsino.CodigoUnidadeSeo.HasValue)
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("código unidade SEO");

            var unidade = EstruturaOrganizacionalService.BuscarUnidadePorId(instituicaoEnsino.CodigoUnidadeSeo.Value);
            if (unidade == null)
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("unidade SEO");

            var pessoaJuridica = PessoaService.BuscarPessoaJuridica(unidade.CodigoPessoaEmpresa);
            if (pessoaJuridica == null || string.IsNullOrEmpty(pessoaJuridica.Cnpj))
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("pessoa jurídica referente à unidade SEO");

            var cnpj = FormatarString.ObterSomenteNumeros(pessoaJuridica.Cnpj);
            retorno.Cnpj = FormatarString.Truncate(cnpj, 14);
        }

        private void PreencherEnderecoComercial(DadosInstituicaoEnsinoVO retorno, InstituicaoEnsino instituicaoEnsino)
        {
            var enderecoComercial = instituicaoEnsino.Enderecos.FirstOrDefault(a => a.TipoEndereco == TipoEndereco.Comercial);
            if (enderecoComercial == null)
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("endereço comercial");

            retorno.Endereco.Logradouro = FormatarString.Truncate(enderecoComercial.Logradouro, 255);

            if (!string.IsNullOrEmpty(enderecoComercial.Numero))
                retorno.Endereco.Numero = FormatarString.Truncate(enderecoComercial.Numero, 60);

            if (!string.IsNullOrEmpty(enderecoComercial.Complemento))
                retorno.Endereco.Complemento = FormatarString.Truncate(enderecoComercial.Complemento, 60);

            if (!string.IsNullOrEmpty(enderecoComercial.Bairro))
                retorno.Endereco.Bairro = FormatarString.Truncate(enderecoComercial.Bairro, 60);
            else
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("bairro do endereço");

            if (!string.IsNullOrEmpty(enderecoComercial.Cep))
            {
                var cep = FormatarString.ObterSomenteNumeros(enderecoComercial.Cep);
                retorno.Endereco.Cep = FormatarString.Truncate(cep, 8);
            }
            else
            {
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("cep do endereço");
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
                            throw new InstituicaoEnsinoInformacaoNaoEncontradaException("código do município IBGE do endereço");
                    }
                    else
                        throw new InstituicaoEnsinoInformacaoNaoEncontradaException("cidade do endereço");
                }
                else
                    throw new InstituicaoEnsinoInformacaoNaoEncontradaException("código da cidade do endereço");
            }
            else
                throw new InstituicaoEnsinoInformacaoNaoEncontradaException("sigla uf do endereço");
        }
    }
}