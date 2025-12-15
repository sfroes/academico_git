using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.DCT.Enums;
using SMC.Academico.Common.Areas.MAT.Constants;
using SMC.Academico.Common.Areas.SRC.Exceptions;
using SMC.Academico.Common.Constants;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.Models;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.Framework.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.SRC.DomainServices
{
    public class SolicitacaoIntercambioDomainService : AcademicoContextDomain<SolicitacaoIntercambio>
    {
        #region Querys

        #region [ _inserirSolicitacaoIntercambioPorSolicitacaoServico ]

        private string _inserirSolicitacaoIntercambioPorSolicitacaoServico =
                        @" INSERT INTO SRC.solicitacao_intercambio (seq_solicitacao_servico) VALUES ({0})";

        #endregion [ _inserirSolicitacaoIntercambioPorSolicitacaoServico ]

        #endregion Querys

        #region DomainService

        private OrientacaoPessoaAtuacaoDomainService OrientacaoPessoaAtuacaoDomainService => Create<OrientacaoPessoaAtuacaoDomainService>();
        private InstituicaoExternaDomainService InstituicaoExternaDomainService => Create<InstituicaoExternaDomainService>();
        private InstituicaoNivelTipoOrientacaoParticipacaoDomainService InstituicaoNivelTipoOrientacaoParticipacaoDomainService => Create<InstituicaoNivelTipoOrientacaoParticipacaoDomainService>();
        private AlunoDomainService AlunoDomainService => this.Create<AlunoDomainService>();
        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => this.Create<ConfiguracaoEventoLetivoDomainService>();
        private OrientacaoDomainService OrientacaoDomainService => this.Create<OrientacaoDomainService>();
        private PessoaAtuacaoTermoIntercambioDomainService PessoaAtuacaoTermoIntercambioDomainService => this.Create<PessoaAtuacaoTermoIntercambioDomainService>();
        private AlunoHistoricoSituacaoDomainService AlunoHistoricoSituacaoDomainService => this.Create<AlunoHistoricoSituacaoDomainService>();
        private InstituicaoNivelTipoTermoIntercambioDomainService InstituicaoNivelTipoTermoIntercambioDomainService => Create<InstituicaoNivelTipoTermoIntercambioDomainService>();
        private InstituicaoNivelTipoVinculoAlunoDomainService InstituicaoNivelTipoVinculoAlunoDomainService => Create<InstituicaoNivelTipoVinculoAlunoDomainService>();
        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();
        private InstituicaoNivelTipoOrientacaoDomainService InstituicaoNivelTipoOrientacaoDomainService => Create<InstituicaoNivelTipoOrientacaoDomainService>();
        private TermoIntercambioDomainService TermoIntercambioDomainService => Create<TermoIntercambioDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private AlunoHistoricoPrevisaoConclusaoDomainService AlunoHistoricoPrevisaoConclusaoDomainService => Create<AlunoHistoricoPrevisaoConclusaoDomainService>();
        private CicloLetivoDomainService CicloLetivoDomainService => Create<CicloLetivoDomainService>();
        private TrabalhoAcademicoDomainService TrabalhoAcademicoDomainService => Create<TrabalhoAcademicoDomainService>();
        private PeriodoIntercambioDomainService PeriodoIntercambioDomainService => Create<PeriodoIntercambioDomainService>();
        private OrientacaoColaboradorDomainService OrientacaoColaboradorDomainService => Create<OrientacaoColaboradorDomainService>();

        #endregion DomainService

        /// <summary>
        /// Verifica se existe solicitação de dispensa para solicitação de serviço, se não existir cria um solicitação de dispensa apenas com o sequencial da solicitação de serviço
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação de serviço</param>
        public void CriarSolicitacaoIntercambioPorSolicitacaoServico(long seqSolicitacaoServico)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<SolicitacaoIntercambio>(seqSolicitacaoServico));

            if (registro == null)
                ExecuteSqlCommand(string.Format(_inserirSolicitacaoIntercambioPorSolicitacaoServico, seqSolicitacaoServico));
        }

        /// <summary>
        /// Realiza o deferimento de uma solicitação de intercâmbio de acordo com a RN_SRC_069
        /// </summary>
        /// <param name="seqSolicitacaoServico">Solicitação de intercambio a ser deferida</param>
        public void DeferirSolicitacaoIntercambio(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação de intercâmbio
            var solicitacao = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqTermoIntercambio = x.SeqTermoIntercambio,
                SeqTipoTermoIntercambio = x.SeqTipoTermoIntercambio,
                SeqTipoOrientacao = x.SeqTipoOrientacao,
                DataInicio = x.DataInicio,
                DataFim = x.DataFim,
                SeqOrientacao = x.SeqOrientacao,
                Participantes = x.Participantes
            });

            /// Valicação 1. Os campos “Tipo de intercâmbio” e “Termo de Intercambio” da página de token
            /// ATENDIMENTO_INTERCAMBIO passam a ser obrigatórios. Em caso de violação, abortar
            /// operação e exibir a mensagem de erro: "Para deferir esta solicitação, os campos
            /// da página "Intercâmbio" devem ser preenchidos".
            if (!solicitacao.SeqTipoTermoIntercambio.HasValue || !solicitacao.SeqTermoIntercambio.HasValue)
            {
                throw new SolicitacaoIntercambioNaoCadastradaException();
            }

            // Busca os dados de origem do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(solicitacao.SeqPessoaAtuacao);

            // Variáveis auxiliares
            DatasEventoLetivoVO cicloInicio = null;
            DatasEventoLetivoVO cicloFim = null;
            long? seqCicloPosteriorInicio = null;

            // CASO O PERÍODO DO INTERCAMBIO ESTEJA INFORMADO...
            if (solicitacao.DataInicio.HasValue && solicitacao.DataFim.HasValue)
            {
                // Busca o ciclo letivo nas datas do intercambio
                cicloInicio = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(solicitacao.DataInicio.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);
                cicloFim = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(solicitacao.DataFim.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                // Se não encontrou o ciclo na data de inicio, erro.
                if (cicloInicio == null)
                    throw new SolicitacaoIntercambioDataInicioInvalidaException();

                // Busca a situação do aluno na data de inicio
                var situacaoAlunoInicio = AlunoHistoricoSituacaoDomainService.BuscarSituacaoAlunoNaData(solicitacao.SeqPessoaAtuacao, cicloInicio.SeqCicloLetivo, solicitacao.DataInicio.Value);

                /// Validação 2. Uma solicitação de intercâmbio só poderá ser deferida se o aluno estiver matriculado na data de 
                /// início do intercâmbio.
                if (situacaoAlunoInicio == null || (situacaoAlunoInicio != null && situacaoAlunoInicio.TokenTipoSituacaoMatricula != TOKENS_TIPO_SITUACAO_MATRICULA.MATRICULADO))
                {
                    //// Busca a situação do aluno no ciclo de inicio do intercambio
                    //var situacoesCicloAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(solicitacao.SeqPessoaAtuacao, cicloInicio.SeqCicloLetivo);

                    //// Verifica se existe situação MATRICULADO ou PROVAVEL FORMANDO, se não existir, erro.
                    //if (situacoesCicloAluno != null && situacoesCicloAluno.SMCCount() > 0 && !situacoesCicloAluno.Any(s => s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO || s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO))
                    //	throw new SolicitacaoIntercambioDataInicioInvalidaException();

                    throw new SolicitacaoIntercambioDataInicioInvalidaException();
                }

                // Busca o prazo de conclusão do curso do aluno
                var spec = new AlunoHistoricoPrevisaoConclusaoFilterSpecification()
                {
                    SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao,
                    AlunoHistoricoAtual = true
                };
                var previsao = AlunoHistoricoPrevisaoConclusaoDomainService.SearchBySpecification(spec).OrderByDescending(x => x.DataInclusao).FirstOrDefault();
                if (previsao == null)
                    throw new PrazoConclusaoInvalidoException();

                /// Validação 3. Uma solicitação de intercâmbio só poderá ser deferida se a data de previsão de conclusão de curso for
                /// maior, em pelo menos dois dias, que a data de fim do termo de intercambio. Em caso de violação, abortar operação e
                /// exibir a mensagem de erro: "Deferimento não permitido. A partir de <DATA PREVISAO DE CONCLUSÃO> o aluno
                /// estará com prazo de conclusão de curso encerrado."
                if ((previsao.DataPrevisaoConclusao <= solicitacao.DataFim.Value) || (previsao.DataPrevisaoConclusao <= solicitacao.DataFim.Value.AddDays(1)))
                    throw new SolicitacaoIntercambioFinalizaAposPrazoConclusaoException(previsao.DataPrevisaoConclusao);

                // Busca o ciclo letivo da data de deferimento
                var cicloAtual = ConfiguracaoEventoLetivoDomainService.BuscarCicloLetivo(DateTime.Now, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                // Busca o ciclo letivo posterior a data de inicio do intercambio
                seqCicloPosteriorInicio = CicloLetivoDomainService.BuscarProximoCicloLetivo(cicloInicio.SeqCicloLetivo);

                /// Validação 4. Se o ciclo letivo da data de deferimento não for igual ao ciclo letivo da data de inicio do termo
                /// ou ao ciclo imediatamente posterior a esse, abortar a operação e exibir a mensagem de erro: "Deferimento não
                /// permitido. A solicitação não pode ser deferida após 2 ciclos letivos do início do intercâmbio."
                if (cicloAtual.SeqCicloLetivo != cicloInicio.SeqCicloLetivo && seqCicloPosteriorInicio.HasValue && cicloAtual.SeqCicloLetivo != seqCicloPosteriorInicio.Value)
                    throw new SolicitacaoIntercambioDeferimentoTardioException();

                /// Valicação 5. Uma solicitação de intercâmbio só poderá ser deferida se não existir para a pessoa atuação em
                /// questão outro termo de intercâmbio já associado cujo período tenha interseção com o período do termo da
                /// solicitação que está sendo deferida. Em caso de violação, abortar a operação e exibir a mensagem de erro:
                /// "Deferimento não permitido. Já existe um termo de intercâmbio associado ao aluno com período coincidente
                /// ao período do termo desta solicitação".
                var specPAT = new PessoaAtuacaoTermoIntercambioFilterSpecification() { SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao };
                var termosPessoa = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionBySpecification(specPAT, t => new
                {
                    Seq = t.Seq,
                    UltimoPeriodo = t.Periodos.OrderByDescending(p => p.DataInclusao).Select(p => new
                    {
                        DataInicio = p.DataInicio,
                        DataFim = p.DataFim
                    }).FirstOrDefault()
                });
                foreach (var termo in termosPessoa)
                {
                    if (termo.UltimoPeriodo != null)
                    {
                        var datas = new SMCDateOverlap[]
                        {
                            new SMCDateOverlap(solicitacao.DataInicio.Value, solicitacao.DataFim.Value),
                            new SMCDateOverlap(termo.UltimoPeriodo.DataInicio, termo.UltimoPeriodo.DataFim)
                        };
                        if (SMCDateTimeHelper.HasOverlap(true, datas))
                            throw new SolicitacaoIntercambioPeriodoCoincidenteException();
                    }
                }
            }

            // Inicia a transação
            using (var unitOfWork = SMCUnitOfWork.Begin())
            {
                /// 1. Os passos a seguir só devem ser executados se o termo de intercâmbio não está associado ao aluno ainda.
                long? seqPeriodoIntercambio = null;

                // Verifica se o termo de intercambio já está associado ao aluno
                var patSpec = new PessoaAtuacaoTermoIntercambioFilterSpecification()
                {
                    SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao,
                    SeqTermoIntercambio = solicitacao.SeqTermoIntercambio,
                    Ativo = true
                };
                var seqPessoaAtuacaoTermoAssociado = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionByKey(patSpec, x => (long?)x.Seq);

                if (!seqPessoaAtuacaoTermoAssociado.HasValue)
                {
                    long? seqOrientacao = null;

                    /* Deixando esse pedaço com duas opções: recupera a orientação da solicitação antes, ou busca a orientação direto
                    no banco, devido à regra 'RN_SRC_069 - Solicitação - Consistências atendimento intercâmbio' estar 
                    com algumas dificuldades no entendimento. Foi feita essa correção para o BUG 46577 */
                    //long? seqOrientacaoAssociada = solicitacao.SeqOrientacao;
                    long? seqOrientacaoAssociada = null;

                    if (!seqOrientacaoAssociada.HasValue)
                    {
                        var specOrientacoesAssociadas = new OrientacaoFilterSpecification() { SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao, SeqTipoOrientacao = solicitacao.SeqTipoOrientacao, SeqTipoTermoIntercambio = solicitacao.SeqTipoTermoIntercambio };
                        var seqsOrientacoesAssociadas = OrientacaoDomainService.SearchProjectionBySpecification(specOrientacoesAssociadas, x => x.Seq).ToList();

                        if (seqsOrientacoesAssociadas.Any())
                        {
                            seqOrientacaoAssociada = seqsOrientacoesAssociadas.FirstOrDefault();
                        }
                    }

                    /// 1.1 Se a pessoa atuação não possui orientação do tipo parametrizado em Instituição-nível-vínculo-tipo 
                    /// de termo, para o tipo do termo selecionado
                    if (!seqOrientacaoAssociada.HasValue)
                    {
                        /// 1.1.1. Criar uma orientação para o aluno:
                        /// - Tipo de orientação: tipo de orientação selecionado.
                        /// - Tipo vínculo aluno: tipo do vínculo do aluno em questão.
                        /// - Tipo termo intercâmbio: tipo de termo do termo selecionado.
                        /// - Origem material: null.

                        /// 1.1.2. Associar os professores/pesquisadores (colaboradores) à orientação
                        /// criada com seus respectivos tipos de participação, com as datas início e
                        /// fim preenchidas e com a instituição externa informada.

                        /// 1.1.3. Associar a orientação criada à pessoa atuação em questão.
                        if (solicitacao.SeqTipoOrientacao.HasValue)
                        {
                            OrientacaoVO orientacao = new OrientacaoVO()
                            {
                                SeqEntidadeInstituicao = dadosOrigem.SeqInstituicaoEnsino,
                                SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                                SeqTipoOrientacao = solicitacao.SeqTipoOrientacao,
                                SeqTipoTermoIntercambio = solicitacao.SeqTipoTermoIntercambio,
                                SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                                OrientacoesPessoaAtuacao = new List<OrientacaoPessoaAtuacaoVO>(),
                                OrientacoesColaborador = new List<OrientacaoColaboradorVO>()
                            };
                            OrientacaoPessoaAtuacaoVO orientacaoPA = new OrientacaoPessoaAtuacaoVO()
                            {
                                SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao
                            };
                            orientacao.OrientacoesPessoaAtuacao.Add(orientacaoPA);
                            foreach (var participante in solicitacao.Participantes)
                            {
                                OrientacaoColaboradorVO participanteVO = new OrientacaoColaboradorVO()
                                {
                                    SeqColaborador = participante.SeqColaborador,
                                    SeqInstituicaoExterna = participante.SeqInstituicaoExterna,
                                    TipoParticipacaoOrientacao = participante.TipoParticipacaoOrientacao,
                                    DataInicioOrientacao = participante.DataInicio,
                                    DataFimOrientacao = participante.DataFim
                                };
                                orientacao.OrientacoesColaborador.Add(participanteVO);
                            }
                            seqOrientacao = OrientacaoDomainService.SalvarOrientacao(orientacao);
                        }
                    }
                    /// 1.2 Se a pessoa atuação já possui orientação do tipo parametrizado em Instituição-nível-vínculo-tipo
                    /// de termo, para o tipo do termo selecionado
                    else
                    {
                        /// 1.2.1. Verificar se o tipo de termo associado a orientação existente é o mesmo tipo do termo selecionado
                        /// na interface.
                        var specOrientacaoAssociada = new SMCSeqSpecification<Orientacao>(seqOrientacaoAssociada.Value);
                        var orientacaoAssociada = OrientacaoDomainService.SearchByKey(specOrientacaoAssociada);

                        if (orientacaoAssociada.SeqTipoTermoIntercambio.GetValueOrDefault() == solicitacao.SeqTipoTermoIntercambio.GetValueOrDefault())
                        {
                            /// 1.2.1.1. Se for, salvar na tabela orientação, para a orientação que o aluno já possui, o tipo do termo 
                            /// selecionado. Na orientação já existente, atualizar, em orientação colaborador, os demais 
                            /// professores/pesquisadores (colaboradores) com seus respectivos tipos de participação, com as datas 
                            /// início e fim preenchidas/atualizadas e com a instituição externa informada.

                            seqOrientacao = seqOrientacaoAssociada.Value;
                            AtualizarOrientacaoIntercambio(seqOrientacao.Value, solicitacao.SeqTipoTermoIntercambio, solicitacao.Participantes.ToList());
                        }
                        else
                        {
                            if (solicitacao.SeqTipoOrientacao.HasValue)
                            {
                                /// 1.2.1.2. Se não for, criar outra orientação conforme:
                                /// - Tipo de orientação: tipo de orientação selecionado.
                                /// - Tipo vínculo aluno: tipo do vínculo do aluno em questão.
                                /// - Tipo termo intercâmbio: tipo de termo do termo selecionado.
                                /// - Origem material: null.
                                OrientacaoVO orientacao = new OrientacaoVO()
                                {
                                    SeqEntidadeInstituicao = dadosOrigem.SeqInstituicaoEnsino,
                                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                                    SeqTipoOrientacao = solicitacao.SeqTipoOrientacao,
                                    SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                                    SeqTipoTermoIntercambio = solicitacao.SeqTipoTermoIntercambio,
                                    OrientacoesPessoaAtuacao = new List<OrientacaoPessoaAtuacaoVO>(),
                                    OrientacoesColaborador = new List<OrientacaoColaboradorVO>()
                                };

                                /// 1.2.1.2.1. Associar os professores/pesquisadores (colaboradores) à orientação criada com seus 
                                /// respectivos tipos de participação, com as datas início e fim preenchidas e com a instituição externa 
                                /// informada.
                                foreach (var participante in solicitacao.Participantes)
                                {
                                    OrientacaoColaboradorVO participanteVO = new OrientacaoColaboradorVO()
                                    {
                                        SeqColaborador = participante.SeqColaborador,
                                        SeqInstituicaoExterna = participante.SeqInstituicaoExterna,
                                        TipoParticipacaoOrientacao = participante.TipoParticipacaoOrientacao,
                                        DataInicioOrientacao = participante.DataInicio,
                                        DataFimOrientacao = participante.DataFim
                                    };
                                    orientacao.OrientacoesColaborador.Add(participanteVO);
                                }

                                /// 1.2.1.2.2. Associar a orientação criada à pessoa atuação em questão.
                                OrientacaoPessoaAtuacaoVO orientacaoPA = new OrientacaoPessoaAtuacaoVO()
                                {
                                    SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao
                                };
                                orientacao.OrientacoesPessoaAtuacao.Add(orientacaoPA);

                                seqOrientacao = OrientacaoDomainService.SalvarOrientacao(orientacao);
                            }
                        }
                    }

                    /// 1.3. Associar o termo de intercâmbio à pessoa atuação com a orientação criada / atualizada no item 1,
                    /// com o termo de intercâmbio e período informados, com o tipo de mobilidade igual a
                    /// “Saída para outra instituição” e o ind_ativo igual a 1 e o sequencial da solicitação de 
                    /// intercâmbio em questão.
                    PessoaAtuacaoTermoIntercambio paTermo = new PessoaAtuacaoTermoIntercambio()
                    {
                        SeqPessoaAtuacao = solicitacao.SeqPessoaAtuacao,
                        SeqTermoIntercambio = solicitacao.SeqTermoIntercambio.Value,
                        SeqOrientacao = seqOrientacao,
                        Ativo = true,
                        TipoMobilidade = TipoMobilidade.SaidaParaOutraInstituicao,
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        Periodos = new List<PeriodoIntercambio>()
                    };

                    // Associa o termo a pessoa
                    PessoaAtuacaoTermoIntercambioDomainService.SaveEntity(paTermo);

                    // Se informou o período...
                    if (solicitacao.DataInicio.HasValue && solicitacao.DataFim.HasValue)
                    {
                        PeriodoIntercambio periodo = new PeriodoIntercambio()
                        {
                            SeqPessoaAtuacaoTermoIntercambio = paTermo.Seq,
                            DataInicio = solicitacao.DataInicio.Value,
                            DataFim = solicitacao.DataFim.Value
                        };

                        PeriodoIntercambioDomainService.SaveEntity(periodo);
                        seqPeriodoIntercambio = periodo.Seq;
                    }

                    // Guarda o sequencial do pessoa atuação x termo criado
                    seqPessoaAtuacaoTermoAssociado = paTermo.Seq;
                }
                else
                {
                    if (!solicitacao.DataInicio.HasValue || !solicitacao.DataFim.HasValue)
                        throw new SolicitacaoIntercambioPeriodoNaoInformadoException();

                    /// 2. Se o termo e a orientação já estão associados ao aluno, ou seja, o tipo de intercâmbio selecionado está
                    /// parametrizado por instituição-nível-vínculo-tipo de termo para conceder formação, somente as datas início e
                    /// fim do período de intercâmbio devem ser salvas para a pessoa-atuação-termo-intercâmbio em questão.
                    PeriodoIntercambio novoPeriodo = new PeriodoIntercambio()
                    {
                        SeqPessoaAtuacaoTermoIntercambio = seqPessoaAtuacaoTermoAssociado.Value,
                        DataInicio = solicitacao.DataInicio.Value,
                        DataFim = solicitacao.DataFim.Value
                    };

                    PeriodoIntercambioDomainService.SaveEntity(novoPeriodo);
                    seqPeriodoIntercambio = novoPeriodo.Seq;

                    // Atualiza os dados da orientação
                    if (solicitacao.SeqOrientacao.HasValue)
                    {
                        AtualizarOrientacaoIntercambio(solicitacao.SeqOrientacao.Value, solicitacao.SeqTipoTermoIntercambio, solicitacao.Participantes.ToList());
                    }
                }

                /// 3. Salvar  no campo "Descrição da solicitação atualizada" em formato HTML, os campos devem ser
                /// concatenados com os respectivos valores preenchidos em UC_SRC_004_13_01 - Atendimento de Intercâmbio
                AtualizarDescricao(seqSolicitacaoServico, false);

                // Se foi informado um período de viagem...
                if (solicitacao.DataInicio.HasValue && solicitacao.DataFim.HasValue)
                {
                    /// 4. CASO O PERÍODO DO INTERCAMBIO ESTEJA INFORMADO, alterar a situação de matrícula do aluno

                    /// 4.1. Incluir em Aluno Histórico Situação, para o aluno histórico ciclo letivo (referente ao
                    /// ciclo letivo onde começa o intercâmbio), a situação MATRICULADO_MOBILIDADE com data início
                    /// igual à data de início do período de intercâmbio, o sequencial da solicitação de serviço e o sequencial
                    /// do período de intercâmbio. Caso exista outra situação de matrícula na mesma data, setar com o horário 
                    /// +1 minuto.
                    var seqHistoricoCicloInicio = AlunoDomainService.SearchProjectionByKey(solicitacao.SeqPessoaAtuacao, x => x.Historicos.Where(h => !h.DataExclusao.HasValue && h.Atual).Select(h => new
                    {
                        SeqAlunoHistoricoCicloLetivo = (long?)h.HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == cicloInicio.SeqCicloLetivo).Seq
                    }).FirstOrDefault());

                    // Busca a referencia do AlunoHistoricoCicloLetivo
                    var seqAlunoHistoricoCicloLetivoInicio = seqHistoricoCicloInicio.SeqAlunoHistoricoCicloLetivo;

                    // Se não conseguiu recuperar...
                    if (!seqAlunoHistoricoCicloLetivoInicio.HasValue)
                        throw new AlunoHistoricoCicloLetivoInvalidoException();

                    DateTime dataInicioSituacaoCicloInicio = solicitacao.DataInicio.Value;
                    var specAlunoHistoricoSituacaoCicloInicio = new AlunoHistoricoSituacaoFilterSpecification() { SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivoInicio };
                    var alunoHistoricosSituacaoCicloInicio = AlunoHistoricoSituacaoDomainService.SearchBySpecification(specAlunoHistoricoSituacaoCicloInicio).ToList();
                    var alunoHistoricosSituacaoCicloInicioMesmaData = alunoHistoricosSituacaoCicloInicio.Where(a => a.DataInicioSituacao.Date == dataInicioSituacaoCicloInicio.Date).ToList();

                    if (alunoHistoricosSituacaoCicloInicioMesmaData.Any())
                        dataInicioSituacaoCicloInicio = alunoHistoricosSituacaoCicloInicioMesmaData.Max(a => a.DataInicioSituacao).AddMinutes(1);

                    var inicioMobilidade = new IncluirAlunoHistoricoSituacaoVO()
                    {
                        SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivoInicio,
                        TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE,
                        DataInicioSituacao = dataInicioSituacaoCicloInicio,
                        SeqSolicitacaoServico = seqSolicitacaoServico,
                        SeqPeriodoIntercambio = seqPeriodoIntercambio
                    };
                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(inicioMobilidade);

                    /// 4.2 Se a data fim do termo de intercâmbio estiver dentro do mesmo ciclo onde começa o intercâmbio,
                    /// incluir no mesmo Aluno Histórico Situação, a situação MATRICULADO ou PROVAVEL_FORMANDO com data
                    /// início igual à data fim do período de intercâmbio mais um dia, o sequencial da solicitação de
                    /// serviço e o sequencial do período do intercâmbio.
                    if (cicloFim != null && cicloInicio.SeqCicloLetivo == cicloFim.SeqCicloLetivo)
                    {
                        DateTime dataInicioSituacaoCicloFim = solicitacao.DataFim.Value.AddDays(1);

                        // Define a nova situação
                        var tokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                        var trabalho = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(solicitacao.SeqPessoaAtuacao);
                        if (trabalho.DataDeposito.HasValue && trabalho.DataDeposito.Value.Date <= dataInicioSituacaoCicloFim.Date)
                        {
                            tokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO;
                        }

                        var seqHistoricoCicloFim = AlunoDomainService.SearchProjectionByKey(solicitacao.SeqPessoaAtuacao, x => x.Historicos.Where(h => !h.DataExclusao.HasValue && h.Atual).Select(h => new
                        {
                            SeqAlunoHistoricoCicloLetivo = (long?)h.HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == cicloFim.SeqCicloLetivo).Seq
                        }).FirstOrDefault());

                        // Busca a referencia do AlunoHistoricoCicloLetivo
                        var seqAlunoHistoricoCicloLetivoFim = seqHistoricoCicloFim.SeqAlunoHistoricoCicloLetivo;

                        // Se não conseguiu recuperar...
                        if (!seqAlunoHistoricoCicloLetivoFim.HasValue)
                            throw new AlunoHistoricoCicloLetivoInvalidoException();

                        var retornoMobilidade = new IncluirAlunoHistoricoSituacaoVO()
                        {
                            SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivoFim,
                            TokenSituacao = tokenSituacao,
                            DataInicioSituacao = dataInicioSituacaoCicloFim,
                            SeqSolicitacaoServico = seqSolicitacaoServico,
                            SeqPeriodoIntercambio = seqPeriodoIntercambio
                        };
                        AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(retornoMobilidade);
                    }
                    /// Se o aluno possui situação MATRICULADO ou PROVAVEL_FORMANDO no ciclo letivo posterior ao do ciclo
                    /// de inicio do intercâmbio e a data fim do termo de intercâmbio for maior ou igual a data início do ciclo
                    /// letivo posterior ao do ciclo de inicio do intercâmbio
                    else if (seqCicloPosteriorInicio.HasValue)
                    {
                        // Busca o período do ciclo letivo
                        var cicloPosterior = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloPosteriorInicio.Value, dadosOrigem.SeqCursoOfertaLocalidadeTurno, null, TOKEN_TIPO_EVENTO.PERIODO_CICLO_LETIVO);

                        // Se a data fim do termo de intercâmbio for maior ou igual a data início do ciclo letivo posterior
                        if (solicitacao.DataFim >= cicloPosterior.DataInicio)
                        {
                            // Busca a situação do aluno no ciclo posterior ao do inicio do intercambio
                            var situacoesCicloAluno = AlunoHistoricoSituacaoDomainService.BuscarSituacoesAlunoNoCicloLetivo(solicitacao.SeqPessoaAtuacao, seqCicloPosteriorInicio.Value);

                            // Verifica se existe situação MATRICULADO ou PROVAVEL FORMANDO
                            if (situacoesCicloAluno != null && situacoesCicloAluno.SMCCount() > 0 && situacoesCicloAluno.Any(s => s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO || s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO))
                            {
                                /// 4.3 Excluir logicamente a situação MATRICULADO ou PROVAVEL em Aluno Histórico Situação, para o aluno
                                /// histórico ciclo letivo posterior ao do inicio do intercambio com os dados:
                                /// - Data de exclusão: data do deferimento.
                                /// - Usuário de exclusão: usuário responsável pelo atendimento.
                                /// - Sequencial solicitação serviço exclusão: sequencial da solicitação de intercâmbio em questão.
                                /// - Observação exclusão: Exclusão devido ao atendimento de solicitação de intercâmbio.
                                foreach (var situacao in situacoesCicloAluno.Where(s => s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.MATRICULADO || s.TokenSituacaoMatricula == TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO))
                                {
                                    string obsExclusao = "Exclusão devido ao atendimento de solicitação de intercâmbio.";
                                    AlunoHistoricoSituacaoDomainService.ExcluirAlunoHistoricoSituacao(situacao.SeqAlunoHistoricoSituacao.GetValueOrDefault(), obsExclusao, seqSolicitacaoServico);
                                }

                                /// 4.4 Incluir em Aluno Histórico Situação, para o aluno histórico ciclo letivo posterior
                                /// ao do inicio do intercambio, a situação MATRICULADO_MOBILIDADE com data início igual
                                /// à data de início do ciclo letivo, o sequencial da solicitação de serviço e o sequencial do 
                                /// período de intercâmbio. Caso exista outra situação de matrícula na mesma data, setar com o 
                                /// horário +1 minuto.
                                var seqHistoricoCicloPosteriorInicio = AlunoDomainService.SearchProjectionByKey(solicitacao.SeqPessoaAtuacao, x => x.Historicos.Where(h => !h.DataExclusao.HasValue && h.Atual).Select(h => new
                                {
                                    SeqAlunoHistoricoCicloLetivo = (long?)h.HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == seqCicloPosteriorInicio.Value).Seq
                                }).FirstOrDefault());

                                // Busca a referencia do AlunoHistoricoCicloLetivo
                                var seqAlunoHistoricoCicloLetivoPosteriorInicio = seqHistoricoCicloPosteriorInicio.SeqAlunoHistoricoCicloLetivo;

                                // Se não conseguiu recuperar...
                                if (!seqAlunoHistoricoCicloLetivoPosteriorInicio.HasValue)
                                    throw new AlunoHistoricoCicloLetivoInvalidoException();

                                DateTime dataInicioSituacaoCicloPosteriorInicio = cicloPosterior.DataInicio;
                                var specAlunoHistoricoCicloLetivoPosteriorInicio = new AlunoHistoricoSituacaoFilterSpecification() { SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivoPosteriorInicio };
                                var alunoHistoricoCicloLetivoPosteriorInicio = AlunoHistoricoSituacaoDomainService.SearchBySpecification(specAlunoHistoricoCicloLetivoPosteriorInicio).ToList();
                                var alunoHistoricoCicloLetivoPosteriorInicioMesmaData = alunoHistoricoCicloLetivoPosteriorInicio.Where(a => a.DataInicioSituacao.Date == dataInicioSituacaoCicloPosteriorInicio.Date).ToList();

                                if (alunoHistoricoCicloLetivoPosteriorInicioMesmaData.Any())
                                    dataInicioSituacaoCicloPosteriorInicio = alunoHistoricoCicloLetivoPosteriorInicioMesmaData.Max(a => a.DataInicioSituacao).AddMinutes(1);

                                var inicioMobilidadeCicloPosterior = new IncluirAlunoHistoricoSituacaoVO()
                                {
                                    SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivoPosteriorInicio,
                                    TokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO_MOBILIDADE,
                                    DataInicioSituacao = dataInicioSituacaoCicloPosteriorInicio,
                                    SeqSolicitacaoServico = seqSolicitacaoServico,
                                    SeqPeriodoIntercambio = seqPeriodoIntercambio
                                };
                                AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(inicioMobilidadeCicloPosterior);

                                /// 4.5 Se a data fim do termo de intercâmbio estiver dentro do ciclo posterior ao inicio
                                /// do intercambio, incluir no Aluno Histórico Situação, a situação MATRICULADO ou PROVAVEL FORMANDO
                                /// com data início igual à data fim do período de intercâmbio mais um dia, o sequencial
                                /// da solicitação de serviço e o sequencial do período de intercâmbio.
                                if (cicloFim != null && cicloFim.SeqCicloLetivo == seqCicloPosteriorInicio.Value)
                                {
                                    DateTime dataInicioSituacaoCiclofim = solicitacao.DataFim.Value.AddDays(1);

                                    // Define a nova situação
                                    var tokenSituacao = TOKENS_SITUACAO_MATRICULA.MATRICULADO;
                                    var trabalho = TrabalhoAcademicoDomainService.BuscarDatasDepositoDefesaTrabalho(solicitacao.SeqPessoaAtuacao);
                                    if (trabalho.DataDeposito.HasValue && trabalho.DataDeposito.Value.Date <= dataInicioSituacaoCiclofim.Date)
                                    {
                                        tokenSituacao = TOKENS_SITUACAO_MATRICULA.PROVAVEL_FORMANDO;
                                    }

                                    var seqHistoricoCicloFim = AlunoDomainService.SearchProjectionByKey(solicitacao.SeqPessoaAtuacao, x => x.Historicos.Where(h => !h.DataExclusao.HasValue && h.Atual).Select(h => new
                                    {
                                        SeqAlunoHistoricoCicloLetivo = (long?)h.HistoricosCicloLetivo.FirstOrDefault(hc => hc.SeqCicloLetivo == cicloFim.SeqCicloLetivo).Seq
                                    }).FirstOrDefault());

                                    // Busca a referencia do AlunoHistoricoCicloLetivo
                                    var seqAlunoHistoricoCicloLetivoFim = seqHistoricoCicloFim.SeqAlunoHistoricoCicloLetivo;

                                    // Se não conseguiu recuperar...
                                    if (!seqAlunoHistoricoCicloLetivoFim.HasValue)
                                        throw new AlunoHistoricoCicloLetivoInvalidoException();

                                    var retornoMobilidade = new IncluirAlunoHistoricoSituacaoVO()
                                    {
                                        SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivoFim,
                                        TokenSituacao = tokenSituacao,
                                        DataInicioSituacao = dataInicioSituacaoCiclofim,
                                        SeqSolicitacaoServico = seqSolicitacaoServico,
                                        SeqPeriodoIntercambio = seqPeriodoIntercambio
                                    };
                                    AlunoHistoricoSituacaoDomainService.IncluirAlunoHistoricoSituacao(retornoMobilidade);
                                }
                            }
                        }
                    }
                }

                // Finaliza a transação
                unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Atualizar a orientação de um intercâmbio.
        /// </summary>
        /// <param name="seqOrientacao">Sequencial da orientação a ser atualizada</param>
        /// <param name="seqTipoTermoIntercambio">Tipo de termo de intercâmbio a ser usado na orientação</param>
        /// <param name="participantesSolicitacao">Lista de participantes da solicitação para atualização  na orientação</param>
        private void AtualizarOrientacaoIntercambio(long seqOrientacao, long? seqTipoTermoIntercambio, List<SolicitacaoIntercambioParticipante> participantesSolicitacao)
        {
            // Busca os dados da orientação
            var specOrientacao = new OrientacaoFilterSpecification() { Seq = seqOrientacao };
            var orientacao = OrientacaoDomainService.SearchProjectionByKey(specOrientacao, o => new
            {
                SeqTipoTermoIntercambio = o.SeqTipoTermoIntercambio,
                Colaboradores = o.OrientacoesColaborador
            });

            /// 1.2.1.1. (...) salvar na tabela orientação, para a orientação que o aluno já possui, o tipo do termo selecionado
            var orientacaoUpdate = new Orientacao()
            {
                Seq = seqOrientacao,
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio
            };
            OrientacaoDomainService.UpdateFields(orientacaoUpdate, x => x.SeqTipoTermoIntercambio);

            /// 1.2.1.1 Na orientação já existente, atualizar, em orientação colaborador, os demais professores/pesquisadores 
            /// (colaboradores) com seus respectivos tipos de participação, com as datas início e fim preenchidas/atualizadas e 
            /// com a instituição externa informada
            foreach (var participante in participantesSolicitacao)
            {
                // Caso seja um novo registro, inclui
                if (!participante.SeqOrientacaoColaborador.HasValue)
                {
                    var orientacaoColaborador = new OrientacaoColaborador()
                    {
                        SeqOrientacao = seqOrientacao,
                        SeqColaborador = participante.SeqColaborador,
                        SeqInstituicaoExterna = participante.SeqInstituicaoExterna,
                        TipoParticipacaoOrientacao = participante.TipoParticipacaoOrientacao,
                        DataInicioOrientacao = participante.DataInicio,
                        DataFimOrientacao = participante.DataFim
                    };
                    OrientacaoColaboradorDomainService.SaveEntity(orientacaoColaborador);
                }
                // Se é um orientador que já participa da orientação, atualiza
                else
                {
                    var orientacaoColaborador = orientacao.Colaboradores.Where(o => o.Seq == participante.SeqOrientacaoColaborador).FirstOrDefault();
                    if (orientacaoColaborador != null &&
                        (orientacaoColaborador.SeqColaborador != participante.SeqColaborador ||
                         orientacaoColaborador.SeqInstituicaoExterna != participante.SeqInstituicaoExterna ||
                         orientacaoColaborador.TipoParticipacaoOrientacao != participante.TipoParticipacaoOrientacao ||
                         orientacaoColaborador.DataInicioOrientacao != participante.DataInicio ||
                         orientacaoColaborador.DataFimOrientacao != participante.DataFim))
                    {
                        orientacaoColaborador.SeqColaborador = participante.SeqColaborador;
                        orientacaoColaborador.SeqInstituicaoExterna = participante.SeqInstituicaoExterna;
                        orientacaoColaborador.TipoParticipacaoOrientacao = participante.TipoParticipacaoOrientacao;
                        orientacaoColaborador.DataInicioOrientacao = participante.DataInicio;
                        orientacaoColaborador.DataFimOrientacao = participante.DataFim;
                        OrientacaoColaboradorDomainService.SaveEntity(orientacaoColaborador);
                    }
                }
            }

            // Se excluiu algum registro
            var seqs = participantesSolicitacao.Where(p => p.SeqOrientacaoColaborador.HasValue).Select(p => p.SeqOrientacaoColaborador);
            foreach (var orientacaoColaborador in orientacao.Colaboradores.Where(o => !seqs.Contains(o.Seq)))
            {
                OrientacaoColaboradorDomainService.DeleteEntity(orientacaoColaborador);
            }
        }

        /// <summary>
        /// Busca os dados iniciais para um atendimento de intercâmbio
        /// </summary>
        public AtendimentoIntercambioVO BuscarDadosIniciaisAtendimentoIntercambio(long seqSolicitacaoServico)
        {
            // Busca os dados da solicitação
            var dados = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
                Cpf = x.PessoaAtuacao.Pessoa.Cpf,
                NumeroPassaporte = x.PessoaAtuacao.Pessoa.NumeroPassaporte,
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqTipoTermoIntercambio = x.SeqTipoTermoIntercambio
            });

            // Cria o retorno
            AtendimentoIntercambioVO ret = null;

            // Carrega o retorno com os dados da solicitação
            ret = SMCMapperHelper.Create<AtendimentoIntercambioVO>(dados);

            // Recupera os dados de origem do aluno
            var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(dados.SeqPessoaAtuacao);

            // Preenche para retorno o nivel e tipo de vinculo
            ret.SeqNivelEnsino = dadosOrigem.SeqNivelEnsino;
            ret.SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno;

            // Busca os dados do InstituicaoNivelTipoVinculoAluno
            var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(dados.SeqPessoaAtuacao);

            // Cria o datasource de tipos de termo para parametrização da instituição nível tipo vínculo aluno
            ret.TiposTermoIntercambio = dadosInstituicaoNivelAluno.TiposTermoIntercambio.Select(t => new SMCDatasourceItem
            {
                Seq = t.SeqTipoTermoIntercambio,
                Descricao = t.TipoTermoIntercambio.Descricao,
                DataAttributes = new List<SMCKeyValuePair>
                {
                    new SMCKeyValuePair { Key = "concedeFormacao", Value = t.ConcedeFormacao.ToString() }
                }
            }).ToList();

            if (!string.IsNullOrEmpty(ret.Cpf) && !string.IsNullOrEmpty(ret.NumeroPassaporte))
            {
                /* Validação para deixar só o CPF ou NumeroPassaporte e não as duas opções para enviar de parâmetro para o TermoIntercambioLookup
                Ao criar um termo, pode associar só uma das informações, então mesmo que o aluno tenha as duas informações preenchidas,
                ao buscar o termo enviando no specification os dois campos não retorna nada, pois vai tentar achar um registro
                com aquele CPF e aquele NumeroPassaporte, sendo que o registro terá uma das informações. 
                No TermoIntercambioLookupAttribute tem várias validações extensas, a query tem vários unions, e como esse lookup é usado
                em vários lugares, a maneira mais fácil de retornar os termos para alunos que tem CPF e NumeroPassaporte é verificando quais
                dessas informações tem nos termos desse aluno, e enviar de parâmetro um desses campos. 
                Essa situação foi descoberta no BUG 46577. */

                var specTermoIntercambio = new TermoIntercambioFilterSpecification()
                {
                    Cpf = ret.Cpf,
                    SeqNivelEnsino = ret.SeqNivelEnsino,
                    SeqTipoTermoIntercambio = ret.SeqTipoTermoIntercambio,
                    TipoMobilidade = TipoMobilidade.SaidaParaOutraInstituicao
                };

                var specVinculo = new InstituicaoNivelTipoVinculoAlunoFilterSpecification()
                {
                    SeqNivelEnsino = ret.SeqNivelEnsino,
                    SeqTipoVinculoAluno = ret.SeqTipoVinculoAluno
                };

                specTermoIntercambio.SeqsTiposTermoIntercambio = InstituicaoNivelTipoVinculoAlunoDomainService
                    .SearchProjectionByKey(specVinculo, p => p.TiposTermoIntercambio.Select(s => s.SeqTipoTermoIntercambio))
                    ?.ToArray();

                var termosComCPF = TermoIntercambioDomainService.SearchBySpecification(specTermoIntercambio).ToList();

                if (termosComCPF.Any())
                {
                    ret.NumeroPassaporte = null;
                }
                else
                {
                    specTermoIntercambio.Cpf = null;
                    specTermoIntercambio.NumeroPassaporte = ret.NumeroPassaporte;
                    var termosComNumeroPassaporte = TermoIntercambioDomainService.SearchBySpecification(specTermoIntercambio).ToList();

                    if (termosComNumeroPassaporte.Any())
                    {
                        ret.Cpf = null;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Busca os dados do atendimento de intercâmbio após selecionad o tipo de termo.
        /// </summary>
        public AtendimentoIntercambioVO BuscarDadosTermoAtendimentoIntercambio(long seqSolicitacaoServico, long seqTipoTermoIntercambio)
        {
            // Busca os dados da solicitação
            var dados = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqTipoOrientacao = x.SeqTipoOrientacao,
                SeqTermoIntercambio = x.SeqTermoIntercambio,
                SeqTipoTermoIntercambioSelecionado = (long?)x.TermoIntercambio.ParceriaIntercambioTipoTermo.SeqTipoTermoIntercambio,
                DataInicio = x.DataInicio,
                DataFim = x.DataFim,
                SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
            });

            // Cria o retorno
            var ret = SMCMapperHelper.Create<AtendimentoIntercambioVO>(dados);

            // Busca os dados do InstituicaoNivelTipoVinculoAluno
            var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(dados.SeqPessoaAtuacao);

            // Caso já tenha selecionado um termo para essa solicitação, verifica se tem o mesmo tipo do tipo que acabei de selecionar
            if (ret.SeqTermoIntercambio.HasValue)
            {
                // Caso o tipo de termo não seja o mesmo do selecionado, limpa o termo
                if (seqTipoTermoIntercambio != dados.SeqTipoTermoIntercambioSelecionado)
                    ret.SeqTermoIntercambio = null;
            }

            // Verifica se o tipo selecionado é de cotutela
            bool solicitacaoCotutela = dadosInstituicaoNivelAluno.TiposTermoIntercambio.FirstOrDefault(t => t.SeqTipoTermoIntercambio == seqTipoTermoIntercambio).ConcedeFormacao;

            // Caso seja uma solicitação de cotutela
            if (solicitacaoCotutela)
            {
                // Busca os dados do termo que já foi associado
                var dadosTermoAssociado = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionByKey(new PessoaAtuacaoTermoIntercambioFilterSpecification
                {
                    SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                    Ativo = true,
                    SeqTipoTermoIntercambio = seqTipoTermoIntercambio
                }, x => new
                {
                    SeqTermoIntercambio = (long?)x.SeqTermoIntercambio,
                    SeqTipoOrientacao = (long?)x.Orientacao.SeqTipoOrientacao,
                });

                if (dadosTermoAssociado != null)
                {
                    // Caso não tenha selecionado um termo, busca o termo associado ao aluno
                    if (!ret.SeqTermoIntercambio.HasValue)
                    {
                        ret.SeqTermoIntercambio = dadosTermoAssociado.SeqTermoIntercambio;
                        ret.SeqTipoOrientacao = dadosTermoAssociado.SeqTipoOrientacao;
                        ret.CotutelaJaAssociada = true;
                    }
                    else if (ret.SeqTermoIntercambio == dadosTermoAssociado.SeqTermoIntercambio)
                        ret.CotutelaJaAssociada = true;
                }
            }

            // Carrega parametrização do Instituição Nivel
            var dadosInstituicaoNivelTipoTermo = InstituicaoNivelTipoTermoIntercambioDomainService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFilterSpecification
            {
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                SeqInstituicaoNivelTipoVinculoAluno = dadosInstituicaoNivelAluno.Seq
            }).FirstOrDefault();

            // Caso tenha termo selecionado, carrega o datasource de tipos de orientações e outras informações
            if (ret.SeqTermoIntercambio.HasValue)
            {
                // Busca os dados adicionais do termo
                var dadosTermo = TermoIntercambioDomainService.BuscarDadosTermoIntercambio(ret.SeqTermoIntercambio.Value);
                ret.DescricaoInstituicaoExterna = dadosTermo.DescricaoInstituicaoExterna;
                ret.DescricaoTipoTermo = dadosTermo.DescricaoTipoTermo;

                // Retorna se exige período ou não
                ret.ExigePeriodo = dadosInstituicaoNivelTipoTermo.ExigePeriodoIntercambioTermo;

                ret.OrientacaoAluno = CadastroOrientacao.Nenhum;
                if (ret.TiposOrientacoes?.FirstOrDefault()?.DataAttributes.Any(d => d.Key == "orientacao-aluno") ?? false)
                    ret.OrientacaoAluno = SMCEnumHelper.GetEnum<CadastroOrientacao>(ret.TiposOrientacoes.FirstOrDefault().DataAttributes.FirstOrDefault(d => d.Key == "orientacao-aluno").Value);

                // Carrega o datasource de tipos de orientação
                ret.TiposOrientacoes = InstituicaoNivelTipoOrientacaoDomainService.BuscarTiposOrientacaoSelect(new InstituicaoNivelTipoOrientacaoFiltroVO
                {
                    SeqInstituicaoNivelTipoTermoIntercambio = dadosInstituicaoNivelTipoTermo.Seq,
                    CadastroOrientacoesAluno = new CadastroOrientacao[] { CadastroOrientacao.Exige, CadastroOrientacao.Permite },
                });

                ret.ExisteTipoOrientacaoParametrizado = ret.TiposOrientacoes.SMCAny();
            }

            return ret;
        }

        /// <summary>
        /// Busca os dados de orientação de uma solicitação de intercâmbio
        /// </summary>
        public AtendimentoIntercambioVO BuscarDadosOrientacaoTermoAtendimentoIntercambio(long seqSolicitacaoServico, long seqTipoOrientacao, long seqTipoTermoIntercambio, long seqTermoIntercambio)
        {
            // Busca os dados da solicitação
            var dados = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
            {
                SeqPessoaAtuacao = x.SeqPessoaAtuacao,
                SeqOrientacao = x.SeqOrientacao,
                SeqTermoIntercambio = x.SeqTermoIntercambio,
                SeqTipoTermoIntercambio = x.SeqTipoTermoIntercambio,
                Participantes = x.Participantes.Select(p => new SolicitacaoIntercambioParticipanteVO
                {
                    SeqColaborador = p.SeqColaborador,
                    SeqInstituicaoExterna = p.SeqInstituicaoExterna,
                    TipoParticipacaoOrientacao = p.TipoParticipacaoOrientacao,
                    DataInicio = p.DataInicio,
                    DataFim = p.DataFim,
                    SeqOrientacaoColaborador = p.SeqOrientacaoColaborador
                }).ToList(),
                SeqInstituicaoEnsino = x.PessoaAtuacao.Pessoa.SeqInstituicaoEnsino,
            });

            // Cria o retorno
            var ret = SMCMapperHelper.Create<AtendimentoIntercambioVO>(dados);

            // Caso já tenha selecionado um termo para essa solicitação, verifica se tem o mesmo tipo do tipo que acabei de selecionar
            if (dados.SeqTermoIntercambio.HasValue)
            {
                // Caso o tipo de termo não seja o mesmo do selecionado, limpa a lista de participantes
                if (seqTipoTermoIntercambio != dados.SeqTipoTermoIntercambio)
                    ret.Participantes = new List<SolicitacaoIntercambioParticipanteVO>();
            }

            // Busca os dados do InstituicaoNivelTipoVinculoAluno
            var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(dados.SeqPessoaAtuacao);

            // Verifica se na solicitação já possui participantes.. caso não tenha, verifica se já tem algum termo com orientação ou se tem orientação do tipo informado
            if (!ret.Participantes.SMCAny())
            {
                // Verifica se o tipo selecionado é de cotutela
                bool solicitacaoCotutela = dadosInstituicaoNivelAluno.TiposTermoIntercambio.FirstOrDefault(t => t.SeqTipoTermoIntercambio == seqTipoTermoIntercambio).ConcedeFormacao;

                // Caso seja uma solicitação de cotutela
                if (solicitacaoCotutela)
                {
                    var dadosTermoAssociado = PessoaAtuacaoTermoIntercambioDomainService.SearchProjectionByKey(new PessoaAtuacaoTermoIntercambioFilterSpecification
                    {
                        SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                        Ativo = true,
                        SeqTipoTermoIntercambio = seqTipoTermoIntercambio
                    }, x => new
                    {
                        SeqOrientacao = x.SeqOrientacao,
                        Participantes = x.Orientacao.OrientacoesColaborador.Select(o => new SolicitacaoIntercambioParticipanteVO
                        {
                            SeqColaborador = o.SeqColaborador,
                            SeqInstituicaoExterna = o.SeqInstituicaoExterna,
                            TipoParticipacaoOrientacao = o.TipoParticipacaoOrientacao,
                            SeqOrientacaoColaborador = o.Seq,
                            DataInicio = o.DataInicioOrientacao,
                            DataFim = o.DataFimOrientacao
                        }).ToList(),
                    });

                    if (dadosTermoAssociado?.SeqOrientacao != null)
                    {
                        // OK.. achou dados do termo atual. utiliza.
                        ret.Participantes = dadosTermoAssociado.Participantes;
                        ret.SeqOrientacao = dadosTermoAssociado.SeqOrientacao;
                    }
                    else
                    {
                        // Não achou... verifica se tem orientação do tipo selecionado
                        var dadosOrientacao = OrientacaoPessoaAtuacaoDomainService.SearchProjectionByKey(new OrientacaoPessoaAtuacaoFilterSpecification
                        {
                            SeqPessoaAtuacao = dados.SeqPessoaAtuacao,
                            SeqsTipoOrientacao = new long[] { seqTipoOrientacao }
                        }, x => new
                        {
                            SeqOrientacao = x.SeqOrientacao,
                            SeqOrientacaoPessoaAtuacao = x.Seq,
                            Participantes = x.Orientacao.OrientacoesColaborador.Select(o => new SolicitacaoIntercambioParticipanteVO
                            {
                                SeqColaborador = o.SeqColaborador,
                                SeqInstituicaoExterna = o.SeqInstituicaoExterna,
                                TipoParticipacaoOrientacao = o.TipoParticipacaoOrientacao,
                                SeqOrientacaoColaborador = o.Seq,
                                DataInicio = o.DataInicioOrientacao,
                                DataFim = o.DataFimOrientacao
                            }).ToList(),
                        });

                        if (dadosOrientacao != null)
                        {
                            ret.SeqOrientacao = dadosOrientacao.SeqOrientacao;
                            ret.Participantes = dadosOrientacao.Participantes;
                        }
                    }
                }
            }

            // Verifica se exige ou não período
            var dadosInstituicaoNivelTipoTermo = InstituicaoNivelTipoTermoIntercambioDomainService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFilterSpecification
            {
                SeqTipoTermoIntercambio = seqTipoTermoIntercambio,
                SeqInstituicaoNivelTipoVinculoAluno = dadosInstituicaoNivelAluno.Seq
            }).FirstOrDefault();

            // Carrega os orientadores (RN_ORT_013 - Filtro Orientador)
            ret.Colaboradores = ColaboradorDomainService.BuscarColaboradoresOrientacaoSelect(new ColaboradorFiltroVO
            {
                TipoAtividade = TipoAtividadeColaborador.Orientacao,
                VinculoAtivo = true,
                SeqsAlunos = new long[] { dados.SeqPessoaAtuacao },
                SeqNivelEnsino = dadosInstituicaoNivelTipoTermo.SeqNivelEnsino
            });

            // Carrega os tipos de participações
            ret.TiposParticipacoes = InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarInstituicaoNivelTipoOrientacaoParticipacaoSelect(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO()
            {
                SeqTipoOrientacao = seqTipoOrientacao,
                SeqNivelEnsino = dadosInstituicaoNivelTipoTermo.SeqNivelEnsino,
                SeqTermoIntercambio = seqTermoIntercambio,
                SeqTipoVinculo = dadosInstituicaoNivelAluno.SeqTipoVinculoAluno
            });

            if (ret.Participantes.Any())
            {
                foreach (var item in ret.Participantes)
                {
                    item.Instituicoes = InstituicaoExternaDomainService.BuscarInstituicaoExternaPorColaboradorSelect(new InstituicaoExternaFiltroVO
                    {
                        SeqColaborador = item.SeqColaborador,
                        Ativo = true,
                        SeqTermoIntercambio = ret.SeqTermoIntercambio,
                        SeqInstituicaoEnsino = dados.SeqInstituicaoEnsino,
                        TipoParticipacaoOrientacao = item.TipoParticipacaoOrientacao,
                        SeqTipoOrientacao = seqTipoOrientacao,
                    });
                }
            }

            return ret;
        }

        public void SalvarDadosAtendimentoIntercambio(AtendimentoIntercambioVO atendimentoIntercambioVO)
        {
            // Caso tenha selecionado termo de intercâmbio, significa que vai deferir o atendimento e deve efetuar as validações
            if (atendimentoIntercambioVO.SeqTermoIntercambio.HasValue)
            {
                // Recupera informações adicionais para validação
                var dadosValidacao = this.SearchProjectionByKey(atendimentoIntercambioVO.Seq, x => new
                {
                    x.SeqPessoaAtuacao
                });

                // Recupera os dados do Termo de Intercâmbio selecionado
                var dadosTermo = TermoIntercambioDomainService.BuscarDadosTermoIntercambio(atendimentoIntercambioVO.SeqTermoIntercambio.Value);

                // Recupera os dados de InstituicaoNivelTipoVinculoAluno
                var dadosInstituicaoNivelAluno = InstituicaoNivelTipoVinculoAlunoDomainService.BuscarInstituicaoNivelTipoVinculoAlunoPorPessoaAtuacao(dadosValidacao.SeqPessoaAtuacao);

                // Recupera os dados de InstituicaoNivelTipoTermoIntercambio
                var dadosTipoVinculo = InstituicaoNivelTipoTermoIntercambioDomainService.BuscarInstituicoesNivelTipoVinculoAluno(new InstituicaoNivelTipoTermoIntercambioFilterSpecification
                {
                    SeqInstituicaoNivelTipoVinculoAluno = dadosInstituicaoNivelAluno.Seq,
                    SeqTipoTermoIntercambio = dadosTermo.SeqTipoTermoIntercambio,
                }).FirstOrDefault();

                /*Ao clicar no botão próximo, consisitir:
                    1.  Se para o tipo de orientação selecionado existirem tipos de participação parametrizados como obrigatórios por instituição-nível-vínculo-tipo de termo,
                        e que não foram selecionados, abortar a operação e exibir a mensagem informativa:
                        "É necessário selecionar o(s) tipo(s) de participação [Tipos de participação parametrizados] para este tipo de orientação".
                */
                if (atendimentoIntercambioVO.SeqTipoOrientacao.HasValue)
                {
                    // Busca os tipos de participação configurados
                    var tiposParticipacaoOrientacao = InstituicaoNivelTipoOrientacaoParticipacaoDomainService.BuscarTipoParticipacaoEOrigemColaborador(new InstituicaoNivelTipoOrientacaoParticipacaoFiltroVO()
                    {
                        SeqTipoOrientacao = atendimentoIntercambioVO.SeqTipoOrientacao,
                        SeqTermoIntercambio = atendimentoIntercambioVO.SeqTermoIntercambio,
                        SeqNivelEnsino = atendimentoIntercambioVO.SeqNivelEnsino,
                        SeqTipoVinculo = dadosInstituicaoNivelAluno.SeqTipoVinculoAluno,
                        ObrigatorioOrientacao = true
                    });

                    string tiposNaoSelecionados = string.Empty;
                    foreach (var tipoParticipacao in tiposParticipacaoOrientacao)
                    {
                        // Verifica se existe a orientação deste tipo
                        var participacoes = atendimentoIntercambioVO.Participantes?.Where(p => p.TipoParticipacaoOrientacao == tipoParticipacao.TipoParticipacaoOrientacao);
                        if (!participacoes.SMCAny())
                            tiposNaoSelecionados += (string.IsNullOrEmpty(tiposNaoSelecionados) ? string.Empty : ", ") + SMCEnumHelper.GetDescription(tipoParticipacao.TipoParticipacaoOrientacao) + $" ({SMCEnumHelper.GetDescription(tipoParticipacao.OrigemColaborador)})";
                        else if (tipoParticipacao.OrigemColaborador != OrigemColaborador.InternoExterno)
                        {
                            // Verifica quais origens de colaborador foram selecionadas para esse tipo de orientação
                            var participacoesOrientador = participacoes.Select(participacao =>
                            {
                                // Recupera a origem da instituição do colaborador selecionado
                                var instituicaoInterna = InstituicaoExternaDomainService.SearchProjectionByKey(participacao.SeqInstituicaoExterna.Value, x => x.SeqInstituicaoEnsino.HasValue);

                                return instituicaoInterna ? OrigemColaborador.Interno : OrigemColaborador.Externo;
                            });

                            if (!participacoesOrientador.Any(t => t == tipoParticipacao.OrigemColaborador))
                                tiposNaoSelecionados += (string.IsNullOrEmpty(tiposNaoSelecionados) ? string.Empty : ", ") + SMCEnumHelper.GetDescription(tipoParticipacao.TipoParticipacaoOrientacao) + $" ({SMCEnumHelper.GetDescription(tipoParticipacao.OrigemColaborador)})";
                        }
                    }

                    if (!string.IsNullOrEmpty(tiposNaoSelecionados))
                        throw new SolicitacaoIntercambioTipoParticipanteFaltanteException(tiposNaoSelecionados);
                }
                else
                    // Limpa a lista de participantes caso não tenha sido selecionado o tipo de orientação
                    atendimentoIntercambioVO.Participantes = new List<SolicitacaoIntercambioParticipanteVO>();
            }
            else
                // Limpa a lista de participantes caso não tenha sido selecionado o termo
                atendimentoIntercambioVO.Participantes = new List<SolicitacaoIntercambioParticipanteVO>();

            var dadosSalvar = this.SearchByKey(atendimentoIntercambioVO.Seq);
            dadosSalvar.DataInicio = atendimentoIntercambioVO.DataInicio;
            dadosSalvar.DataFim = atendimentoIntercambioVO.DataFim;
            dadosSalvar.SeqTermoIntercambio = atendimentoIntercambioVO.SeqTermoIntercambio;
            dadosSalvar.SeqTipoOrientacao = atendimentoIntercambioVO.SeqTipoOrientacao;
            dadosSalvar.SeqTipoTermoIntercambio = atendimentoIntercambioVO.SeqTipoTermoIntercambio;
            dadosSalvar.SeqOrientacao = atendimentoIntercambioVO.SeqOrientacao;

            // Limpa os participantes em caso de não ter selecionado o tipo de orientação
            if (dadosSalvar.SeqTipoOrientacao.HasValue)
                dadosSalvar.Participantes = atendimentoIntercambioVO.Participantes?.Select(a => a.Transform<SolicitacaoIntercambioParticipante>()).ToList();
            else
                dadosSalvar.Participantes = new List<SolicitacaoIntercambioParticipante>();

            this.SaveEntity(dadosSalvar);
        }

        /// <summary>
        /// Atualiza a descrição de uma solicitação de intercâmbio
        /// </summary>
        /// <param name="seqSolicitacaoServico">Sequencial da solicitação a ser atualizada</param>
        /// <param name="indOriginal">Flag que informa se é para atualizar a descrição original ou a atualizada</param>
        /// <returns>Descrição atualizada da solicitação</returns>
        public string AtualizarDescricao(long seqSolicitacaoServico, bool indOriginal)
        {
            // Cria descrição da solicitação
            string novaDescricao = string.Empty;

            // Templates para HTML
            string templateItem = "<div class=\"{2}\"><div class=\"smc-display\"><label>{0}</label><p>{1}</p></div></div>";
            string templateFieldset = "<fieldset><legend>{0}</legend>{1}</fieldset>";

            // De acordo com o flag de original, monta a descrição
            if (indOriginal)
            {
                // Busca os dados do formulário da solicitação
                var spec = new SMCSeqSpecification<SolicitacaoIntercambio>(seqSolicitacaoServico);
                var dadosSolicitacao = this.SearchProjectionByKey(spec, x => new
                {
                    DadosCampos = x.Formularios.FirstOrDefault().DadosCampos.Select(d => new
                    {
                        TokenElemento = d.TokenElemento,
                        Valor = d.Valor
                    })
                });

                /// Salvar no campo "Descrição da solicitação original" em formato HTML, os campos a seguir que devem
                /// ser concatenados com os respectivos valores do formulário do SGF da solicitação:
                /// - Tipo de intercâmbio: token TIPO_INTERCAMBIO
                /// - Data início: token DATA_INICIO_INTERCAMBIO
                /// - Data fim: token DATA_FIM_INTERCAMBIO
                /// - Orientador: token ORIENTADOR
                /// - Instituição de ensino: token INSTITUICAO_INTERCAMBIO
                var tipo = dadosSolicitacao.DadosCampos.FirstOrDefault(d => d.TokenElemento == "TIPO_INTERCAMBIO")?.Valor;
                string itemTipo = string.Format(templateItem, "Tipo de intercâmbio", tipo?.Split('|').LastOrDefault(), SMCSizeHelper.GetSizeClasses(SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid8_24));
                string itemDataInicio = string.Format(templateItem, "Data início", dadosSolicitacao.DadosCampos.FirstOrDefault(d => d.TokenElemento == "DATA_INICIO_INTERCAMBIO")?.Valor, SMCSizeHelper.GetSizeClasses(SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid8_24));
                string itemDataFim = string.Format(templateItem, "Data fim", dadosSolicitacao.DadosCampos.FirstOrDefault(d => d.TokenElemento == "DATA_FIM_INTERCAMBIO")?.Valor, SMCSizeHelper.GetSizeClasses(SMCSize.Grid8_24, SMCSize.Grid12_24, SMCSize.Grid8_24, SMCSize.Grid8_24));
                string itemOrientador = string.Format(templateItem, "Orientador na instituição de ensino destino", dadosSolicitacao.DadosCampos.FirstOrDefault(d => d.TokenElemento == "ORIENTADOR_INSTITUICAO_DESTINO")?.Valor, SMCSizeHelper.GetSizeClasses(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24));
                string itemInstituicao = string.Format(templateItem, "Instituição de ensino destino", dadosSolicitacao.DadosCampos.FirstOrDefault(d => d.TokenElemento == "INSTITUICAO_INTERCAMBIO")?.Valor, SMCSizeHelper.GetSizeClasses(SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24, SMCSize.Grid12_24));
                novaDescricao = string.Format("<div class=\"smc-sga-confirmacao-dados\">{0}{1}{2}{3}{4}</div>", itemTipo, itemDataInicio, itemDataFim, itemOrientador, itemInstituicao);
            }
            else
            {
                // Busca os dados da solicitação de intercâmbio
                var solicitacao = this.SearchProjectionByKey(seqSolicitacaoServico, x => new
                {
                    DescricaoTermo = x.TermoIntercambio.Descricao,
                    DescricaoTipoTermo = x.TermoIntercambio.ParceriaIntercambioTipoTermo.TipoTermoIntercambio.Descricao,
                    NomeInstituicaoExterna = x.TermoIntercambio.ParceriaIntercambioInstituicaoExterna.InstituicaoExterna.Nome,
                    DataInicio = x.DataInicio,
                    DataFim = x.DataFim,
                    DescricaoTipoOrientacao = x.TipoOrientacao.Descricao,
                    Participantes = x.Participantes.Select(p => new
                    {
                        NomeColaborador = p.Colaborador.DadosPessoais.Nome,
                        TipoParticipacao = p.TipoParticipacaoOrientacao,
                        NomeInstituicao = p.InstituicaoExterna.Nome,
                        DataInicio = p.DataInicio,
                        DataFim = p.DataFim
                    })
                });

                /// Salvar  no campo "Descrição da solicitação atualizada" em formato HTML, os campos a seguir que
                /// devem ser concatenados com os respectivos valores preenchidos em UC_SRC_004_13_01 - Atendimento
                /// de Intercâmbio
                /// Intercâmbio (fieldset):
                /// - Termo de intercâmbio: descrição do termo de intercâmbio associado ao aluno em questão.
                /// - Tipo de termo: descrição do tipo do termo associado ao aluno em questão.
                /// - Instituição de ensino externa: descrição da instituição externa associada ao termo de intercâmbio
                /// associado ao aluno em questão.
                /// Data início: data início do termo associado ao aluno ou a data preenchida na tela.
                /// Data fim: data fim do termo associado ao aluno ou a data preenchida na tela.
                string termo = string.Format(templateItem, "Termo de intercambio", solicitacao.DescricaoTermo, SMCSizeHelper.GetSizeClasses(SMCSize.Grid10_24, SMCSize.Grid10_24, SMCSize.Grid10_24, SMCSize.Grid10_24));
                string tipoTermo = string.Format(templateItem, "Tipo de termo", solicitacao.DescricaoTipoTermo, SMCSizeHelper.GetSizeClasses(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24));
                string instituicao = string.Format(templateItem, "Instituição de ensino externa", solicitacao.NomeInstituicaoExterna, SMCSizeHelper.GetSizeClasses(SMCSize.Grid10_24, SMCSize.Grid10_24, SMCSize.Grid10_24, SMCSize.Grid10_24));
                string dataInicio = string.Format(templateItem, "Data início", solicitacao.DataInicio.SMCDataAbreviada(), SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
                string dataFim = string.Format(templateItem, "Data fim", solicitacao.DataFim.SMCDataAbreviada(), SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
                string fdIntercambio = string.Format(templateFieldset, "Intercâmbio", termo + tipoTermo + instituicao + dataInicio + dataFim);

                /// Orientação (fieldset):
                /// - Tipo de orientação: descrição do tipo de orientação selecionado na tela.
                string tipoOrientacao = string.Format(templateItem, "Tipo de orientação", solicitacao.DescricaoTipoOrientacao, SMCSizeHelper.GetSizeClasses(SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid24_24));
                string fdOrientacao = string.Format(templateFieldset, "Orientação", tipoOrientacao);

                /// Participações (fieldset):
                /// - Colaborador: nome do professor selecionado na tela.
                /// - Tipo de participação: tipo de participação selecionado na tela.
                /// - Instituição de ensino: instituição de ensino selecionado na tela.
                /// - Data início: data de inicio da participação
                /// - Data fim: data de fim da participação
                string listaParticipante = string.Empty;
                foreach (var participante in solicitacao.Participantes)
                {
                    string colaborador = string.Format(templateItem, "Colaborador", participante.NomeColaborador, SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
                    string tipoParticipante = string.Format(templateItem, "Tipo de participação", participante.TipoParticipacao.SMCGetDescription(), SMCSizeHelper.GetSizeClasses(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24));
                    string instituicaoColab = string.Format(templateItem, "Instituição de ensino", participante.NomeInstituicao, SMCSizeHelper.GetSizeClasses(SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24, SMCSize.Grid6_24));
                    string dataInicioParticipante = string.Format(templateItem, "Data início", participante.DataInicio.SMCDataAbreviada(), SMCSizeHelper.GetSizeClasses(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24));
                    string dataFimParticipante = string.Format(templateItem, "Data fim", participante.DataFim.SMCDataAbreviada(), SMCSizeHelper.GetSizeClasses(SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24, SMCSize.Grid4_24));

                    listaParticipante += colaborador + tipoParticipante + instituicaoColab + dataInicioParticipante + dataFimParticipante + "<hr>";
                }
                string fdParticipante = string.Format(templateFieldset, "Participações", listaParticipante);

                novaDescricao = string.Format("<div class=\"smc-sga-confirmacao-dados\">{0}{1}{2}</div>", fdIntercambio, fdOrientacao, fdParticipante);
            }

            // Salva a Descrição
            if (indOriginal)
                this.UpdateFields<SolicitacaoIntercambio>(new SolicitacaoIntercambio { Seq = seqSolicitacaoServico, DescricaoOriginal = novaDescricao }, x => x.DescricaoOriginal);
            else
                this.UpdateFields<SolicitacaoIntercambio>(new SolicitacaoIntercambio { Seq = seqSolicitacaoServico, DescricaoAtualizada = novaDescricao }, x => x.DescricaoAtualizada);

            return novaDescricao;
        }
    }
}