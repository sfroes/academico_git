using SMC.Academico.Common.Areas.ALN.Exceptions;
using SMC.Academico.Common.Areas.ALN.Includes;
using SMC.Academico.Common.Areas.ORT.Constants;
using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.Common.Constants;
using SMC.Academico.Common.Enums;
using SMC.Academico.Domain.Areas.ALN.Models;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.ALN.ValueObjects;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.Specifications;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.ORT.DomainServices;
using SMC.Academico.Domain.Areas.ORT.Models;
using SMC.Academico.Domain.Areas.ORT.Specifications;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.SRC.DomainServices;
using SMC.Academico.Domain.Areas.SRC.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Exceptions;
using SMC.Framework.Repository;
using SMC.Framework.Security;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Data;
using SMC.IntegracaoAcademico.ServiceContract.Areas.IAC.Interfaces;
using SMC.Notificacoes.Common.Areas.NTF.Enums;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Data;
using SMC.Notificacoes.ServiceContract.Areas.NTF.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.ALN.DomainServices
{
    public class PlanoEstudoDomainService : AcademicoContextDomain<PlanoEstudo>
    {
        private IIntegracaoAcademicoService IntegracaoAcademicoService
        {
            get { return this.Create<IIntegracaoAcademicoService>(); }
        }

        private INotificacaoService NotificacaoService => this.Create<INotificacaoService>();

        #region [ _buscaDiferencaCreditos ]

        private string _buscaDiferencaCreditos =
            @"
            declare
	            @CREDITOS_ATUAL int,
	            @CREDITOS_ANTERIOR int

            select	@CREDITOS_ATUAL = sum(p.qtd_credito)
            from	(	select distinct
				            t.seq_turma,
				            c.qtd_credito
			            from	ALN.plano_estudo p
			            join	ALN.plano_estudo_item i
					            on p.seq_plano_estudo = i.seq_plano_estudo
			            join	TUR.divisao_turma d
					            on i.seq_divisao_turma = d.seq_divisao_turma
			            join	TUR.turma t
					            on d.seq_turma = t.seq_turma
			            join	TUR.turma_configuracao_componente tcc
					            on t.seq_turma = tcc.seq_turma
					            and tcc.ind_principal = 1
			            join	CUR.configuracao_componente cc
					            on tcc.seq_configuracao_componente = cc.seq_configuracao_componente
			            join	CUR.componente_curricular c
					            on cc.seq_componente_curricular = c.seq_componente_curricular
			            where	p.seq_aluno_historico_ciclo_letivo = @SEQ_ALUNO_HISTORICO_CICLO_LETIVO
			            and		p.ind_atual = 1) p

            select	@CREDITOS_ANTERIOR = sum(p.qtd_credito)
            from	(	select distinct 
				            t.seq_turma,
				            c.qtd_credito
			            from	ALN.plano_estudo p
			            join	ALN.plano_estudo_item i
					            on p.seq_plano_estudo = i.seq_plano_estudo
			            join	TUR.divisao_turma d
					            on i.seq_divisao_turma = d.seq_divisao_turma
			            join	TUR.turma t
					            on d.seq_turma = t.seq_turma
			            join	TUR.turma_configuracao_componente tcc
					            on t.seq_turma = tcc.seq_turma
					            and tcc.ind_principal = 1
			            join	CUR.configuracao_componente cc
					            on tcc.seq_configuracao_componente = cc.seq_configuracao_componente
			            join	CUR.componente_curricular c
					            on cc.seq_componente_curricular = c.seq_componente_curricular
			            where	p.seq_aluno_historico_ciclo_letivo = @SEQ_ALUNO_HISTORICO_CICLO_LETIVO
			            and		p.ind_atual = 0
			            and		p.dat_inclusao = (	select	max(dat_inclusao)
										            from	ALN.plano_estudo p2
										            where	p2.seq_aluno_historico_ciclo_letivo = @SEQ_ALUNO_HISTORICO_CICLO_LETIVO
										            and		p2.ind_atual = 0)) p

            select ISNULL(@CREDITOS_ATUAL,0) - ISNULL(@CREDITOS_ANTERIOR,0) as DiferencaCredito";

        #endregion [ _buscaDiferencaCreditos ]

        #region [ DomainServices ]

        private SolicitacaoMatriculaDomainService SolicitacaoMatriculaDomainService => Create<SolicitacaoMatriculaDomainService>();

        private AlunoDomainService AlunoDomainService => Create<AlunoDomainService>();

        private AlunoHistoricoCicloLetivoDomainService AlunoHistoricoCicloLetivoDomainService => Create<AlunoHistoricoCicloLetivoDomainService>();

        private ConfiguracaoEventoLetivoDomainService ConfiguracaoEventoLetivoDomainService => Create<ConfiguracaoEventoLetivoDomainService>();

        private DivisaoTurmaColaboradorDomainService DivisaoTurmaColaboradorDomainService => Create<DivisaoTurmaColaboradorDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private OrientacaoColaboradorDomainService OrientacaoColaboradorDomainService => Create<OrientacaoColaboradorDomainService>();

        private OrientacaoDomainService OrientacaoDomainService => Create<OrientacaoDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        private EntidadeConfiguracaoNotificacaoDomainService EntidadeConfiguracaoNotificacaoDomainService => this.Create<EntidadeConfiguracaoNotificacaoDomainService>();

        private SolicitacaoServicoEnvioNotificacaoDomainService SolicitacaoServicoEnvioNotificacaoDomainService => this.Create<SolicitacaoServicoEnvioNotificacaoDomainService>();

        #endregion [ DomainServices ]

        /// <summary>
        /// Retorna o plano de estudos de um aluno para o plano anterior
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno que deve ter o plano retornado</param>
        /// <param name="seqCicloLetivo">Sequencial do ciclo letivo do plano de estudos</param>
        /// <param name="seqSolicitacaoServicoOrigem">Solicitação de serviço que originou esta necessidade (opcional)</param>
        /// <param name="observacaoPlanoNovo">Descrição para observação a ser adicionada no plano de estudos novo que será criado</param>
        public void RetornarAoPlanoEstudosAnterior(long seqAluno, long seqCicloLetivo, long? seqSolicitacaoServicoOrigem, string observacaoPlanoNovo)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                // Recupera qual o plano de estudo do aluno no ciclo letivo
                var specPlanoAtual = new PlanoEstudoFilterSpecification()
                {
                    SeqAluno = seqAluno,
                    SeqCicloLetivo = seqCicloLetivo,
                    Atual = true
                };
                specPlanoAtual.SetOrderByDescending(x => x.Seq);
                var planoAtual = this.SearchProjectionByKey(specPlanoAtual, x => new
                {
                    Seq = x.Seq,
                    Itens = x.Itens.Select(i => new
                    {
                        i.SeqConfiguracaoComponente,
                        i.SeqDivisaoTurma,
                        DadosOrientacao = new
                        {
                            i.SeqOrientacao,
                            Colaboradores = i.Orientacao.OrientacoesColaborador.Select(oc => new
                            {
                                SeqOrientacaoColaborador = oc.Seq,
                                oc.SeqColaborador,
                                oc.SeqInstituicaoExterna,
                                DataInicioOrientacao = oc.DataInicioOrientacao,
                                oc.DataFimOrientacao
                            })
                        },
                    }),
                    TipoAluno = x.AlunoHistoricoCicloLetivo.TipoAluno
                });

                // Caso não tenha plano atual neste ciclo, dispara exceção
                if (planoAtual == null)
                    throw new SMCApplicationException("Não foi encontrado plano de estudos atual para este aluno neste ciclo letivo");

                // Recupera o plano imediatamente anterior
                var specPlanoAnterior = new PlanoEstudoFilterSpecification()
                {
                    SeqAluno = seqAluno,
                    SeqCicloLetivo = seqCicloLetivo,
                    Atual = false
                };
                specPlanoAnterior.SetOrderByDescending(x => x.Seq);
                var planoAnterior = this.SearchProjectionBySpecification(specPlanoAnterior, x => new
                {
                    Seq = x.Seq,
                    SeqAlunoHistoricoCicloLetivo = x.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = x.SeqMatrizCurricularOferta,
                    AnoCiclo = x.AlunoHistoricoCicloLetivo.CicloLetivo.Ano,
                    NumeroCiclo = x.AlunoHistoricoCicloLetivo.CicloLetivo.Numero,
                    Itens = x.Itens.Select(i => new
                    {
                        i.SeqConfiguracaoComponente,
                        i.SeqDivisaoTurma,
                        DadosOrientacao = new
                        {
                            i.SeqOrientacao,
                            Colaboradores = i.Orientacao.OrientacoesColaborador.Select(oc => new
                            {
                                SeqOrientacaoColaborador = oc.Seq,
                                oc.SeqColaborador,
                                oc.SeqInstituicaoExterna
                            })
                        },
                        CodigoTurma = (int?)i.DivisaoTurma.Turma.Codigo,
                        NumeroTurma = (short?)i.DivisaoTurma.Turma.Numero,
                        Creditos = i.ConfiguracaoComponente.ComponenteCurricular.Credito,
                        SeqTurma = (long?)i.DivisaoTurma.SeqTurma,
                        SeqComponenteCurricular = i.ConfiguracaoComponente.SeqComponenteCurricular
                    })
                }).FirstOrDefault();

                // Caso não tenha plano atual neste ciclo, dispara exceção
                if (planoAnterior == null)
                    throw new SMCApplicationException("Não foi encontrado plano de estudos anteriores para este aluno neste ciclo letivo");

                // Recupera os dados de origem do aluno
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqAluno, true);

                // Recupera o período letivo do ciclo em questão para a oferta do aluno
                var dataPeriodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqCicloLetivo, dadosOrigem.SeqCursoOfertaLocalidadeTurno, planoAtual.TipoAluno, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);

                /* 3.2.2. Para cada divisão de turma existente no plano de estudo ATUAL do ciclo letivo do processo em questão, subtrair 1 da quantidade de vagas ocupadas. */
                // Recupera o plano de estudo atual no ciclo do aluno

                var seqsDivisoesOrientacoesEncerradas = new List<long>();
                foreach (var item in planoAtual.Itens)
                {
                    if (item.SeqDivisaoTurma.HasValue)
                        DivisaoTurmaDomainService.LiberarVagaTurma(seqAluno, item.SeqDivisaoTurma.Value, true);

                    // Caso o item tenha orientação gerada, verificar se ele está presente no plano anterior.
                    if (item.DadosOrientacao != null)
                    {
                        // Caso não esteja, atualizar o campo data fim do colaborador na orientação do item do plano, de acordo com:
                        if (!planoAnterior.Itens.Any(i => i.SeqDivisaoTurma == item.SeqDivisaoTurma))
                        {
                            item.DadosOrientacao.Colaboradores.SMCForEach(oc =>
                            {
                                OrientacaoColaboradorDomainService.UpdateFields(new OrientacaoColaborador
                                {
                                    Seq = oc.SeqOrientacaoColaborador,
                                    DataFimOrientacao = oc.DataInicioOrientacao > DateTime.Now ? oc.DataInicioOrientacao : dataPeriodoLetivo.DataFim < DateTime.Now ? oc.DataFimOrientacao : DateTime.Now,
                                }, x => x.DataFimOrientacao);

                                // Adiciona na lista qual divisao teve orientação encerrada
                                seqsDivisoesOrientacoesEncerradas.Add(item.SeqDivisaoTurma.Value);
                            });
                        }
                    }
                }

                // Exclui as associações do colaborador caso o mesmo não tenha mais orientações para a turma
                if (seqsDivisoesOrientacoesEncerradas.Any())
                    DivisaoTurmaColaboradorDomainService.ExcluirProfessorOrientador(seqsDivisoesOrientacoesEncerradas, planoAtual.Seq);

                // Informa que o plano não é mais o atual
                UpdateFields(new PlanoEstudo { Seq = planoAtual.Seq, Atual = false }, x => x.Atual);

                /* 3.2.3. Incluir um novo registro de plano de estudos exatamente igual ao plano não atual que possui maior sequencial,
								 * dentre os planos do ciclo letivo do processo. No plano criado, setar o indicador de atual como “Sim”, associar a solicitação
								 * ao plano e atribuir ao campo observação com o seguinte valor
								 * “Plano criado devido à reabertura da solicitação: <número do protocolo da solicitação reaberta>.”.
								 * Copiar todos os itens do plano de estudo encontrado, somando 1 na quantidade de vagas ocupadas de cada divisão das turmas.*/

                // Cria o novo plano de estudos copiando os dados do plano anterior
                var planoEstudoNovo = new PlanoEstudo
                {
                    Atual = true,
                    SeqAlunoHistoricoCicloLetivo = planoAnterior.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = planoAnterior.SeqMatrizCurricularOferta,
                    Observacao = observacaoPlanoNovo
                };

                // Caso tenha sido informada a solicitação origem desta alteração, associa ela ao novo plano de estudos
                if (seqSolicitacaoServicoOrigem.HasValue)
                    // Associa a solicitação ao plano de estudo novo
                    planoEstudoNovo.SeqSolicitacaoServico = seqSolicitacaoServicoOrigem;

                // Inicia a lista de itens do plano de estudo novo
                planoEstudoNovo.Itens = new List<PlanoEstudoItem>();

                // Cria uma lista para armazenar as turmas que não tem mais vagas disponíveis
                List<string> turmasComProblema = new List<string>();

                // Armazena quais orientações foram ajustadas para ter data fim igual do fim do ciclo letivo
                var orientacoesIncluidas = new List<DivisaoTurmaColaboradorVO>();

                // Percorre os itens do plano anterior e adiciona no novo plano, já descontando a vaga
                foreach (var item in planoAnterior.Itens)
                {
                    var itemNovo = new PlanoEstudoItem
                    {
                        SeqConfiguracaoComponente = item.SeqConfiguracaoComponente,
                        SeqDivisaoTurma = item.SeqDivisaoTurma,
                    };

                    // Caso seja turma, processa a vaga
                    if (item.SeqDivisaoTurma.HasValue)
                    {
                        // Recupera a situação da turma desta divisão
                        var dadosTurma = DivisaoTurmaDomainService.SearchProjectionByKey(item.SeqDivisaoTurma.Value, x => new
                        {
                            Situacao = x.Turma.HistoricosSituacao.OrderByDescending(h => h.Seq).FirstOrDefault().SituacaoTurma,
                            Codigo = x.Turma.Codigo,
                            Numero = x.Turma.Numero
                        });

                        if (dadosTurma.Situacao == Common.Areas.TUR.Enums.SituacaoTurma.Cancelada)
                        {
                            turmasComProblema.Add($"{dadosTurma.Codigo}.{dadosTurma.Numero} - Motivo: Turma cancelada");
                            continue;
                        }

                        var retTurma = DivisaoTurmaDomainService.ProcessarVagaTurma(seqAluno, item.SeqDivisaoTurma.Value, $"{item.CodigoTurma}.{item.NumeroTurma}", true);
                        if (!string.IsNullOrEmpty(retTurma))
                        {
                            turmasComProblema.Add(retTurma);
                            continue;
                        }
                    }

                    // Caso tenha orientação no item
                    if (item.DadosOrientacao != null)
                    {
                        // Atribui o SeqOrientacao no item
                        itemNovo.SeqOrientacao = item.DadosOrientacao.SeqOrientacao;

                        // Verificar se o item estava na solicitação atual.
                        // Caso não esteja, significa que ele teve data de fim definida e precisa ser atualizado.
                        if (!planoAtual.Itens.Any(i => i.SeqDivisaoTurma == item.SeqDivisaoTurma))
                        {
                            item.DadosOrientacao.Colaboradores.SMCForEach(oc =>
                            {
                                OrientacaoColaboradorDomainService.UpdateFields(new OrientacaoColaborador
                                {
                                    Seq = oc.SeqOrientacaoColaborador,
                                    DataFimOrientacao = dataPeriodoLetivo.DataFim
                                }, x => x.DataFimOrientacao);

                                orientacoesIncluidas.Add(new DivisaoTurmaColaboradorVO
                                {
                                    SeqColaborador = oc.SeqColaborador,
                                    SeqDivisao = item.SeqDivisaoTurma.Value
                                });
                            });
                        }
                    }

                    planoEstudoNovo.Itens.Add(itemNovo);
                }

                DivisaoTurmaColaboradorDomainService.AdicionarProfessorOrientador(orientacoesIncluidas);

                // Caso tenha turma sem vagas...
                if (turmasComProblema.Any())
                    throw new SMCApplicationException($"Não é possível prosseguir. Existem itens da solicitação que impedem a sua reabertura: <br/> - Turma {string.Join("<br/> - Turma ", turmasComProblema)}");

                // Salva o plano novo
                SaveEntity(planoEstudoNovo);

                /* 3.2.5. Acionar a RN_MAT_121 - Ajustar valor mensalidade na alteração de plano.*/
                AjustarValorMensalidadePlano(planoAnterior.SeqAlunoHistoricoCicloLetivo,
                                dadosOrigem.CodigoAlunoMigracao ?? 0,
                                planoAnterior.AnoCiclo,
                                planoAnterior.NumeroCiclo,
                                planoAnterior.Itens.Select(i => new AlunoTurmaSGPData
                                {
                                    Creditos = i.Creditos,
                                    SeqComponenteCurricular = i.SeqComponenteCurricular,
                                    SeqTurma = i.SeqTurma
                                }).ToList());

                transacao.Commit();
            }
        }

        /// <summary>
        /// Ajusta o valor da mensalidade no SGP, de acordo com o plano de estudo.
        /// RN_MAT_121
        /// </summary>
        public void AjustarValorMensalidadePlano(long seqAlunoHistoricoCicloLetivo, long codigoAlunoSGP, int anoCicloLetivo, short numeroCicloLetivo, List<AlunoTurmaSGPData> turmas)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                // Calcula a diferença de créditos entre os planos de estudo do aluno
                var diferenca = RawQuery<int>(_buscaDiferencaCreditos,
                        new SMCFuncParameter("@SEQ_ALUNO_HISTORICO_CICLO_LETIVO", seqAlunoHistoricoCicloLetivo)).FirstOrDefault();

                // Cria o data para enviar ao SGP e atualizar os planos de estudo
                var alunoVeteranoSGPData = new AlunoVeteranoSGPData()
                {
                    CodigoAlunoSGP = codigoAlunoSGP,
                    AnoCicloLetivo = anoCicloLetivo,
                    NumeroCicloLetivo = numeroCicloLetivo,
                    DiferencaCreditos = diferenca,
                    Turmas = turmas,
                    SomenteAtualizaProfessor = false,
                    Usuario = SMCContext.User.SMCGetCodigoPessoa().GetValueOrDefault().ToString()
                };
                IntegracaoAcademicoService.AtualizarPlanoEstudoAlunoSGP(alunoVeteranoSGPData);

                transacao.Commit();
            }
        }

        /// <summary>
        /// Alterar o plano de estudo atual e criar um novo plano de estudo com os itens da
        /// solicitação de matrícula
        /// </summary>
        /// <param name="seqPessoaAtuacao">Sequencial da pessoa atuação</param>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matrícula</param>
        /// <param name="seqAlunoHistoricoCicloLetivo">Sequencial do aluno histórico ciclo letivo para criação do novo plano</param>
        public void AlterarPlanoDeEstudoPorSolicitacaoMatricula(long seqPessoaAtuacao, long seqSolicitacaoMatricula, long seqAlunoHistoricoCicloLetivo)
        {
            // Verifica se o aluno possui um plano de estudos atual no histórico ciclo letivo informado.
            var specPlano = new PlanoEstudoFilterSpecification()
            {
                SeqAluno = seqPessoaAtuacao,
                SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivo,
                Atual = true
            };

            var planoAtual = this.SearchByKey(specPlano, IncludesPlanoEstudo.Itens);
            var seqsConfiguracaoComponentePlanoAtual = planoAtual?.Itens.Select(s => s.SeqConfiguracaoComponente.GetValueOrDefault()).ToList() ?? new List<long>();

            // Busca a matriz atual do aluno
            long? seqMatrizCurricularOferta = null;
            List<PlanoEstudoItem> itensPlanoAtual = new List<PlanoEstudoItem>();

            // Busca os items da solicitação de matrícula
            var itensSolicitacao = SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaItensPlano(seqSolicitacaoMatricula);

            // Recupera os itens da solicitação que fazem parte do plano, mas foram marcados como cancelado, entretanto devem ser considerados na criação de um novo plano
            // Quer dizer que foi feita uma alteração do grupo de uma divisão, mas foi indeferida. Sendo assim, devo voltar o item anterior para o plano
            // 1.3.2.1. Neste caso, se existir outro item na solicitação, da mesma turma, com a mesma divisão de componente e com o campo “Pertence ao plano de estudo” igual “Sim”, incluí - lo no novo plano de estudo.
            var itensNaoPertence = itensSolicitacao.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && !w.PertencePlanoEstudo.GetValueOrDefault()).ToList();
            var itensCanceladosConsiderar = itensSolicitacao.Where(w => w.PertencePlanoEstudo.GetValueOrDefault() &&
                                                                        itensNaoPertence.Any(i => i.SeqTurma == w.SeqTurma && i.SeqDivisaoComponente == w.SeqDivisaoComponente)).Select(i => i.Seq).ToList();

            if (planoAtual != null)
            {
                itensPlanoAtual = planoAtual.Itens.ToList();

                // Se encontrou o plano, é alteração de plano dentro do mesmo ciclo letivo.
                // Guarda a matriz curricular do aluno para criação do próximo plano
                seqMatrizCurricularOferta = planoAtual.SeqMatrizCurricularOferta;

                // Finaliza apenas os itens não aproveitados do plano
                var SeqsItensNaoAlterado = itensSolicitacao.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && w.PertencePlanoEstudo.GetValueOrDefault() == true
                       || w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado
                       || itensCanceladosConsiderar.Contains(w.Seq)).Select(s => s.SeqConfiguracaoComponente).ToList();

                var SeqsItensPlano = planoAtual.Itens.Where(w => !SeqsItensNaoAlterado.Contains(w.SeqConfiguracaoComponente)).Select(s => s.Seq).ToList();

                // Finaliza as orientações de turma do plano atual
                FinalizaOrientacoesPlanoItens(SeqsItensPlano, planoAtual.Seq);

                // Altera o indicador de atual para false
                planoAtual.Atual = false;
                this.UpdateFields(planoAtual, x => x.Atual);
            }
            else
            {
                // Se não encontrou o plano, é renovação/reabertura
                // Busca a matriz oferta do ultimo plano de estudo do aluno
                seqMatrizCurricularOferta = AlunoDomainService.BuscarDadosMatrizCurricularAlunoUltimoPlano(seqPessoaAtuacao).SeqMatrizCurricularOferta;
            }

            // Se tem algum item que gera orientação, monta a orientação base
            OrientacaoVO orientacaoBase = null;
            var itemGeraOrientacao = itensSolicitacao.FirstOrDefault(x => x.GeraOrientacao.HasValue && x.GeraOrientacao.Value && x.SeqDivisaoTurma.HasValue &&
                          ((x.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso && x.PertencePlanoEstudo.GetValueOrDefault() == false) ||
                           itensCanceladosConsiderar.Contains(x.Seq)));
            //|| (x.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && x.PertencePlanoEstudo.GetValueOrDefault() == true)
            //|| (x.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado)));

            if (itemGeraOrientacao != null)
            {
                // Recupera os dados de origem da pessoa atuação
                var dadosOrigem = PessoaAtuacaoDomainService.RecuperaDadosOrigem(seqPessoaAtuacao, true);

                List<OrientacaoColaboradorVO> listaOrientadores = new List<OrientacaoColaboradorVO>();

                var tokenServicoSolicitacao = SolicitacaoMatriculaDomainService.SearchProjectionByKey(seqSolicitacaoMatricula, x => x.ConfiguracaoProcesso.Processo.Servico.Token);
                if (tokenServicoSolicitacao != TOKEN_SERVICO.SOLICITACAO_INCLUSAO_DISCIPLINA_ELETIVA)
                {
                    // Busca dados de orientação do aluno
                    var specOrientacao = new OrientacaoFilterSpecification()
                    {
                        SeqPessoaAtuacao = seqPessoaAtuacao,
                        SomenteSemTipoTermoIntercambio = true,
                        TokenTipoOrientacao = TOKEN_TIPO_ORIENTACAO.ORIENTACAO_CONCLUSAO_CURSO
                    };

                    listaOrientadores = OrientacaoDomainService.SearchProjectionByKey(specOrientacao, o =>
                    o.OrientacoesColaborador.Where(x => x.DataInicioOrientacao <= DateTime.Today && (!x.DataFimOrientacao.HasValue || x.DataFimOrientacao >= DateTime.Today))
                    .Select(s => new OrientacaoColaboradorVO()
                    {
                        SeqColaborador = s.SeqColaborador,
                        TipoParticipacaoOrientacao = s.TipoParticipacaoOrientacao,
                        SeqInstituicaoExterna = s.SeqInstituicaoExterna
                    }).ToList());
                }

                //Criar orientação da pessoa atuação
                var orientacaoPessoaAtuacao = new List<OrientacaoPessoaAtuacaoVO>();
                orientacaoPessoaAtuacao.Add(new OrientacaoPessoaAtuacaoVO() { SeqPessoaAtuacao = seqPessoaAtuacao });

                // Cria a orientação base
                orientacaoBase = new OrientacaoVO()
                {
                    SeqEntidadeInstituicao = dadosOrigem.SeqInstituicaoEnsino,
                    SeqNivelEnsino = dadosOrigem.SeqNivelEnsino,
                    SeqTipoVinculoAluno = dadosOrigem.SeqTipoVinculoAluno,
                    OrientacoesColaborador = listaOrientadores,
                    OrientacoesPessoaAtuacao = orientacaoPessoaAtuacao,
                };

                // Buscar período letivo do ciclo do plano
                var seqsFiltroPeriodo = AlunoHistoricoCicloLetivoDomainService.BuscarCicloLetivoLocalidadeTurnoPorAlunoHistoricoCicloLetivo(seqAlunoHistoricoCicloLetivo);
                var seqCursoOfertaLocalidadeTurnoTurma = DivisaoTurmaDomainService.BuscarCursoOfertaLocalidadeTurnoPorDivisaoTurmaQuantidades(itemGeraOrientacao.SeqDivisaoTurma.Value);
                if (seqsFiltroPeriodo.SeqCicloLetivo > 0 && seqCursoOfertaLocalidadeTurnoTurma > 0)
                {
                    var dataPeriodoLetivo = ConfiguracaoEventoLetivoDomainService.BuscarDatasEventoLetivo(seqsFiltroPeriodo.SeqCicloLetivo, seqCursoOfertaLocalidadeTurnoTurma, seqsFiltroPeriodo.TipoAluno, TOKEN_TIPO_EVENTO.PERIODO_LETIVO);

                    // Atualiza as datas de orientação com as datas do ciclo letivo
                    orientacaoBase?.OrientacoesColaborador.SMCForEach(f => f.DataInicioOrientacao = dataPeriodoLetivo.DataInicio);
                    orientacaoBase?.OrientacoesColaborador.SMCForEach(f => f.DataFimOrientacao = dataPeriodoLetivo.DataFim);
                }
            }

            // Cria o novo plano de estudo
            var planoEstudoNovo = new PlanoEstudo()
            {
                SeqAlunoHistoricoCicloLetivo = seqAlunoHistoricoCicloLetivo,
                SeqMatrizCurricularOferta = seqMatrizCurricularOferta,
                SeqSolicitacaoServico = seqSolicitacaoMatricula,
                Atual = true,
            };

            #region Validação de componente dispensado comentada porque será abortada a operação ao invés de incluir no plano as disciplinas não dispensadas

            ////Verificando se tem algum componente dispensado para não inserir no plano de estudo 
            //var alunoHistoricoCicloLetivo = this.AlunoHistoricoCicloLetivoDomainService.SearchByKey(new SMCSeqSpecification<AlunoHistoricoCicloLetivo>(seqAlunoHistoricoCicloLetivo));
            //var componentesDispensados = this.HistoricoEscolarDomainService.ObterComponentesCurricularesDispensados(alunoHistoricoCicloLetivo.SeqAlunoHistorico);           
            //var seqsComponentesDispensados = componentesDispensados.Where(a => a.SeqComponenteCurricular.HasValue).Select(a => a.SeqComponenteCurricular).ToList();

            //var seqsConfiguracaoComponenteItensSolicitacao = itensSolicitacao.Where(a => a.SeqConfiguracaoComponente.HasValue).Select(a => a.SeqConfiguracaoComponente).ToArray();
            //var specConfiguracaoComponente = new ConfiguracaoComponenteFilterSpecification() { SeqConfiguracoesComponentes = seqsConfiguracaoComponenteItensSolicitacao };
            //var seqsComponentesItensSolicitacao = this.ConfiguracaoComponenteDomainService.SearchProjectionBySpecification(specConfiguracaoComponente, x => x.SeqComponenteCurricular).ToList();

            //foreach(var seqComponenteDispensado in seqsComponentesDispensados)
            //{
            //    if(seqsComponentesItensSolicitacao.Contains(seqComponenteDispensado.Value))
            //    {
            //        var itensParaRemover = itensSolicitacao.Where(a => a.SeqComponenteCurricular == seqComponenteDispensado).ToList();

            //        foreach(var itemParaRemover in itensParaRemover)
            //        {
            //            itensSolicitacao.Remove(itemParaRemover);
            //        }
            //    }
            //}

            #endregion

            //Só é criado orientação quando o item foi incluído no plano de estudo, quando o item ja pertencia ao plano utiliza a orientação antiga
            var listaItensOrientacao = new List<PlanoEstudoItem>();

            //Itens incluidos no plano - gerar orientação nova
            listaItensOrientacao.AddRange(itensSolicitacao.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso && w.PertencePlanoEstudo.GetValueOrDefault() == false)
                   .Select(s => new PlanoEstudoItem()
                   {
                       SeqDivisaoTurma = s.SeqDivisaoTurma,
                       SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                       Orientacao = OrientacaoDomainService.CriarOrientacaoPlanoEstudoItem(s.GeraOrientacao.GetValueOrDefault(), s.SeqTipoOrientacao.GetValueOrDefault(), s.TipoParticipacaoOrientacao.GetValueOrDefault(), orientacaoBase),
                   }).ToList());

            //Itens cancelados por alguma modificação de grupo que não foi deferida... voltar com o item
            listaItensOrientacao.AddRange(itensSolicitacao.Where(w => itensCanceladosConsiderar.Contains(w.Seq))
                   .Select(s => new PlanoEstudoItem()
                   {
                       SeqDivisaoTurma = s.SeqDivisaoTurma,
                       SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                       Orientacao = OrientacaoDomainService.CriarOrientacaoPlanoEstudoItem(s.GeraOrientacao.GetValueOrDefault(), s.SeqTipoOrientacao.GetValueOrDefault(), s.TipoParticipacaoOrientacao.GetValueOrDefault(), orientacaoBase),
                   }).ToList());

            //Aproveita o sequencial da orientação do item no plano de estudo antigo
            listaItensOrientacao.AddRange(itensSolicitacao.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && w.PertencePlanoEstudo.GetValueOrDefault() == true
                   || (w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.NaoAlterado && seqsConfiguracaoComponentePlanoAtual.Contains(w.SeqConfiguracaoComponente.GetValueOrDefault())))
            .Select(s => new PlanoEstudoItem()
            {
                SeqDivisaoTurma = s.SeqDivisaoTurma,
                SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                SeqOrientacao = itensPlanoAtual.FirstOrDefault(f => f.SeqDivisaoTurma == s.SeqDivisaoTurma && f.SeqConfiguracaoComponente == s.SeqConfiguracaoComponente)?.SeqOrientacao,
            }).ToList());

            //6.1.4. Se NÃO pertence ao plano de estudo e a situação selecionada é uma situação configurada de acordo com a etapa para ser final com a classificação "Finalizada sem sucesso" e existir pelo menos um item, da mesma turma, com a mesma divisão de componente com uma situação configurada de acordo com a etapa para ser final com a classificação "Cancelada".
            listaItensOrientacao.AddRange(itensSolicitacao.Where(w => w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && w.PertencePlanoEstudo.GetValueOrDefault() == true
                   && (w.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado && seqsConfiguracaoComponentePlanoAtual.Contains(w.SeqConfiguracaoComponente.GetValueOrDefault())))
            .Select(s => new PlanoEstudoItem()
            {
                SeqDivisaoTurma = s.SeqDivisaoTurma,
                SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                SeqOrientacao = itensPlanoAtual.FirstOrDefault(f => f.SeqDivisaoTurma == s.SeqDivisaoTurma && f.SeqConfiguracaoComponente == s.SeqConfiguracaoComponente)?.SeqOrientacao,
            }).ToList());

            planoEstudoNovo.Itens = listaItensOrientacao;

            // Se não tem nenhum item no plano, erro
            if (planoEstudoNovo.Itens.Count == 0)
                throw new PlanoEstudoSemItensException(null);
            else
                this.SaveEntity(planoEstudoNovo);

            // Para as turmas de orientação que estão no plano de estudo, atualiza o professor (orientador) na divisção de turma
            var planoItemOrientadores = PlanoEstudoItemDomainService.BuscarPlanoEstudoItemOrientacoesTurmas(planoEstudoNovo.Seq, null);
            var seqsDivisoesColaboradores = planoItemOrientadores.Where(w => w.SeqDivisaoTurma.HasValue)
                                                 .SelectMany(s => s.Orientacao.OrientacoesColaborador.Select(o => new DivisaoTurmaColaboradorVO()
                                                 {
                                                     SeqDivisaoTurma = s.SeqDivisaoTurma.Value,
                                                     SeqColaborador = o.SeqColaborador
                                                 })).ToList();
            DivisaoTurmaColaboradorDomainService.AdicionarProfessorOrientador(seqsDivisoesColaboradores);
            //DivisaoTurmaColaboradorDomainService.ExcluirProfessorOrientadorPorSolicitacaoMatricula(seqSolicitacaoMatricula);
        }

        /// <summary>
        /// Finaliza as orientações do plano de estudo
        /// </summary>
        /// <param name="seqPlano">Sequencial do plano de estudos a ter as orientações finalizadas</param>
        public void FinalizaOrientacoesPlano(long seqPlano, DateTime? dataFimOrientacao = null)
        {
            // Coloca valor padrão para data fim de orientação
            dataFimOrientacao = dataFimOrientacao ?? DateTime.Now;

            // Busca os itens do plano que possuem orientação
            var itensOrientadores = PlanoEstudoItemDomainService.BuscarPlanoEstudoItemOrientacoesTurmas(seqPlano, null);
            var colaboradoresOrientacao = itensOrientadores.SelectMany(s => s.Orientacao.OrientacoesColaborador).ToList();
            foreach (var item in colaboradoresOrientacao)
            {
                // Caso a data de fim seja menor que a data de início, usa a própria data de início para a data fim.
                if (item.DataInicioOrientacao > dataFimOrientacao)
                    item.DataFimOrientacao = item.DataInicioOrientacao;
                else
                    item.DataFimOrientacao = dataFimOrientacao;

                OrientacaoColaboradorDomainService.UpdateFields(item, x => x.DataFimOrientacao);
            }
        }

        /// <summary>
        /// Finaliza as orientações do plano de estudo
        /// </summary>
        /// <param name="seqPlano">Sequencial do plano de estudos a ter as orientações finalizadas</param>
        public void FinalizaOrientacoesPlanoItens(List<long> seqsPlanoEstudoItem, long seqPlanoEstudoAtual)
        {
            if (seqsPlanoEstudoItem != null && seqsPlanoEstudoItem.Count > 0)
            {
                // Busca os itens do plano que possuem orientação
                var spec = new PlanoEstudoItemFilterSpecification()
                {
                    SeqsPlanoEstudoItem = seqsPlanoEstudoItem,
                    OrientacaoTurma = true,
                    PlanoEstudoAtual = true,
                    SomenteTurma = true
                };

                // Armazena todas as divisões de turma que tiveram orientações canceladas
                List<long> seqsDivisoesOrientacoesEncerradas = new List<long>();

                var itensOrientadores = PlanoEstudoItemDomainService.SearchBySpecification(spec, IncludesPlanoEstudoItem.Orientacao | IncludesPlanoEstudoItem.Orientacao_OrientacoesColaborador).ToList();
                seqsDivisoesOrientacoesEncerradas.AddRange(itensOrientadores.Where(i => i.SeqDivisaoTurma.HasValue).Select(i => i.SeqDivisaoTurma.Value));

                var colaboradoresOrientacao = itensOrientadores.SelectMany(s => s.Orientacao.OrientacoesColaborador).ToList();
                foreach (var item in colaboradoresOrientacao)
                {
                    item.DataFimOrientacao = item.DataInicioOrientacao.HasValue && DateTime.Now < item.DataInicioOrientacao ? item.DataInicioOrientacao : DateTime.Now;
                    OrientacaoColaboradorDomainService.UpdateFields(item, x => x.DataFimOrientacao);
                }

                // Exclui as associações do colaborador caso o mesmo não tenha mais orientações para a turma
                if (seqsDivisoesOrientacoesEncerradas.Any())
                    DivisaoTurmaColaboradorDomainService.ExcluirProfessorOrientador(seqsDivisoesOrientacoesEncerradas, seqPlanoEstudoAtual);
            }
        }

        /// <summary>
        /// Buscar o ciclo letivo e o curso oferta localidade turno do plano de estudo
        /// </summary>
        /// <param name="seq">Sequencial do plano de estudo</param>
        /// <returns>Retorno o sequencial do ciclo letivo e do curso oferta localidade turno</returns>
        public (long SeqCicloLetivo, long SeqCursoOfertaLocalidadeTurno) BuscarCicloLetivoLocalidadeTurnoPlanoEstudo(long seq)
        {
            var registro = this.SearchProjectionByKey(new SMCSeqSpecification<PlanoEstudo>(seq), p => new
            {
                SeqCicloLetivo = p.AlunoHistoricoCicloLetivo.SeqCicloLetivo,
                SeqCursoOfertaLocalidadeTurno = p.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqCursoOfertaLocalidadeTurno
            });

            return (registro.SeqCicloLetivo, registro.SeqCursoOfertaLocalidadeTurno.GetValueOrDefault());
        }

        /// <summary>
        /// Exclui uma turma de um plano de estudos. Utilizado quando a turma é cancelada.
        /// Ações a serem executadas
        /// 1) Setar o valor "Não" para o indicador de atual no plano que terá a turma excluída
        /// 2) Verificar se existe orientação associada ao item do plano de estudo da turma que será excluida
        /// Caso exista, setar no campo data fim de todos os colaboradores associados à orientação com a
        /// data/hora correntes.
        /// 3) Criar um novo registro de plano de estudo para o histórico do aluno no ciclo letivo em questão,
        /// replicando os dados do plano e o indicador de atual com valor "Sim". Replicar também todos os
        /// itens, exceto o item referente a turma que está sendo cancelada. No campo "Observação" do plano
        /// de estudo, incluir o seguinte texto: "Plano de estudo criado através do cancelamento da turma
        /// <descrição da turma>." Onde, <descrição da turma> recebe a descrição da turma referente à
        /// configuração do item de plano de estudo da turma que está sendo excluída.
        /// </summary>
        /// <param name="seqPlano">Sequencial do plano que deve ter a turma excluída</param>
        /// <param name="seqsDivisaoTurma">Sequenciais das divisões de turma que devem ser excluidas do plano</param>
        public void ExcluirTurmaDoPlano(long seqPlano, List<long> seqsDivisaoTurma)
        {
            // Busca o plano de estudo que será alterado
            var specPlano = new SMCSeqSpecification<PlanoEstudo>(seqPlano);
            var plano = this.SearchByKey(specPlano, IncludesPlanoEstudo.Itens | IncludesPlanoEstudo.Itens_ConfiguracaoComponente);
            if (plano != null && plano.Atual)
            {
                // Busca a descrição da turma sendo excluída
                string descricaoTurma = string.Empty;

                // Inicia transação
                using (ISMCUnitOfWork transacao = SMCUnitOfWork.Begin())
                {
                    // Guarda os itens que tem orientação para encerrar
                    var itensOrientacao = new List<long>();

                    // Cria um novo plano replica do anterior
                    var novoPlano = new PlanoEstudo()
                    {
                        SeqAlunoHistoricoCicloLetivo = plano.SeqAlunoHistoricoCicloLetivo,
                        SeqMatrizCurricularOferta = plano.SeqMatrizCurricularOferta,
                        SeqSolicitacaoServico = null,
                        Atual = true,
                        Itens = new List<PlanoEstudoItem>()
                    };
                    foreach (var item in plano.Itens)
                    {
                        // Verifica se o item é um dos que deve ser excluído
                        if (item.SeqDivisaoTurma.HasValue && seqsDivisaoTurma.Contains(item.SeqDivisaoTurma.Value))
                        {
                            // Guarda a descrição da turma sendo excluída
                            if (String.IsNullOrEmpty(descricaoTurma))
                                descricaoTurma = item.ConfiguracaoComponente.Descricao;

                            // Item deve ser excluido, verifica se tem orientação para encerrar.
                            if (item.SeqOrientacao.HasValue)
                                itensOrientacao.Add(item.Seq);
                        }
                        else
                        {
                            // Item não deve ser excluído, coloca no novo plano
                            var novoItem = new PlanoEstudoItem()
                            {
                                SeqDivisaoTurma = item.SeqDivisaoTurma,
                                SeqConfiguracaoComponente = item.SeqConfiguracaoComponente,
                                SeqOrientacao = item.SeqOrientacao
                            };
                            novoPlano.Itens.Add(novoItem);
                        }
                    }
                    novoPlano.Observacao = string.Format("Plano de estudo criado através do cancelamento da turma {0}.", descricaoTurma);
                    this.SaveEntity(novoPlano);

                    // Encerra a orientação dos itens excluidos com orientação.
                    FinalizaOrientacoesPlanoItens(itensOrientacao, seqPlano);

                    // Altera o indicador atual do plano
                    // Deve ser feito depois de finalizar as orientações porque o método verifica se o plano é o atual
                    // para encerrar a orientação.
                    plano.Atual = false;
                    this.UpdateFields<PlanoEstudo>(plano, p => p.Atual);

                    // Finaliza a transação
                    transacao.Commit();
                }
            }
        }

        /// <summary>
        /// AJUSTES NO PLANO DE ESTUDO ATUAL
        ///     - Liberar as vagas ocupadas pelo anuno nas turmas do plano de estudo atual,
        ///       considerando a data do pedido de solicitação CONSIDERANDO A
        ///     - Para cada divisão de turma existente no plano de estudo ATUAL do
        ///       ciclo letivo de acordo com o PARAMETRO RECEBIDO e o evento que se aplica ao aluno de
        ///       acordo com as parametrizações por entidade e nível de ensino, subtrair 1 da qtd_vagas_ocupadas.
        ///     - Finaliza as orientações de turma do plano ATUAL
        ///     - Alterar o indicador de atual do plano de estudos ATUAL para o valor NÃO.
        ///     - Incluir um novo registro de plano de estudos sem item, de acordo com RN_MAT_112 -Inclusão plano
        ///       de estudo sem item *, passando como parâmetro o ciclo letivo em questão do plano ATUAL,
        ///       a oferta de matriz do plano de estudo em questão, caso exista e a solicitação de serviço, caso exista.
        /// </summary>
        public void AjustarPlanoEstudo(AjustePlanoEstudoVO modelo)
        {
            var specPlano = new PlanoEstudoFilterSpecification()
            {
                SeqAluno = modelo.SeqPessoaAtuacao,
                Atual = true,
                SeqCicloLetivo = modelo.SeqCicloLetivoReferencia
            };
            var planoEstudo = this.SearchByKey(specPlano, IncludesPlanoEstudo.Itens);
            if (planoEstudo != null)
            {
                // Para cada item do plano, libera a vaga.
                foreach (var item in planoEstudo.Itens.Where(i => i.SeqDivisaoTurma.HasValue))
                    PlanoEstudoItemDomainService.LiberarVagaPlanoEstudoItem(item.Seq);

                // Finaliza as orientações de turma do plano atual
                this.FinalizaOrientacoesPlano(planoEstudo.Seq, modelo.DataFimOrientacao);

                // Altera o indicador de atual para false
                planoEstudo.Atual = false;
                this.UpdateFields(planoEstudo, x => x.Atual);

                // Inclui novo plano de estudos atual sem item
                PlanoEstudo planoSemItem = new PlanoEstudo()
                {
                    SeqAlunoHistoricoCicloLetivo = planoEstudo.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = planoEstudo.SeqMatrizCurricularOferta,
                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServico,
                    Atual = modelo.Atual ?? true,
                    Observacao = modelo.Observacao
                };
                this.InsertEntity(planoSemItem);
            }
        }

        public void CriarNovoPlanoEstudos(long seqAluno, long seqPlanoEstudo, int codTurma, string descricaoCicloLetivoInicio, string descricaoFormatada, long seqComponenteCurricular, long? seqComponenteCurricularAssunto)
        {
            // Recupera qual o plano de estudo do aluno no ciclo letivo
            var specPlanoAtual = new PlanoEstudoFilterSpecification()
            {
                SeqAluno = seqAluno,
                Seq = seqPlanoEstudo,
                Atual = true
            };
            specPlanoAtual.SetOrderByDescending(x => x.Seq);
            var planoAtual = this.SearchProjectionByKey(specPlanoAtual, x => new
            {
                Seq = x.Seq,
                SeqAlunoHistoricoCicloLetivo = x.SeqAlunoHistoricoCicloLetivo,
                DescricaoCicloLetivo = x.AlunoHistoricoCicloLetivo.CicloLetivo.Descricao,
                x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Pessoa.SeqInstituicaoEnsino,
                x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.Nome,
                x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.NomeSocial,
                SeqEntidadeVinculo = x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Historicos.FirstOrDefault(h => h.Atual).SeqEntidadeVinculo,
                Emails = x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(d => d.EnderecoEletronico.Descricao).ToList(),
                SeqMatrizCurricularOferta = x.SeqMatrizCurricularOferta,
                SeqComponenteCurricularPadrao = x.MatrizCurricularOferta.MatrizCurricular.SeqComponenteCurricularPadrao,
                Itens = x.Itens.Select(i => new
                {
                    i.Seq,
                    i.SeqConfiguracaoComponente,
                    i.ConfiguracaoComponente.SeqComponenteCurricular,
                    i.DivisaoTurma.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).RestricoesTurmaMatriz.FirstOrDefault(f => f.OfertaMatrizPrincipal).SeqComponenteCurricularAssunto,
                    emailsColaborador = i.DivisaoTurma.Colaboradores.SelectMany(sm => sm.Colaborador.EnderecosEletronicos.Select(s => s.EnderecoEletronico.Descricao)).ToList(),
                    i.SeqDivisaoTurma,
                    DadosOrientacao = new
                    {
                        i.SeqOrientacao,
                        Colaboradores = i.Orientacao.OrientacoesColaborador.Select(oc => new
                        {
                            SeqOrientacaoColaborador = oc.Seq,
                            oc.SeqColaborador,
                            oc.SeqInstituicaoExterna,
                            DataInicioOrientacao = oc.DataInicioOrientacao,
                            oc.DataFimOrientacao
                        })
                    },
                }),
                TipoAluno = x.AlunoHistoricoCicloLetivo.TipoAluno
            });

            UpdateFields(new PlanoEstudo { Seq = planoAtual.Seq, Atual = false }, x => x.Atual);

            // Cria o novo plano de estudos copiando os dados do plano anterior
            var planoEstudoNovo = new PlanoEstudo
            {
                Atual = true,
                SeqAlunoHistoricoCicloLetivo = planoAtual.SeqAlunoHistoricoCicloLetivo,
                SeqMatrizCurricularOferta = planoAtual.SeqMatrizCurricularOferta,
                Observacao = "Removido item aprovado na turma: " + codTurma
            };

            var planoEstudoItem = planoAtual.Itens.Where(w => w.SeqComponenteCurricular == seqComponenteCurricular && w.SeqComponenteCurricularAssunto == seqComponenteCurricularAssunto).ToList();

            // Inicia a lista de itens do plano de estudo novo
            planoEstudoNovo.Itens = planoAtual.Itens.Where(w => !planoEstudoItem.Any(a => a.SeqComponenteCurricular == w.SeqComponenteCurricular &&
                                                                                          a.SeqComponenteCurricularAssunto == w.SeqComponenteCurricularAssunto))
                                                    .Select(s => new PlanoEstudoItem()
                                                    {
                                                        SeqConfiguracaoComponente = s.SeqConfiguracaoComponente,
                                                        SeqDivisaoTurma = s.SeqDivisaoTurma,
                                                        SeqOrientacao = s.DadosOrientacao.SeqOrientacao
                                                    }).ToList();

            if (planoEstudoNovo.Itens == null || !planoEstudoNovo.Itens.Any())
            {
                planoEstudoNovo.Itens = new List<PlanoEstudoItem>();

                var planoEstudoItemPadrao = new PlanoEstudoItem() { SeqConfiguracaoComponente = planoAtual.SeqComponenteCurricularPadrao };
                planoEstudoNovo.Itens.Add(planoEstudoItemPadrao);
            }

            //Atualizar as vagas para a divisão de turma
            //Levando em consideração o componente curricular e o assunto

            var dataFimOrientacao = DateTime.Now;
            foreach (var item in planoEstudoItem)
            {
                PlanoEstudoItemDomainService.LiberarVagaPlanoEstudoItem(item.Seq);

                if (item.DadosOrientacao.SeqOrientacao.HasValue)
                {
                    foreach (var colaborador in item.DadosOrientacao.Colaboradores)
                    {
                        var orientacaoColaborador = OrientacaoColaboradorDomainService.SearchByKey(colaborador.SeqOrientacaoColaborador);
                        orientacaoColaborador.DataFimOrientacao = dataFimOrientacao;
                        OrientacaoColaboradorDomainService.UpdateFields(orientacaoColaborador, x => x.DataFimOrientacao);

                        var specPlanoEstudoItem = new PlanoEstudoItemFilterSpecification() { SeqDivisaoTurma = item.SeqDivisaoTurma, SeqColaborador = colaborador.SeqColaborador };
                        var planosEstudoItem = PlanoEstudoItemDomainService.SearchBySpecification(specPlanoEstudoItem).ToList();

                        if (planosEstudoItem != null && planosEstudoItem.Count() == 1)
                        {
                            var specDivisaoTurmaColaborador = new DivisaoTurmaColaboradorFilterSpecification() { SeqDivisaoTurma = item.SeqDivisaoTurma, SeqColaborador = colaborador.SeqColaborador };
                            var divisaoTurmaColaborador = DivisaoTurmaColaboradorDomainService.SearchByKey(specDivisaoTurmaColaborador);
                            if (divisaoTurmaColaborador != null)
                                DivisaoTurmaColaboradorDomainService.DeleteEntity(divisaoTurmaColaborador);
                        }
                    }
                }
            }
            SaveEntity(planoEstudoNovo);

            // Cria dicionário para tags de envio de notificação
            var dadosMerge = new Dictionary<string, string>
                {
                    { TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(planoAtual.Nome) ? planoAtual.Nome : planoAtual.NomeSocial },
                    { TOKEN_TAG_NOTIFICACAO.CICLO_LETIVO_MATRICULADO, planoAtual.DescricaoCicloLetivo},
                    { TOKEN_TAG_NOTIFICACAO.CICLO_LETIVO_CONCLUIDO, descricaoCicloLetivoInicio },
                    { TOKEN_TAG_NOTIFICACAO.SITUACAO_HE, "Aprovado" },
                    { TOKEN_TAG_NOTIFICACAO.TURMA, descricaoFormatada }
                };

            // Envia a notificação para o professor/secretaria
            NotificarAlteracaoPlano(planoAtual.SeqInstituicaoEnsino, TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PLANO_DISCIPLINA_CONCLUIDA_PROF_SEC, dadosMerge, planoEstudoItem.FirstOrDefault()?.emailsColaborador);

            // Envia a notificação para o aluno
            NotificarAlteracaoPlano(planoAtual.SeqInstituicaoEnsino, TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PLANO_DISCIPLINA_CONCLUIDA_ALUNO, dadosMerge, planoAtual.Emails);
        }

        private void NotificarAlteracaoPlano(long seqEntidade, string tokenNotificacao, Dictionary<string, string> dadosMerge, List<string> emails)
        {
            // Busca a configuração a ser enviada
            var seqConfigNotificacao = EntidadeConfiguracaoNotificacaoDomainService.BuscarSeqConfiguracaoNotificacaoAtivo(seqEntidade, tokenNotificacao);

            // Caso tenha configuração de notificação, prosseguir com o envio
            if (seqConfigNotificacao > 0)
            {
                // Monta o Data para o serviço de notificação
                var data = new NotificacaoEmailData()
                {
                    SeqConfiguracaoNotificacao = seqConfigNotificacao,
                    DataPrevistaEnvio = DateTime.Now,
                    DadosMerge = dadosMerge,
                    PrioridadeEnvio = PrioridadeEnvio.QuandoPossivel,
                    Destinatarios = new List<NotificacaoEmailDestinatarioData>() { new NotificacaoEmailDestinatarioData() { EmailDestinatario = string.Join(";", emails) } }
                };

                // Chama o serviço de envio de notificação
                this.NotificacaoService.SalvarNotificacao(data);
            }
        }

        /// <summary>
        /// Realiza a regra RN_APR_047 - Alteração de plano com matrícula em outro ciclo letivo
        /// </summary>
        /// <param name="listaAlteracao">Lista de planos de estudo para serem alterados</param>
        /// <param name="seqSolicitacaoServico">Solicitação de serviço que deu origem a alteração (opcional)</param>
        /// <param name="justificativaAlteracao">Justificativa da alteração</param>
        /// <param name="descricaoCicloLetivoHistorico">Descrição do ciclo letivo que gerou o histórico da alteração de plano</param>
        /// <param name="situacaoHistorico">Situação do histórico que gerou a alteração de plano</param>
        public void RemoverItensPlanoEstudoAtual(List<PlanoEstudoAlterarVO> listaAlteracao, long? seqSolicitacaoServico, string justificativaAlteracao, string descricaoCicloLetivoHistorico, string situacaoHistorico)
        {
            // Para cada plano a ser alterado
            foreach (var seqPlano in listaAlteracao.Select(l => l.SeqPlanoEstudo).Distinct())
            {
                // Busca os dados do plano atual
                var specPlanoAtual = new PlanoEstudoFilterSpecification() { Seq = seqPlano };
                var planoAtual = this.SearchProjectionByKey(specPlanoAtual, x => new
                {
                    Seq = x.Seq,
                    SeqAlunoHistoricoCicloLetivo = x.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = x.SeqMatrizCurricularOferta,
                    SeqConfiguracaoComponentePadrao = x.MatrizCurricularOferta.MatrizCurricular.ComponenteCurricularPadrao.Configuracoes.FirstOrDefault().Seq,
                    Itens = x.Itens.Select(i => new
                    {
                        i.Seq,
                        i.SeqConfiguracaoComponente,
                        i.SeqDivisaoTurma,
                        i.SeqOrientacao,
                        EmailsProfessoresTurma = i.DivisaoTurma.Colaboradores.SelectMany(sm => sm.Colaborador.EnderecosEletronicos.Where(e => e.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(s => s.EnderecoEletronico.Descricao)).ToList()
                    }),
                    SeqAluno = x.AlunoHistoricoCicloLetivo.AlunoHistorico.SeqAluno,
                    NomeSocialAluno = x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.NomeSocial,
                    NomeAluno = x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.DadosPessoais.Nome,
                    EmailsAluno = x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.EnderecosEletronicos.Where(w => w.EnderecoEletronico.TipoEnderecoEletronico == TipoEnderecoEletronico.Email).Select(d => d.EnderecoEletronico.Descricao).ToList(),
                    DescricaoCicloLetivo = x.AlunoHistoricoCicloLetivo.CicloLetivo.Descricao,
                    EmailEntidadeVinculoAluno = x.AlunoHistoricoCicloLetivo.AlunoHistorico.EntidadeVinculo.EnderecosEletronicos.Where(t => t.TipoEnderecoEletronico == TipoEnderecoEletronico.Email && (t.CategoriaEnderecoEletronico == null || t.CategoriaEnderecoEletronico == CategoriaEnderecoEletronico.Secretaria)).Select(e => e.Descricao).ToList(),
                    SeqInstituicaoEnsino = x.AlunoHistoricoCicloLetivo.AlunoHistorico.Aluno.Pessoa.SeqInstituicaoEnsino
                });

                // Cria o novo plano de estudos copiando os dados do plano anterior
                var novoPlanoEnsino = new PlanoEstudo()
                {
                    SeqAlunoHistoricoCicloLetivo = planoAtual.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = planoAtual.SeqMatrizCurricularOferta,
                    Atual = true,
                    Observacao = justificativaAlteracao,
                    SeqSolicitacaoServico = seqSolicitacaoServico, 
                    Itens = new List<PlanoEstudoItem>()
                };

                // Descrição formatada das turmas que serão excluidas do plano para notificação
                string descricaoFormatadaTurmasExcluidas = string.Empty;

                // Junta o e-mail dos professores dos itens sendo excluidos
                List<string> listaSecProf = new List<string>();
                listaSecProf.AddRange(planoAtual.EmailEntidadeVinculoAluno);

                // Inclui no novo plano os itens do atual retirando os itens que foram aprovados/dispensados
                foreach (var item in planoAtual.Itens)
                {
                    if (!listaAlteracao.Any(i => i.SeqPlanoEstudoItemRemover == item.Seq))
                    {
                        var planoItem = new PlanoEstudoItem() 
                        {
                            SeqConfiguracaoComponente = item.SeqConfiguracaoComponente,
                            SeqDivisaoTurma = item.SeqDivisaoTurma,
                            SeqOrientacao = item.SeqOrientacao
                        };
                        novoPlanoEnsino.Itens.Add(planoItem);
                    }
                    else
                    {
                        // Se o item está sendo excluido do plano, liberar a vaga se é componente de turma
                        PlanoEstudoItemDomainService.LiberarVagaPlanoEstudoItem(item.Seq);

                        // Se é um item que tem orientação, finalizar a mesma e verificar se precisa retirar o professor da divisão de turma
                        if (item.SeqOrientacao.HasValue)
                            this.FinalizaOrientacoesPlanoItens(new List<long>() { item.Seq }, seqPlano);

                        // Guarda a descrição formatada do item excluido
                        if (!string.IsNullOrEmpty(descricaoFormatadaTurmasExcluidas))
                            descricaoFormatadaTurmasExcluidas += "<br />";
                        descricaoFormatadaTurmasExcluidas += DivisaoTurmaDomainService.ObterDescricaoDivisaoTurma(item.SeqDivisaoTurma.Value, planoAtual.SeqAluno, true);

                        // Guarda o e-mail dos professores das turmas excluidas
                        listaSecProf.AddRange(item.EmailsProfessoresTurma);
                    }
                }

                // Se apos a inclusão dos itens que não foram alterados o plano ficou sem itens, inclui o padrão da matriz
                if (novoPlanoEnsino.Itens.Count() == 0)
                {
                    var planoItem = new PlanoEstudoItem()
                    {
                        SeqConfiguracaoComponente = planoAtual.SeqConfiguracaoComponentePadrao
                    };
                    novoPlanoEnsino.Itens.Add(planoItem);
                }

                // Informa que o plano atual não é mais o atual
                UpdateFields(new PlanoEstudo { Seq = planoAtual.Seq, Atual = false }, x => x.Atual);

                // Salva o plano novo
                SaveEntity(novoPlanoEnsino);

                // Enviar notificação para o aluno, para o professor da turma excluída e para a secretaria informando da
                // alteração de plano, conforme regra  RN_APR_046 - Notificação - Alteração automática de plano de estudo

                // Cria dicionário para tags de envio de notificação
                var dadosMerge = new Dictionary<string, string>
                {
                    // {{NOM_PESSOA}}' - Nome do aluno (nome social ou nome da pessoa_atuacao)
                    { TOKEN_TAG_NOTIFICACAO.NOM_PESSOA, string.IsNullOrEmpty(planoAtual.NomeSocialAluno) ? planoAtual.NomeAluno : planoAtual.NomeSocialAluno },

                    // '{{CICLO_LETIVO_MATRICULADO}}' - ciclo letivo do plano de estudo que será alterado
                    { TOKEN_TAG_NOTIFICACAO.CICLO_LETIVO_MATRICULADO,  planoAtual.DescricaoCicloLetivo},

                    // '{{CICLO_LETIVO_CONCLUIDO}}' - ciclo letivo da turma que está sendo aprovado/dispensado
                    { TOKEN_TAG_NOTIFICACAO.CICLO_LETIVO_CONCLUIDO, descricaoCicloLetivoHistorico },

                    // '{{SITUACAO_HE}}') -Para fechamento de diário texto "aprovado", para solicitação de dispensa texto "Dispensa"
                    { TOKEN_TAG_NOTIFICACAO.SITUACAO_HE, situacaoHistorico },

                    // '{{TURMA}}' - turma que está sendo excluída do plano, usar o formato: < cod_turma > - [Descrição da configuração do componente] +":" + [Descrição da configuração do componente substituto]
                    // *< cod_turma > = [Código da Turma] + "." + [Número da Turma] + "." + [Número da Divisão da Configuração do Componente] +"." + [Número do Grupo]
                    { TOKEN_TAG_NOTIFICACAO.TURMA, descricaoFormatadaTurmasExcluidas }
                };

                // Enviar a notificação correspondente ao tipo ALTERACAO_PLANO_DISCIPLINA_CONCLUIDA_ALUNO para o aluno
                if (planoAtual.EmailsAluno.Count > 0)
                {
                    var tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PLANO_DISCIPLINA_CONCLUIDA_ALUNO;

                    // Se foi informada uma solicitação, realiza o envio da notificação considerando a configuração do processo
                    if (seqSolicitacaoServico.HasValue)
                    {
                        var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico.Value,
                            TokenNotificacao = tokenNotificacao,
                            DadosMerge = dadosMerge,
                            EnvioSolicitante = true,
                            ConfiguracaoPrimeiraEtapa = false
                        };
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                    }
                    else 
                        NotificarAlteracaoPlano(planoAtual.SeqInstituicaoEnsino, tokenNotificacao, dadosMerge, planoAtual.EmailsAluno);
                }

                // Envia a notificação para o professor/secretaria
                if (listaSecProf.Count > 0)
                {
                    var tokenNotificacao = TOKEN_TIPO_NOTIFICACAO.ALTERACAO_PLANO_DISCIPLINA_CONCLUIDA_PROF_SEC;

                    if (seqSolicitacaoServico.HasValue)
                    {
                        var parametros = new EnvioNotificacaoSolicitacaoServicoVO()
                        {
                            SeqSolicitacaoServico = seqSolicitacaoServico.Value,
                            TokenNotificacao = tokenNotificacao,
                            DadosMerge = dadosMerge,
                            EnvioSolicitante = false,
                            ConfiguracaoPrimeiraEtapa = false,
                            Destinatarios = listaSecProf
                        };
                        SolicitacaoServicoEnvioNotificacaoDomainService.EnviarNotificacaoSolicitacaoServico(parametros);
                    }
                    else
                        NotificarAlteracaoPlano(planoAtual.SeqInstituicaoEnsino, tokenNotificacao, dadosMerge, listaSecProf);
                }
            }
        }

        public void IncluirNovoPlanoEstudoSemItem(long seqCicloLetivo, string observacao, long seqPessoaAtuacao)
        {
            var ajustePlanoEstudo = new AjustePlanoEstudoVO()
            {
                SeqCicloLetivoReferencia = seqCicloLetivo,
                Observacao = observacao,
                SeqPessoaAtuacao = seqPessoaAtuacao
            };

            this.AjustarPlanoEstudo(ajustePlanoEstudo);
        }

        public void AjustarPlanoEstudoIntercambio(AjustePlanoEstudoVO modelo)
        {
            var specPlano = new PlanoEstudoFilterSpecification()
            {
                SeqAluno = modelo.SeqPessoaAtuacao,
                Atual = true,
                SeqCicloLetivo = modelo.SeqCicloLetivoReferencia
            };
            var planoEstudo = this.SearchByKey(specPlano);
            if (planoEstudo != null)
            {
                // Finaliza as orientações de turma do plano atual
                this.FinalizaOrientacoesPlano(planoEstudo.Seq, modelo.DataFimOrientacao);

                // Altera o indicador de atual para false
                planoEstudo.Atual = false;
                this.UpdateFields(planoEstudo, x => x.Atual);

                // Inclui novo plano de estudos atual sem item
                PlanoEstudo planoSemItem = new PlanoEstudo()
                {
                    SeqAlunoHistoricoCicloLetivo = planoEstudo.SeqAlunoHistoricoCicloLetivo,
                    SeqMatrizCurricularOferta = planoEstudo.SeqMatrizCurricularOferta,
                    SeqSolicitacaoServico = modelo.SeqSolicitacaoServico,
                    Atual = modelo.Atual ?? true,
                    Observacao = modelo.Observacao
                };
                this.InsertEntity(planoSemItem);
            }
        }
    }
}