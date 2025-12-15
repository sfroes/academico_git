using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Areas.PES.Exceptions;
using SMC.Academico.Common.Areas.PES.Includes;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.PES.Models;
using SMC.Academico.Domain.Areas.PES.Specifications;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Helpers;
using SMC.DadosMestres.Common;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.DadosMestres.ServiceContract.Areas.PES.Data;
using SMC.DadosMestres.ServiceContract.Areas.PES.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Localidades.ServiceContract.Areas.LOC.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.PES.DomainServices
{
    public class FuncionarioDomainService : AcademicoContextDomain<Funcionario>
    {
        #region [ Service ]

        private ILocalidadeService LocalidadeService => Create<ILocalidadeService>();

        private IIntegracaoDadoMestreService IntegracaoDadoMestreService => Create<IIntegracaoDadoMestreService>();

        #endregion [ Service ]

        #region [ DomainService ]

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private PessoaDomainService PessoaDomainService => Create<PessoaDomainService>();
        private FuncionarioVinculoDomainService FuncionarioVinculoDomainService => Create<FuncionarioVinculoDomainService>();
        private TipoFuncionarioDomainService TipoFuncionarioDomainService => Create<TipoFuncionarioDomainService>();

        #endregion [ DomainService ]

        /// <summary>
        /// Busca funcionários com seus dados pessoais
        /// </summary>
        /// <param name="filtros">Filtros para busca</param>
        /// <returns>Dados paginados dos funcionários</returns>
        public SMCPagerData<FuncionarioListaVO> BuscarFuncionarios(FuncionarioFiltroVO funcionarioFiltroVO)
        {
            var filtros = funcionarioFiltroVO.Transform<FuncionarioFilterSpecification>();

            if (funcionarioFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarApenasFiltros(this, FILTER.INSTITUICAO_ENSINO);
            }

            var dataAtual = DateTime.Today;
            var funcionario = SearchProjectionBySpecification(filtros, p => new FuncionarioListaVO()
            {
                Seq = p.Seq,
                Cpf = p.Pessoa.Cpf,
                NumeroPassaporte = p.Pessoa.NumeroPassaporte,
                Falecido = p.Pessoa.Falecido,
                DataNascimento = p.Pessoa.DataNascimento,
                Nome = p.DadosPessoais.Nome,
                NomeSocial = p.DadosPessoais.NomeSocial,
                Sexo = p.DadosPessoais.Sexo,
                VinculosAtivos = p.Vinculos
                    .Where(vinculo => vinculo.DataInicio <= dataAtual && (!vinculo.DataFim.HasValue || vinculo.DataFim >= dataAtual))
                    .OrderByDescending(o => o.DataInicio)
                    .ThenByDescending(o => o.DataFim)
                    .Select(s => new FuncionarioVinculoListaVO()
                    {
                        Seq = s.Seq,
                        SeqFuncionario = s.SeqFuncionario,
                        SeqTipoFuncionario = s.SeqTipoFuncionario,
                        DescricaoTipoFuncionario = s.TipoFuncionario.DescricaoMasculino,
                        DataInicio = s.DataInicio,
                        DataFim = s.DataFim,
                        DescricaoEntidadeCadastrada = s.EntidadeVinculo.Nome
                    }).ToList()
            }, out int total).ToList();

            if (funcionarioFiltroVO.IgnorarFiltros)
            {
                FilterHelper.AtivarFiltros(this);
            }

            var funcionarioPagerData = new SMCPagerData<FuncionarioListaVO>(funcionario, total);

            return funcionarioPagerData;
        }

        /// <summary>
        /// Recupera um funcionário com seus dados pessoais e de contato
        /// </summary>
        /// <param name="seq">Sequencial do funcionário</param>
        /// <returns>Dados do funcionario com suas dependências</returns>
        public FuncionarioVO BuscarFuncionario(long seq)
        {
            PessoaDomainService.ValidarTipoAtuacaoConfiguradoNaInstituicao(TipoAtuacao.Colaborador);

            IncludesFuncionario includesFuncionario = IncludesFuncionario.DadosPessoais_ArquivoFoto
                                                      | IncludesFuncionario.Enderecos_PessoaEndereco_Endereco
                                                      | IncludesFuncionario.EnderecosEletronicos_EnderecoEletronico
                                                      | IncludesFuncionario.Pessoa_Filiacao
                                                      | IncludesFuncionario.Telefones_Telefone;

            Funcionario funcionario = this.SearchByKey(seq, includesFuncionario);

            var funcionarioVo = funcionario.Transform<FuncionarioVO>();

            if (funcionario.DadosPessoais.SeqArquivoFoto.HasValue)
                funcionarioVo.ArquivoFoto.GuidFile = funcionario.DadosPessoais.ArquivoFoto.UidArquivo.ToString();

            if (funcionarioVo.Enderecos.SMCCount() > 0)
            {
                // Preenche as descrições dos países
                var paises = LocalidadeService.BuscarPaisesValidosCorreios();
                funcionarioVo.Enderecos.SMCForEach(f => f.DescPais = paises.SingleOrDefault(s => s.Codigo == f.CodigoPais)?.Nome);
            }

            PessoaAtuacaoDomainService.AplicarValidacaoPermiteAlterarCpf(ref funcionarioVo);

            return funcionarioVo;
        }

        /// <summary>
        /// Valida as configurações iniciais do funcionário
        /// </summary>
        /// <returns>Novo objeto de colaborador caso o tipo de vínculo esteja configurado</returns>
        /// <exception cref="InstituicaoTipoAtuacaoNaoConfiguradaException">Caso o tipo de atuação não esteja configurado na instituição</exception>
        public FuncionarioVO BuscarConfiguracaoFuncionario()
        {
            PessoaDomainService.ValidarTipoAtuacaoConfiguradoNaInstituicao(TipoAtuacao.Funcionario);
            var retorno = new FuncionarioVO();
            PessoaAtuacaoDomainService.AplicarValidacaoPermiteCadastrarNomeSocial(ref retorno);
            return retorno;
        }

        /// <summary>
        /// Grava um funcionario com seus dados pessoais e dados de contato
        /// </summary>
        /// <param name="funcionarioVo">Dados do funcionario a ser gravado</param>
        /// <returns>Sequencial do funcionario</returns>
        public long SalvarFuncionario(FuncionarioVO funcionarioVo)
        {
            PessoaAtuacaoDomainService.RestaurarCamposReadonlyCpf(ref funcionarioVo);
            PessoaDomainService.FormatarNomesPessoaVo(ref funcionarioVo);
            Pessoa pessoa = RecuperarPessoaComDependencias(funcionarioVo.SeqPessoa);
            Funcionario funcionario;

            //Atualização dos campos de pessoa
            pessoa.SeqInstituicaoEnsino = funcionarioVo.SeqInstituicaoEnsino;
            pessoa.SeqUsuarioSAS = funcionarioVo.SeqUsuarioSAS;
            pessoa.Cpf = funcionarioVo.Cpf;
            pessoa.NumeroPassaporte = funcionarioVo.NumeroPassaporte;
            pessoa.CodigoPaisEmissaoPassaporte = funcionarioVo.CodigoPaisEmissaoPassaporte;
            pessoa.DataValidadePassaporte = funcionarioVo.DataValidadePassaporte;
            pessoa.DataNascimento = funcionarioVo.DataNascimento;
            pessoa.Falecido = funcionarioVo.Falecido;
            pessoa.TipoNacionalidade = funcionarioVo.TipoNacionalidade;
            pessoa.CodigoPaisNacionalidade = funcionarioVo.CodigoPaisNacionalidade;
            pessoa.Filiacao = funcionarioVo.Filiacao.TransformList<PessoaFiliacao>();

            var dadosPessoais = funcionarioVo.Transform<PessoaDadosPessoais>();
            dadosPessoais.Seq = 0;
            dadosPessoais.Atuacoes = pessoa.Atuacoes.Where(w => w.TipoAtuacao != TipoAtuacao.Funcionario).ToList();

            //Inclui os dados pessoais no histórico
            pessoa.DadosPessoais.Add(dadosPessoais);

            //Se as fotos não foram atualizadas, atualiza com o conteúdo do banco
            pessoa.DadosPessoais.SMCForEach(f => this.EnsureFileIntegrity(f, x => x.SeqArquivoFoto, x => x.ArquivoFoto));

            this.PessoaDomainService.ValidarQuantidadesFiliacao(pessoa, TipoAtuacao.Funcionario);

            using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
            {
                //Grava a pessoa com suas dependências
                this.PessoaDomainService.SalvarPessoa(pessoa);

                // Recupera os dados do colaborador do vo
                funcionario = funcionarioVo.Transform<Funcionario>();
                // Campo utilizado pela formação acadêmica
                funcionario.Descricao = null;
                funcionario.TipoAtuacao = TipoAtuacao.Funcionario;
                // Remove o último nível para evitar duplicidade
                funcionario.Telefones.SMCForEach(f => f.Telefone = null);
                funcionario.Enderecos.SMCForEach(f => f.PessoaEndereco = null);
                funcionario.EnderecosEletronicos.SMCForEach(f => f.EnderecoEletronico = null);

                if (funcionario.Seq == 0)
                {

                    FuncionarioVinculo vinculo = new FuncionarioVinculo();
                    vinculo.SeqTipoFuncionario = funcionarioVo.SeqTipoFuncionario;
                    vinculo.DataInicio = funcionarioVo.DataInicioVinculo;
                    vinculo.DataFim = funcionarioVo.DataFimVinculo;
                    vinculo.SeqEntidadeVinculo = funcionarioVo.SeqEntidadeVinculo;

                    funcionario.Vinculos = new List<FuncionarioVinculo>();
                    funcionario.Vinculos.Add(vinculo);

                    //caso seja inclusão, insere uma pessoa física nos Dados Mestres
                    var pessoaFisicaDadosMetres = funcionarioVo.Transform<InserePessoaFisicaData>();

                    pessoaFisicaDadosMetres.NomeBanco = TOKEN_DADOSMESTRES.BANCO_ACADEMICO;
                    pessoaFisicaDadosMetres.NomeTabela = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA;
                    pessoaFisicaDadosMetres.NomeAtributoChave = TOKEN_DADOSMESTRES.BANCO_ACADEMICO_PESSOA_SEQ;
                    pessoaFisicaDadosMetres.SeqAtributoChaveIntegracao = funcionarioVo.SeqPessoa;
                    if (!string.IsNullOrEmpty(pessoaFisicaDadosMetres.Cpf))
                        pessoaFisicaDadosMetres.Cpf = pessoaFisicaDadosMetres.Cpf.SMCRemoveNonDigits();

                    pessoaFisicaDadosMetres.Filiacao = new List<InserePessoaFisicaFiliacaoData>();

                    foreach (var filiacao in funcionarioVo.Filiacao)
                    {
                        pessoaFisicaDadosMetres.Filiacao.Add(new InserePessoaFisicaFiliacaoData()
                        {
                            TipoParentesco = filiacao.TipoParentesco,
                            NomePessoaParentesco = filiacao.Nome
                        });
                    }

                    IntegracaoDadoMestreService.InserePessoaFisica(pessoaFisicaDadosMetres);
                }

                funcionario.SeqPessoaDadosPessoais = dadosPessoais.Seq;

                this.SaveEntity(funcionario);

                transacao.Commit();
            }

            return funcionario.Seq;
        }

        private Pessoa RecuperarPessoaComDependencias(long seqPessoa)
        {
            Pessoa pessoa;
            var specPessoa = new SMCSeqSpecification<Pessoa>(seqPessoa);
            var includesPessoa = IncludesPessoa.DadosPessoais_ArquivoFoto
                               | IncludesPessoa.Atuacoes;

            pessoa = this.PessoaDomainService.SearchByKey(specPessoa, includesPessoa);
            return pessoa;
        }

        public List<SMCDatasourceItem> ListarFuncionariosVinculoAtivo(string tokenTipoFuncionario)
        {
            var specFuncionariosVinculos = new FuncionarioVinculoFilterSpecification() { TokenTipoFuncionario = tokenTipoFuncionario, VinculoAtivo = true };

            var resultado = this.FuncionarioVinculoDomainService.SearchProjectionBySpecification(specFuncionariosVinculos, a => new SMCDatasourceItem
            {
                Seq = a.SeqFuncionario,
                Descricao = a.Funcionario.DadosPessoais.Nome
            }).ToList();

            return resultado;
        }        
    }
}