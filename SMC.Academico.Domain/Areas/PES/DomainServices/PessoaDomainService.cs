using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.Validators;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Models;
using SMC.DadosMestres.Common;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.PES.Data;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Exceptions;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.Util;
using SMC.Inscricoes.ServiceContract.Areas.INS.Data;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class PessoaDomainService : AcademicoContextDomain<Pessoa>
    {
        #region [ Services ]

        private InstituicaoEnsinoDomainService InstituicaoEnsinoDomainService
        {
            get { return this.Create<InstituicaoEnsinoDomainService>(); }
        }

        private IIntegracaoDadoMestreService IntegracaoDadoMestreService
        {
            get { return this.Create<IIntegracaoDadoMestreService>(); }
        }

        private InstituicaoTipoAtuacaoDomainService InstituicaoTipoAtuacaoDomainService
        {
            get { return this.Create<InstituicaoTipoAtuacaoDomainService>(); }
        }

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService
        {
            get { return this.Create<PessoaAtuacaoDomainService>(); }
        }

        #endregion [ Services ]

        private string[] IgnoredProperties
        {
            get
            {
                return new List<string>
                {
                    nameof(PessoaDadosPessoais.Seq),
                    nameof(PessoaDadosPessoais.UsuarioAlteracao),
                    nameof(PessoaDadosPessoais.UsuarioInclusao),
                    nameof(PessoaDadosPessoais.DataAlteracao),
                    nameof(PessoaDadosPessoais.DataInclusao)
                }.ToArray();
            }
        }

        /// <summary>
        /// Grava os dados de uma pessoa com suas dependências
        /// </summary>
        /// <param name="pessoa">Pessoa a ser gravada</param>
        /// <returns>Sequencial da pessoa gravada</returns>
        public long SalvarPessoa(Pessoa pessoa)
        {
            pessoa.Cpf = pessoa.Cpf.SMCRemoveNonDigits();
            if (pessoa.TipoNacionalidade == TipoNacionalidade.Estrangeira)
            {
                this.ValidarCamposPassaporte(pessoa);
            }
            this.SaveEntity(pessoa, new PessoaValidator());

            return pessoa.Seq;
        }

        public Pessoa BuscarPessoa(PessoaFilterSpecification filter)
        {
            return this.SearchBySpecification(filter).FirstOrDefault();
        }

        public Pessoa[] BuscarPessoas(PessoaFilterSpecification filter)
        {
            return this.SearchBySpecification(filter).ToArray();
        }

        /// <summary>
        /// Valida as quantidades de validação conforme a configuração da instituição logada
        /// </summary>
        /// <param name="pessoa">Dados pessoais da pessoa a ser gravada</param>
        /// <param name="tipoAtuacao">Tipo da atuação</param>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        /// <exception cref="PessoaDadosPesosaisQuantidadeFiliacaoMaximaException">Caso tenha mais que a quantidade máxima de filiação configurada para o tipo de atuação</exception>
        /// <exception cref="PessoaDadosPesosaisQuantidadeFiliacaoMinimaException">Caso não tenha a quantidade mínima de filiação obrigatória configurada para o tipo de atuação</exception>
        public void ValidarQuantidadesFiliacao(Pessoa pessoa, TipoAtuacao tipoAtuacao)
        {
            InstituicaoTipoAtuacao configuracao = BuscarConfiguracaoVinculo(tipoAtuacao);

            // Valida as quantidades mínima e máxima de filiação
            if (pessoa.Filiacao.SMCCount() < configuracao.QuantidadeMinimaFiliacaoObrigatoria)
            {
                throw new PessoaDadosPesosaisQuantidadeFiliacaoMinimaException(configuracao.QuantidadeMinimaFiliacaoObrigatoria);
            }
            if (pessoa.Filiacao.SMCCount() > configuracao.QuantidadeMaximaFiliacao)
            {
                throw new PessoaDadosPesosaisQuantidadeFiliacaoMaximaException(configuracao.QuantidadeMaximaFiliacao);
            }
        }

        /// <summary>
        /// Verifica se o tipo de atuação está configurado na instituição logada
        /// </summary>
        /// <param name="tipoAtuacao">Tipo de atuação a ser verificado</param>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        public void ValidarTipoAtuacaoConfiguradoNaInstituicao(TipoAtuacao tipoAtuacao)
        {
            BuscarConfiguracaoVinculo(tipoAtuacao);
        }

        /// <summary>
        /// Busca a configuração do tipo de vínculo na instituição logada
        /// </summary>
        /// <param name="tipoAtuacao">Tipo da atuação</param>
        /// <returns>Instituição tipo atuação com os dados de configuração da atuação na instituição logada</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        private InstituicaoTipoAtuacao BuscarConfiguracaoVinculo(TipoAtuacao tipoAtuacao)
        {
            // O filtro global garante que apenas a instituição logada seja retornada
            var spec = new InstituicaoTipoAtuacaoFilterSpecification() { TipoAtuacao = tipoAtuacao };
            var includes = IncludesInstituicaoTipoAtuacao.InstituicaoEnsino;
            var configuracao = this.InstituicaoTipoAtuacaoDomainService.SearchByKey(spec, includes);

            if (configuracao == null)
                throw new InstituicaoTipoAtuacaoNaoConfiguradaException(tipoAtuacao);

            return configuracao;
        }

        public int BuscarCodigoDePessoaNosDadosMestres(long seqPessoa, TipoPessoa tipoPessoa, long? seqPessoaAtuacao, bool transacao = false)
        {
            // IntegracaoOrigem – representa a integração do dados mestres que você tem em mãos
            // ACADEMICO.PES.pessoa ou ACADEMICO.FIN.pessoa_juridica
            var integracaoOrigem = new IntegracaoDadoMestreData()
            {
                NomeBanco = TOKEN_DADOSMESTRES.BANCO_ACADEMICO,
                NomeTabela = tipoPessoa == TipoPessoa.Fisica ? TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA : TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_JURIDICA,
                NomeAtributoChave = tipoPessoa == TipoPessoa.Fisica ? TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ : TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ_JURIDICA,
                SeqAtributoChaveIntegracao = seqPessoa
            };

            int? codigoPessoa = null;

            // Se é pessoa física, busca primeiro o codigo de pessoa de integração com o GRA..aluno
            if (tipoPessoa == TipoPessoa.Fisica)
            {
                //  NomeBanco = GRA, NomeTabela = aluno e NomeAtributoChave = cod_pessoa
                var tabelaDestino = new TabelaData()
                {
                    NomeBanco = TOKEN_DADOSMESTRES.BANCO_FINANCEIRO,
                    NomeTabela = TOKEN_DADOSMESTRES.BANCO_FINANCEIRO_ALUNO,
                    NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_FINANCEIRO_ALUNO_SEQ
                };
                codigoPessoa = (int?)(transacao ?
                    IntegracaoDadoMestreService.BuscarSeqAtributoChaveIntegracaoTransaction(integracaoOrigem, tabelaDestino) :
                    IntegracaoDadoMestreService.BuscarSeqAtributoChaveIntegracao(integracaoOrigem, tabelaDestino));
            }

            // Se não encontrou a pessoa física no GRA ou é pessoa juridica, procura no CAD
            if (!codigoPessoa.HasValue)
            {
                // NomeBanco = CAD, NomeTabela = pessoa e NomeAtributoChave = pessoa_codigo
                var tabelaDestinoCad = new TabelaData()
                {
                    NomeBanco = TOKEN_DADOSMESTRES.BANCO_CAD,
                    NomeTabela = TOKEN_DADOSMESTRES.BANCO_CAD_PESSOA,
                    NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_CAD_PESSOA_SEQ
                };
                codigoPessoa = (int?)(transacao ?
                    IntegracaoDadoMestreService.BuscarSeqAtributoChaveIntegracaoTransaction(integracaoOrigem, tabelaDestinoCad) :
                    IntegracaoDadoMestreService.BuscarSeqAtributoChaveIntegracao(integracaoOrigem, tabelaDestinoCad));

                // Se não encontrou a pessoa no CAD no DadosMestres, ERRO!
                if (codigoPessoa == null)
                    throw new CodigoPessoaDadosMestreException(tipoPessoa, seqPessoa);
            }

            // Se encontrou a pessoa CAD, é pessoa física e informou a pessoa atuação, insere a integração com o GRA
            if (codigoPessoa.HasValue && tipoPessoa == TipoPessoa.Fisica && seqPessoaAtuacao != null)
            {
                // Busca os dados de contato da pessoa x atuação
                var contato = PessoaAtuacaoDomainService.SearchProjectionByKey(new SMCSeqSpecification<PessoaAtuacao>(seqPessoaAtuacao.Value), x => new
                {
                    EnderecoCorrespondencia = x.Enderecos.FirstOrDefault(i => i.EnderecoCorrespondencia == EnderecoCorrespondencia.Financeira || i.EnderecoCorrespondencia == EnderecoCorrespondencia.AcademicaFinanceira).PessoaEndereco.Endereco,
                    Telefones = x.Telefones.Select(t => t.Telefone),
                    Emails = x.EnderecosEletronicos.Select(e => e.EnderecoEletronico).Where(e => e.TipoEnderecoEletronico == TipoEnderecoEletronico.Email)
                });
                if (contato != null)
                {
                    // Chama a rotina de integração com o GRA
                    InsereIntegracaoGRAData obj = new InsereIntegracaoGRAData()
                    {
                        NomeBanco = TOKEN_DADOSMESTRES.BANCO_ACADEMICO,
                        NomeTabela = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA,
                        NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ,
                        SeqAtributoChaveIntegracao = seqPessoa,
                        CodigoPessoaCAD = codigoPessoa
                    };
                    if (contato.EnderecoCorrespondencia != null)
                    {
                        obj.Cep = contato.EnderecoCorrespondencia.Cep;
                        obj.Logradouro = contato.EnderecoCorrespondencia.Logradouro;
                        obj.Numero = contato.EnderecoCorrespondencia.Numero;
                        obj.Complemento = contato.EnderecoCorrespondencia.Complemento;
                        obj.Bairro = contato.EnderecoCorrespondencia.Bairro;
                        obj.CodigoCidade = contato.EnderecoCorrespondencia.CodigoCidade;
                        obj.NomeCidade = contato.EnderecoCorrespondencia.NomeCidade;
                        obj.SiglaUf = contato.EnderecoCorrespondencia.SiglaUf;
                        obj.CodigoPais = contato.EnderecoCorrespondencia.CodigoPais;
                    }
                    if (contato.Telefones.Count() > 0)
                    {
                        obj.Telefones = contato.Telefones.TransformList<TelefoneData>();
                    }
                    if (contato.Emails.Count() > 0)
                    {
                        obj.Emails = new List<string>();
                        foreach (var email in contato.Emails)
                        {
                            obj.Emails.Add(email.Descricao);
                        }
                    }
                    string msgErro = IntegracaoDadoMestreService.InsereIntegracaoGRA(obj);
                    if (msgErro != null)
                        throw new IntegracaoDadosMestresException(msgErro);
                }
            }

            return (int)codigoPessoa.Value;
        }

        /// <summary>
        /// Busca uma lista de pessoas com seus dados pessoais
        /// </summary>
        /// <param name="filtro">Filtros da listagem de pessoas</param>
        /// <returns>Lista não paginada de pessoas</returns>
        /// <exception cref="PessoaAtuacaoDuplicadaException">Caso o tipo de atuação seja colaborador e o usuário já tenha uma atuação de colaborador cadastrada</exception>
        public List<PessoaExistenteListaVO> BuscarPessoaExistente(PessoaFiltroVO filtro)
        {
            List<PessoaListaVO> pessoasVO = new List<PessoaListaVO>();
            var includes = IncludesPessoa.DadosPessoais
                         | IncludesPessoa.Atuacoes
                         | IncludesPessoa.Filiacao;

            // A regra RN_PES_036 - Busca pessoa existente define que:
            // A primeira busca deve ser feita por Nome, DataNascimento E (CPF ou Passaporte)
            var specNomeData = new PessoaFilterSpecification()
            {
                Nome = filtro.Nome,
                DataNascimento = filtro.DataNascimento,
                DadosPessoaisCadastrados = filtro.DadosPessoaisCadastrados
            };
            var specDocumento = filtro.TipoNacionalidade == TipoNacionalidade.Estrangeira ?
                new PessoaFilterSpecification() { NumeroPassaporte = filtro.NumeroPassaporte, DadosPessoaisCadastrados = filtro.DadosPessoaisCadastrados } :
                new PessoaFilterSpecification() { Cpf = filtro.Cpf, DadosPessoaisCadastrados = filtro.DadosPessoaisCadastrados };

            var specNomeDataEDocumento = new SMCAndSpecification<Pessoa>(specNomeData, specDocumento);
            var pessoas = this.SearchBySpecification(specNomeDataEDocumento, includes).ToList();
            if (pessoas.Count >= 1)
            {
                pessoasVO = pessoas.TransformList<PessoaListaVO>();
            }
            else
            {
                // Caso não encontre nenhum resultado e nos filtros foi informado um documento (CPF ou Passaporte)
                // a busca devera ser refeita por Nome E DataNascimento OU (CPF ou Passaporte)
                if (!string.IsNullOrEmpty(filtro.Cpf) || !string.IsNullOrEmpty(filtro.NumeroPassaporte))
                {
                    var specNomeDataOrDocumento = new SMCOrSpecification<Pessoa>(specNomeData, specDocumento);
                    specNomeDataOrDocumento.SetOrderBy(o => o.DadosPessoais.FirstOrDefault().NomeSocial);
                    specNomeDataOrDocumento.SetOrderBy(o => o.DadosPessoais.FirstOrDefault().Nome);

                    pessoas = this.SearchBySpecification(specNomeDataOrDocumento, includes).ToList();
                    pessoasVO = pessoas.TransformList<PessoaListaVO>();
                }
            }

            // Caso encontre apenas 1 resultado, este já deverá ser devolvido como marcado
            if (pessoasVO.Count == 1)
                pessoasVO.First().Selecionado = true;

            var ret = new List<PessoaExistenteListaVO>();
            foreach (var pessoa in pessoasVO)
            {
                var pessoaExistente = pessoa.Transform<PessoaExistenteListaVO>();
                pessoaExistente.Filiacao = pessoa.Filiacao.Select(s => $"{SMCEnumHelper.GetDescription(s.TipoParentesco)}: {s.Nome}").ToList();
                pessoaExistente.Atuacoes = pessoa.Atuacoes.Select(s => s.TipoAtuacao).ToList();
                ret.Add(pessoaExistente);
            }

            return ret;
        }

        /// <summary>
        /// Retorna todas as instituições de ensino que uma pessoa faz parte
        /// </summary>
        /// <param name="filter">Filtro a ser aplicado para busca das pessoas</param>
        /// <returns>Lista de instituições de ensino</returns>
        public List<SMCDatasourceItem> BuscarInstituicoesEnsinoPessoaSelect(PessoaFilterSpecification filter)
        {
            return SearchProjectionBySpecification(filter, x => new SMCDatasourceItem { Seq = x.SeqInstituicaoEnsino, Descricao = x.InstituicaoEnsino.Nome }).SMCDistinct(x => x.Seq).OrderBy(x => x.Descricao).ToList();
        }

        /// <summary>
        /// Valida se todos os campos do passaporte estão preenchidos ou nulos
        /// </summary>
        /// <param name="pessoa">Dados da pessoa</param>
        public void ValidarCamposPassaporte(Pessoa pessoa)
        {
            int campos = 0;
            campos += string.IsNullOrEmpty(pessoa.NumeroPassaporte) ? 0 : 1;
            campos += pessoa.DataValidadePassaporte == null ? 0 : 1;
            campos += pessoa.CodigoPaisEmissaoPassaporte == null ? 0 : 1;
            if (campos != 0 && campos != 3)
            {
                throw new PessoaPassaporteCamposObrigatoriosException();
            }
        }

        /// <summary>
        /// Formata o nome e o nome social da pessoa informada
        /// </summary>
        /// <typeparam name="T">Tipo da atuação</typeparam>
        /// <param name="pessoa">Pessoa formatar o nome e nome social</param>
        public void FormatarNomesPessoaVo<T>(ref T pessoa) where T : InformacoesPessoaVO
        {
            pessoa.Nome = pessoa.Nome.SMCToPascalCaseName();
            pessoa.NomeSocial = pessoa.NomeSocial.SMCToPascalCaseName();
            pessoa?.Filiacao.ForEach(f => f.Nome = f.Nome.SMCToPascalCaseName());
        }

        /// <summary>
        /// Processa um inscrito, criando ou atualizando seu registro de pessoa
        /// </summary>
        /// <param name="inscrito">Dados do inscrito</param>
        /// <param name="seqEntidadeInstituicao">Sequencial da instituição</param>
        /// <returns>Seqeuncial da pessoa</returns>
        public long ProcessarInscrito(PessoaIntegracaoData inscrito, long seqEntidadeInstituicao)
        {
            // Recupera a 'pessoa'
            var seqPessoa = IntegracaoDadoMestreService.BuscarSeqAtributoChaveIntegracao(
                                                            new IntegracaoDadoMestreData()
                                                            {
                                                                NomeBanco = TOKEN_DADOSMESTRES.BANCO_INSCRICAO,
                                                                NomeTabela = TOKEN_DADOSMESTRES.BANCO_INSCRICAO_INSCRITO,
                                                                NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_INSCRICAO_INSCRITO_SEQ,
                                                                SeqAtributoChaveIntegracao = inscrito.SeqInscrito
                                                            },
                                                            new TabelaData()
                                                            {
                                                                NomeBanco = TOKEN_DADOSMESTRES.BANCO_ACADEMICO,
                                                                NomeTabela = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA,
                                                                NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ
                                                            });
            if (!seqPessoa.HasValue)
            {
                // Caso uma pessoa não tenha sido encontrada, cria uma nova e retorna o sequencial para ser informado ao serviço de ingressante.
                seqPessoa = CriaNovaPessoa(inscrito, seqEntidadeInstituicao);
            }
            else
            {
                // Se existir uma pessoa, verifica se houve alterações nos dados
                AtualizarPessoaExistente(inscrito, seqPessoa.Value);
            }
            return seqPessoa.Value;
        }

        private long CriaNovaPessoa(PessoaIntegracaoData data, long seqInstituicaoEnsino)
        {
            // Cria uma nova pessoa
            var pessoa = data.Transform<Pessoa>();
            pessoa.SeqInstituicaoEnsino = seqInstituicaoEnsino;

            // Cria um novo registro de dados pessoais
            var dadosPessoais = data.Transform<PessoaDadosPessoais>();

            // Adiciona os dados pessoais à pessoa
            pessoa.DadosPessoais = new List<PessoaDadosPessoais>() {
                dadosPessoais
            };

            pessoa.Telefones = new List<PessoaTelefone>();
            foreach (var telefone in data.Telefones)
            {
                pessoa.Telefones.Add(new PessoaTelefone()
                {
                    Pessoa = pessoa,
                    Telefone = telefone.Transform<Telefone>()
                });
            }

            pessoa.Enderecos = new List<PessoaEndereco>();
            foreach (var endereco in data.Enderecos)
            {
                pessoa.Enderecos.Add(new PessoaEndereco()
                {
                    Pessoa = pessoa,
                    Endereco = endereco.Transform<Endereco>()
                });
            }

            pessoa.EnderecosEletronicos = new List<PessoaEnderecoEletronico>();
            foreach (var endEletronico in data.EnderecosEletronicos)
            {
                pessoa.EnderecosEletronicos.Add(new PessoaEnderecoEletronico
                {
                    Pessoa = pessoa,
                    EnderecoEletronico = endEletronico.Transform<EnderecoEletronico>()
                });
            }

            pessoa.Filiacao = new List<PessoaFiliacao>();
            if (!string.IsNullOrWhiteSpace(data.NomeMae))
            {
                pessoa.Filiacao.Add(new PessoaFiliacao()
                {
                    Nome = data.NomeMae,
                    TipoParentesco = TipoParentesco.Mae
                });
            }
            if (!string.IsNullOrWhiteSpace(data.NomePai)
                // Se o nome do pai e da mãe forem o mesmo, o nome será arbitrado como o nome da mãe.
                && data.NomePai != data.NomeMae)
            {
                pessoa.Filiacao.Add(new PessoaFiliacao()
                {
                    Nome = data.NomePai,
                    TipoParentesco = TipoParentesco.Pai
                });
            }

            SaveEntity(pessoa);

            return pessoa.Seq;
        }

        private void AtualizarPessoaExistente(PessoaIntegracaoData data, long seqPessoa)
        {
            bool possuiAlteracoes = false;

            var pessoa = SearchByKey(new SMCSeqSpecification<Pessoa>(seqPessoa),
                                     IncludesPessoa.Filiacao
                                   | IncludesPessoa.Telefones_Telefone
                                   | IncludesPessoa.Enderecos_Endereco
                                   | IncludesPessoa.EnderecosEletronicos_EnderecoEletronico);

            if (pessoa == null)
                throw new SMCApplicationException($"Usuário id {seqPessoa} encontrado nos dados mestres não existe no SGA.");

            pessoa.SeqUsuarioSAS = data.SeqUsuarioSAS;
            pessoa.Cpf = data.Cpf;
            pessoa.NumeroPassaporte = data.NumeroPassaporte;
            pessoa.DataNascimento = data.DataNascimento;
            pessoa.TipoNacionalidade = data.TipoNacionalidade;
            pessoa.CodigoPaisNacionalidade = data.CodigoPaisNacionalidade;

            // Verifica se todo o parentesco existe na pessoa
            possuiAlteracoes |= AtualizaFiliacao(pessoa, data);

            // Verifica por alterações nos telefones
            foreach (var telefone in data.Telefones)
            {
                if (!pessoa.Telefones.Any(f => SMCReflectionHelper.CompareExistingPrimitivePropertyValues(f.Telefone, telefone, IgnoredProperties)))
                {
                    pessoa.Telefones.Add(new PessoaTelefone
                    {
                        SeqPessoa = pessoa.Seq,
                        Telefone = telefone.Transform<Telefone>()
                    });
                    possuiAlteracoes = true;
                }
            }

            // Verifica por alterações nos endereços
            foreach (var endereco in data.Enderecos)
            {
                if (!pessoa.Enderecos.Any(f => SMCReflectionHelper.CompareExistingPrimitivePropertyValues(f.Endereco, endereco, IgnoredProperties)))
                {
                    pessoa.Enderecos.Add(new PessoaEndereco
                    {
                        SeqPessoa = pessoa.Seq,
                        Endereco = endereco.Transform<Endereco>()
                    });
                    possuiAlteracoes = true;
                }
            }

            // Verifica por alterações no endereços eletronicos
            foreach (var enderecoEletronico in data.EnderecosEletronicos)
            {
                if (!pessoa.EnderecosEletronicos.Any(f => SMCReflectionHelper.CompareExistingPrimitivePropertyValues(f.EnderecoEletronico, enderecoEletronico, IgnoredProperties)))
                {
                    pessoa.EnderecosEletronicos.Add(new PessoaEnderecoEletronico
                    {
                        SeqPessoa = pessoa.Seq,
                        EnderecoEletronico = enderecoEletronico.Transform<EnderecoEletronico>()
                    });
                    possuiAlteracoes = true;
                }
            }

            if (possuiAlteracoes)
            {
                SaveEntity(pessoa);
            }
        }

        private bool AtualizaFiliacao(Pessoa pessoa, PessoaIntegracaoData data)
        {
            bool possuiAlteracoes = false;

            if (!string.IsNullOrWhiteSpace(data.NomeMae))
            {
                var mae = pessoa.Filiacao.FirstOrDefault(f => f.TipoParentesco == TipoParentesco.Mae);
                if (mae == null)
                {
                    pessoa.Filiacao.Add(new PessoaFiliacao()
                    {
                        Nome = data.NomeMae,
                        TipoParentesco = TipoParentesco.Mae
                    });
                    possuiAlteracoes = true;
                }
                else if (data.NomeMae != mae.Nome)
                {
                    mae.Nome = data.NomeMae;
                    possuiAlteracoes = true;
                }
            }
            if (!string.IsNullOrWhiteSpace(data.NomePai))
            {
                var pai = pessoa.Filiacao.FirstOrDefault(f => f.TipoParentesco == TipoParentesco.Pai);
                if (pai == null)
                {
                    pessoa.Filiacao.Add(new PessoaFiliacao()
                    {
                        Nome = data.NomePai,
                        TipoParentesco = TipoParentesco.Pai
                    });
                    possuiAlteracoes = true;
                }
                else if (data.NomePai != pai.Nome)
                {
                    pai.Nome = data.NomePai;
                    possuiAlteracoes = true;
                }
            }

            return possuiAlteracoes;
        }
    }
}