using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Exceptions.Aula;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.FIN.Specifications;
using SMC.Academico.Domain.Areas.GRD.DomainServices;
using SMC.Academico.Domain.Areas.GRD.Models;
using SMC.Academico.Domain.Areas.ORG.DomainServices;
using SMC.Academico.Domain.Areas.PES.DomainServices;
using SMC.Academico.Domain.Areas.PES.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Calendarios.Common.Areas.CLD.Enums;
using SMC.Calendarios.ServiceContract.Areas.CLD.Data;
using SMC.Calendarios.ServiceContract.Areas.CLD.Interfaces;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class ApuracaoFrequenciaGradeDomainService : AcademicoContextDomain<ApuracaoFrequenciaGrade>
    {
        #region [ Domain ]

        private OrigemAvaliacaoDomainService OrigemAvaliacaoDomainService => Create<OrigemAvaliacaoDomainService>();
        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();
        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();
        private EventoAulaDomainService EventoAulaDomainService => Create<EventoAulaDomainService>();
        private InstituicaoNivelDomainService InstituicaoNivelDomainService => Create<InstituicaoNivelDomainService>();
        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();
        private MensagemPessoaAtuacaoDomainService MensagemPessoaAtuacaoDomainService => Create<MensagemPessoaAtuacaoDomainService>();
        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();
        private PessoaAtuacaoDomainService PessoaAtuacaoDomainService => Create<PessoaAtuacaoDomainService>();
        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        #endregion [ Domain ]

        #region [ Service ]

        private ITabelaHorarioService TabelaHorarioService => Create<ITabelaHorarioService>();

        #endregion [ Service ]

        public LancamentoFrequenciaVO BuscarLancamentoFrequencia(long seqOrigemAvaliacao)
        {
            var lancamentoFrequencia = new LancamentoFrequenciaVO()
            {
                SeqOrigemAvaliacao = seqOrigemAvaliacao,
                DataLimite = DateTime.Today.AddDays(-7)
            };
            long seqOrigemAvaliacaoTurma;
            long seqTurma;
            long? seqDivisaoTurma = null;
            var specDivisaoTurma = new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao };
            var dadosOrigemAvaliacaoTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisaoTurma, p => new
            {
                p.Seq,
                p.SeqTurma,
                p.Turma.SeqOrigemAvaliacao,
                p.OrigemAvaliacao.Descricao,
                p.DivisaoComponente.CargaHorariaGrade,
                p.Turma.Colaboradores
            }).FirstOrDefault();
            seqTurma = dadosOrigemAvaliacaoTurma.SeqTurma;
            seqDivisaoTurma = dadosOrigemAvaliacaoTurma.Seq;
            seqOrigemAvaliacaoTurma = dadosOrigemAvaliacaoTurma.SeqOrigemAvaliacao;

            var instituicaoNivel = InstituicaoNivelDomainService.BuscarInstituicaoNivelPorTurma(seqTurma);
            lancamentoFrequencia.QuantidadeDiasPrazoApuracaoFrequencia = instituicaoNivel.QuantidadeDiasPrazoApuracaoFrequencia;
            lancamentoFrequencia.QuantidadeMinutosPrazoAlteracaoFrequencia = instituicaoNivel.QuantidadeMinutosPrazoAlteracaoFrequencia;
            lancamentoFrequencia.CargaHoraria = dadosOrigemAvaliacaoTurma.CargaHorariaGrade.GetValueOrDefault();

            var descricaoOrigemAvaliacao = dadosOrigemAvaliacaoTurma.Descricao;
            if (string.IsNullOrEmpty(descricaoOrigemAvaliacao))
            {
                descricaoOrigemAvaliacao = OrigemAvaliacaoDomainService.BuscarDescricaoOrigemAvaliacao(seqOrigemAvaliacao);
            }
            lancamentoFrequencia.DescricaoOrigemAvaliacao = descricaoOrigemAvaliacao;

            var alunosDiario = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, seqDivisaoTurma, null, null);
            lancamentoFrequencia.Alunos = alunosDiario.Select(s => new LancamentoFrequenciaAlunoVO()
            {
                SeqAlunoHistoricoCicloLetivo = s.SeqAlunoHistoricoCicloLetivo,
                NumeroRegistroAcademico = s.NumeroRegistroAcademico,
                Nome = s.NomeAluno, // Já implementa a RN_PES_037 - Nome e Nome Social - Visão Aluno
                Apuracoes = new List<ApuracaoFrequenciaGradeVO>(),
                SeqAluno = s.SeqPessoaAtuacao,
                AlunoFormado = s.AlunoFormado,
                AlunoHistoricoEscolar = s.SeqHistoricoEscolar.HasValue
            }).ToList();

            List<MensagemListaVO> mensagensAluno = MensagemPessoaAtuacaoDomainService.BuscarMensagensPorCategoria(lancamentoFrequencia.Alunos.Select(s => s.SeqAluno).ToList(), CategoriaMensagem.Ocorrencia);
            AtulizarFrequenciaPorMensagem(mensagensAluno);

            var specEventosAula = new EventoAulaFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma, DataFutura = false };
            specEventosAula.SetOrderBy(o => o.Data);
            specEventosAula.SetOrderBy(o => o.HoraInicio);
            //Valida se o professor e o principal da turma, pois caso seja ele verá todas as aulas
            //Caso contrario será filtrada somente as aulas deste professor
            ColaboradorFilterSpecification colaboradorFilterSpecification = new ColaboradorFilterSpecification() { SeqUsuarioSAS = (long)SMCContext.User.SMCGetSequencialUsuario() };
            long colaboradorLogado = (long)ColaboradorDomainService.SearchProjectionBySpecification(colaboradorFilterSpecification, p => p.Seq).FirstOrDefault();
            if (!dadosOrigemAvaliacaoTurma.Colaboradores.Any(a => a.SeqColaborador == colaboradorLogado))
            {
                specEventosAula.SeqsColaborador = new List<long>() { colaboradorLogado };
            }

            int aula = 1;
            lancamentoFrequencia.Aulas = EventoAulaDomainService.SearchProjectionBySpecification(specEventosAula, p => new
            {
                p.Seq,
                p.Data,
                p.HoraInicio,
                p.HoraFim,
                p.SituacaoApuracaoFrequencia,
                p.Turno,
                p.DataPrimeiraApuracaoFrequencia,
                p.DataLimiteApuracaoFrequencia,
                p.UsuarioPrimeiraApuracaoFrequencia,
            }).ToList()
            .Select(s => new LancamentoFrequenciaAulaVO()
            {
                SeqEventoAula = s.Seq,
                DescricaoFormatada = $"A{aula} - {s.HoraInicio:hh\\:mm} às {s.HoraFim:hh\\:mm}",
                Sigla = $"A{aula++}",
                SituacaoApuracaoFrequencia = s.SituacaoApuracaoFrequencia,
                Data = s.Data,
                Turno = s.Turno,
                HoraInicio = s.HoraInicio,
                HoraFim = s.HoraFim,
                DataPrimeiraApuracaoFrequencia = s.DataPrimeiraApuracaoFrequencia,
                DataLimiteApuracaoFrequencia = s.DataLimiteApuracaoFrequencia,
                UsuarioPrimeiraApuracaoFrequencia = s.UsuarioPrimeiraApuracaoFrequencia,
            }).OrderByDescending(o => o.Data).ThenByDescending(o => o.HoraInicio).ToList();

            //UC_APR_006_02_01.NV05 (utilizar a ch grade para o cálculo de percentual de frequência ou a ch lançada caso seja maior)
            if (lancamentoFrequencia.Aulas.Count > lancamentoFrequencia.CargaHoraria)
            {
                lancamentoFrequencia.CargaHoraria = (short)lancamentoFrequencia.Aulas.Count;
            }

            lancamentoFrequencia.Aulas = FiltrarAulasTurno(lancamentoFrequencia.Aulas);

            var apuracoesSpec = new ApuracaoFrequenciaGradeFilterSpecification() { SeqDivisaoTurma = seqDivisaoTurma };
            var apuracoesAvaliacao = SearchProjectionBySpecification(apuracoesSpec, p => new ApuracaoFrequenciaGradeVO()
            {
                Seq = p.Seq,
                SeqEventoAula = p.SeqEventoAula,
                SeqAlunoHistoricoCicloLetivo = p.SeqAlunoHistoricoCicloLetivo,
                Frequencia = p.Frequencia,
                ObservacaoRetificacao = p.ObservacaoRetificacao,
                SeqMensagem = p.SeqMensagem,
                OcorrenciaFrequencia = p.OcorrenciaFrequencia,
                DataRetificacao = p.DataRetificacao,
                DescricaoTipoMensagem = p.Mensagem.TipoMensagem.Descricao,
                SeqSolicitacao = p.SeqSolicitacaoServico
            }).ToList();
            foreach (var aluno in lancamentoFrequencia.Alunos)
            {
                aluno.Nome = aluno.Nome.Trim().ToUpper();
                aluno.Apuracoes = new List<ApuracaoFrequenciaGradeVO>();
                foreach (var aplicacaoAvaliacao in lancamentoFrequencia.Aulas)
                {
                    var apuracao = apuracoesAvaliacao
                        .FirstOrDefault(f => f.SeqAlunoHistoricoCicloLetivo == aluno.SeqAlunoHistoricoCicloLetivo
                                          && f.SeqEventoAula == aplicacaoAvaliacao.SeqEventoAula) ??
                        new ApuracaoFrequenciaGradeVO()
                        {
                            Seq = 0,
                            SeqEventoAula = aplicacaoAvaliacao.SeqEventoAula,
                            SeqAlunoHistoricoCicloLetivo = aluno.SeqAlunoHistoricoCicloLetivo,
                            Frequencia = null,
                            Observacao = null
                        };
                    AtualizacaoObservacao(apuracao, mensagensAluno);
                    aluno.Apuracoes.Add(apuracao);
                }
            }
            lancamentoFrequencia.Alunos = lancamentoFrequencia.Alunos.OrderBy(o => o.Nome).ToList();
            return lancamentoFrequencia;
        }

        public TimeSpan? BuscarHorarioLimiteTurno(long seqOrigemAvaliacao)
        {
            var specDivisaoTurma = new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao };
            var dadosOrigemAvaliacaoTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(specDivisaoTurma, p => new
            {
                p.Turma.SeqAgendaTurma,
                p.Turma.DataInicioPeriodoLetivo,
                p.Turma.DataFimPeriodoLetivo
            }).FirstOrDefault();

            TabelaHorarioData result = TabelaHorarioService.BuscarTabelaHorarioAgendaTurma(new TabelaHorarioAgendaTurmaFiltroData()
            {
                SeqAgenda = dadosOrigemAvaliacaoTurma.SeqAgendaTurma.GetValueOrDefault(),
                DataInicioPeriodoLetivo = dadosOrigemAvaliacaoTurma.DataInicioPeriodoLetivo,
                DataFimPeriodoLetivo = dadosOrigemAvaliacaoTurma.DataFimPeriodoLetivo
            });

            var diaSemanaAgd = (TipoDiaSemana)Math.Pow(2, (int)DateTime.Today.DayOfWeek);

            var horario = result.Horarios
                .Where(w => w.DiaSemana == diaSemanaAgd)
                .GroupBy(g => g.Turno)
                .Select(s => new
                {
                    SeqTurno = s.Key,
                    HoraInicio = s.Min(m => m.HoraInicio),
                    HoraFim = s.Max(m => m.HoraFim)
                })
                .OrderBy(o => o.HoraInicio)
                .LastOrDefault(l => DateTime.Now.TimeOfDay >= l.HoraInicio)
                ?.HoraFim;

            return horario;
        }

        public void SalvarLancamentoFrequencia(List<ApuracaoFrequenciaGradeVO> data)
        {
            var apuracoes = data.TransformList<ApuracaoFrequenciaGrade>();
            ValidarColisao(apuracoes);

            foreach (var apuracao in apuracoes)
            {
                SaveEntity(apuracao);
            }

            var seqAulas = data.Select(s => s.SeqEventoAula).Distinct().ToList();
            AtualizarAulas(seqAulas);

            AtualizarFrequenciaHistoricoEscolar(apuracoes);
        }

        /// <summary>
        /// Buscas apurações de frequencia
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        /// <param name="inicioPeriodo">Incio do periodo</param>
        /// <param name="fimPeriodo">Fim do periodo</param>
        /// <returns>Todas as apurações de um aluno</returns>
        public virtual List<ApuracaoFrequenciaGradeVO> BuscarApuracaoFrequenciaGradePorAluno(long seqAluno, DateTime? inicioPeriodo, DateTime? fimPeriodo)
        {
            List<long> seqsDvisoesAluno = PlanoEstudoItemDomainService.BuscarTurmasAlunoNoPeriodo(seqAluno, inicioPeriodo.Value, fimPeriodo)
                                                       .Select(s => s.SeqDivisaoTurma).Cast<long>().ToList();

            var spec = new ApuracaoFrequenciaGradeFilterSpecification() { SeqsDivisaoTurma = seqsDvisoesAluno, SeqAluno = seqAluno, InicioPeriodo = inicioPeriodo, FimPeriodo = fimPeriodo };

            List<ApuracaoFrequenciaGradeVO> list = SearchProjectionBySpecification(spec, p => new ApuracaoFrequenciaGradeVO
            {
                Seq = p.Seq,
                SeqEventoAula = p.SeqEventoAula,
                SeqAlunoHistoricoCicloLetivo = p.SeqAlunoHistoricoCicloLetivo,
                Frequencia = p.Frequencia,
                SeqMensagem = p.SeqMensagem,
                DataRetificacao = p.DataRetificacao,
                ObservacaoRetificacao = p.ObservacaoRetificacao,
                OcorrenciaFrequencia = p.OcorrenciaFrequencia,
                DataInclusao = p.DataInclusao
            }).ToList();

            return list;
        }

        /// <summary>
        /// Buscar todas a apurações do Aluno na turma
        /// </summary>
        /// <param name="seqAluno">Seqeuncial do aluno</param>
        /// <param name="seqTurma">Sequencial da turma</param>
        /// <returns>Todas as apurações do aluno na turma</returns>
        public List<ApuracaoFrequenciaGradeVO> BuscarApuracoesFrequenciaTurmaAluno(long seqAluno, long seqTurma)
        {
            var spec = new ApuracaoFrequenciaGradeFilterSpecification() { SeqAluno = seqAluno, SeqTurma = seqTurma };

            var retorno = SearchProjectionBySpecification(spec, p => new ApuracaoFrequenciaGradeVO
            {
                DataInclusao = p.DataInclusao,
                DataRetificacao = p.DataRetificacao,
                Frequencia = p.Frequencia,
                ObservacaoRetificacao = p.ObservacaoRetificacao,
                OcorrenciaFrequencia = p.OcorrenciaFrequencia,
                Seq = p.Seq,
                SeqAlunoHistoricoCicloLetivo = p.SeqAlunoHistoricoCicloLetivo,
                SeqEventoAula = p.SeqEventoAula,
                SeqMensagem = p.SeqMensagem,
                SeqSolicitacao = p.SeqSolicitacaoServico
            }).ToList();

            return retorno;
        }

        private void ValidarColisao(List<ApuracaoFrequenciaGrade> apuracoes)
        {
            var seqsAula = apuracoes
                .Select(s => s.SeqEventoAula)
                .Distinct()
                .ToList();
            var specUsuarios = new EventoAulaFilterSpecification()
            {
                Seqs = seqsAula,
                ComPrimeiraApuracao = true
            };
            var usuariosPrimeiraApuracao = EventoAulaDomainService.SearchProjectionBySpecification(specUsuarios, p => p.UsuarioPrimeiraApuracaoFrequencia, true);
            var usuarioLogado = SMCContext.User.Identity.Name;
            if (usuariosPrimeiraApuracao.Any(a => a != usuarioLogado))
            {
                throw new ApuracaoFrequenciaGradeColisaoProfessorException();
            }
        }

        private void AtualizarAulas(List<long> seqAulas)
        {
            var specAulas = new EventoAulaFilterSpecification() { Seqs = seqAulas };
            var eventoAulas = EventoAulaDomainService.SearchProjectionBySpecification(specAulas, p => new
            {
                p.Seq,
                QuatidadeApuracoes = p.ApuracoesFrequenciaGrade.Count(),
                p.DataPrimeiraApuracaoFrequencia
            }).ToList();
            foreach (var eventoAula in eventoAulas)
            {
                var eventoAulaModel = new EventoAula()
                {
                    Seq = eventoAula.Seq,
                    SituacaoApuracaoFrequencia = eventoAula.QuatidadeApuracoes > 0 ? SituacaoApuracaoFrequencia.Executada : SituacaoApuracaoFrequencia.NaoApurada,
                    DataPrimeiraApuracaoFrequencia = eventoAula.DataPrimeiraApuracaoFrequencia
                };
                if (!eventoAula.DataPrimeiraApuracaoFrequencia.HasValue)
                {
                    eventoAulaModel.DataPrimeiraApuracaoFrequencia = DateTime.Now;
                }
                eventoAulaModel.UsuarioPrimeiraApuracaoFrequencia = SMCContext.User.Identity.Name;
                EventoAulaDomainService.UpdateFields(eventoAulaModel, p => p.SituacaoApuracaoFrequencia,
                                                                      p => p.DataPrimeiraApuracaoFrequencia,
                                                                      p => p.UsuarioPrimeiraApuracaoFrequencia);
            }
        }

        /// <summary>
        /// Atualizar frequencia de eventos criados posteriormente a criação da mensagem
        /// </summary>
        /// <param name="mensagens">Dados da mensagem</param>
        private void AtulizarFrequenciaPorMensagem(List<MensagemListaVO> mensagens)
        {
            foreach (var item in mensagens)
            {
                MensagemVO mensagem = new MensagemVO()
                {
                    DataInicioVigencia = item.DataInicioVigencia,
                    DataFimVigencia = item.DataFimVigencia,
                    SeqMensagem = item.SeqMensagem,
                    SeqPessoaAtuacao = item.SeqPessoaAtuacao,
                    SeqTipoMensagem = item.SeqTipoMensagem
                };
                MensagemPessoaAtuacaoDomainService.AtualizarFrequencias(mensagem, true);
            }
        }

        /// <summary>
        /// Atualiza o campo de observação baseado na retificação ou abono/sanção
        /// </summary>
        /// <param name="apuracao">dados apuracao</param>
        private void AtualizacaoObservacao(ApuracaoFrequenciaGradeVO apuracao, List<MensagemListaVO> mensagens)
        {
            if (apuracao.SeqMensagem != null)
            {
                MensagemListaVO mensagem = mensagens.FirstOrDefault(f => f.SeqMensagem == apuracao.SeqMensagem);
                apuracao.Observacao = mensagem.DescricaoMensagem;
                apuracao.DataObservacao = $"{mensagem.DataInicioVigencia.ToShortDateString()} - {mensagem.DataFimVigencia.Value.ToShortDateString()}";
            }
            else if (apuracao.SeqSolicitacao != null)
            {
                apuracao.Observacao = apuracao.ObservacaoRetificacao;
                apuracao.DataObservacao = apuracao.DataRetificacao.Value.ToShortDateString();
                apuracao.DescricaoTipoMensagem = "Retificação de falta";
            }
        }

        /// <summary>
        /// Filtrar as aulas se estam dentro do turno do dia atual
        /// </summary>
        /// <param name="aulas">Aulas a serem filtradas</param>
        /// <returns>Aulas filtradas</returns>
        private List<LancamentoFrequenciaAulaVO> FiltrarAulasTurno(List<LancamentoFrequenciaAulaVO> aulas)
        {
            //Faz o agrupamento de todas as aulas abaixo do horario atual
            var agurpamentoAulas = aulas.GroupBy(g => g.Turno)
                                    .Select(s => new
                                    {
                                        Turno = s.Key,
                                        HoraInicio = s.Min(m => m.HoraInicio),
                                        HoraFim = s.Max(m => m.HoraFim),
                                    }).ToList();

            //Caso exista somente aulas acima do horario acima do permitirdo
            if (!agurpamentoAulas.SMCAny())
            {
                return new List<LancamentoFrequenciaAulaVO>();
            }

            var horarioMaximo = agurpamentoAulas.Max(m => m.HoraFim);

            List<LancamentoFrequenciaAulaVO> aulasFiltradas = new List<LancamentoFrequenciaAulaVO>();
            //Filtro se o dia é hoje para vermos se no dia de hoje a aula esta dentro do turno permitido
            aulas.ForEach(aula =>
            {
                if (aula.Data < DateTime.Today)
                {
                    aulasFiltradas.Add(aula);
                }
                else
                {
                    if (aula.HoraInicio < horarioMaximo && DateTime.Now.TimeOfDay > aula.HoraInicio)
                    {
                        aulasFiltradas.Add(aula);
                    }
                }
            });

            return aulasFiltradas;
        }

        /// <summary>
        /// Atualizar historico escolar de alunos que tiveram o lançamento de notas alterados, 
        /// caso o mesmo já tenha tido historico fechado
        /// </summary>
        /// <param name="apuracoes">Apurações a serem atualizadas</param>
        private void AtualizarFrequenciaHistoricoEscolar(List<ApuracaoFrequenciaGrade> apuracoes)
        {
            // Como todos os eventos aula são da mesma turma precisa-se de somente um evento para se ter os dados da turma
            long seqEvento = apuracoes.Select(s => s.SeqEventoAula).FirstOrDefault();

            EventoAula eventoAula = EventoAulaDomainService.SearchByKey(seqEvento);
            long seqTurma = DivisaoTurmaDomainService.SearchProjectionByKey(eventoAula.SeqDivisaoTurma, p => p.SeqTurma);
            List<DiarioTurmaAlunoVO> alunosDivisao = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, eventoAula.SeqDivisaoTurma, null, null).ToList();

            alunosDivisao.ForEach(aluno =>
            {
                // Se aluno já tem histórico escolar, atualiza a frequencia e situação
                if (aluno.SeqHistoricoEscolar.HasValue && !aluno.AlunoFormado)
                {
                    HistoricoEscolarCompletoVO historicoEscolarAtual = HistoricoEscolarDomainService.BuscarHistoricoEscolarTurma(seqTurma, aluno.SeqPessoaAtuacao);
                    SituacaoHistoricoEscolar situacaoAlunoAtual = historicoEscolarAtual.SituacaoHistoricoEscolar;

                    // Busca a carga horária 
                    decimal cargaHoraria = HistoricoEscolarDomainService.RecuperarCargaHoraria(aluno.CargaHorariaGrade,
                                                                                               aluno.CargaHoraria.GetValueOrDefault(),
                                                                                               aluno.CargaHorariaExecutada);

                    // Prepara para calcular a nova situação do aluno
                    // Significa que se eu tiver um numero negativo de faltas irei adicionar ao meu total de faltas
                    var alunoHistoricoVO = aluno.Transform<HistoricoEscolarSituacaoFinalVO>();
                    alunoHistoricoVO.CargaHoraria = (short)cargaHoraria;

                    alunoHistoricoVO.Faltas = aluno.SomaFaltasApuracao;

                    // Calcula a situação com a nova frequencia
                    var situacaoCalculada = HistoricoEscolarDomainService.CalcularSituacaoFinal(alunoHistoricoVO);

                    // Calcula no novo percentual de frequencia
                    var percentualFrequenciaCalculada = HistoricoEscolarDomainService.CalcularPercentualFrequencia(cargaHoraria, aluno.SomaFaltasApuracao);

                    // Atualiza o histórico escolar
                    HistoricoEscolar historicoEscolar = new HistoricoEscolar
                    {
                        Seq = aluno.SeqHistoricoEscolar.Value,
                        SituacaoHistoricoEscolar = situacaoCalculada,
                        Faltas = aluno.SomaFaltasApuracao,
                        PercentualFrequencia = percentualFrequenciaCalculada
                    };
                    HistoricoEscolarDomainService.UpdateFields(historicoEscolar, x => x.SituacaoHistoricoEscolar, x => x.Faltas, x => x.PercentualFrequencia);
                }
            });
        }
    }
}