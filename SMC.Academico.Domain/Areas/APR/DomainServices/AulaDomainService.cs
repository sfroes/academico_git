using SMC.Academico.Common.Areas.APR.Exceptions.Aula;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.APR.Specifications;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Mapper;
using SMC.Framework.Specification;
using SMC.Framework.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class AulaDomainService : AcademicoContextDomain<Aula>
    {
        public HistoricoEscolarDomainService HistoricoEscolarDomainService => this.Create<HistoricoEscolarDomainService>();
        public TurmaDomainService TurmaDomainService => this.Create<TurmaDomainService>();
        public DivisaoTurmaDomainService DivisaoTurmaDomainService => this.Create<DivisaoTurmaDomainService>();
        public ApuracaoFrequenciaDomainService ApuracaoFrequenciaDomainService => this.Create<ApuracaoFrequenciaDomainService>();

        /// <summary>
        /// Prepara o modelo de apuração para inclusão de uma nova aula. Considera os alunos matriculados numa divisão.
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão</param>
        /// <param name="agruparAluosCurso">Quando setado os alunos são divididos por curso oferta localidade turno, caso contrário todos alunos são retornados num único “grupo”</param>
        /// <param name="seqsOrientadores">Sequenciais dos orientadores que estão chamando o recurso.</param>
        /// <returns>Lista de alunos para apuração</returns>
        public List<AulaOfertaVO> BuscarAlunosNovaApuracao(long seqDivisaoTurma, bool agruparAluosCurso, List<long> seqsOrientadores)
        {
            // Recupera inicialmente o sequencial da turma de uma divisão
            var seqTurma = DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, x => x.SeqTurma);

            var alunos = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, seqDivisaoTurma, null, seqsOrientadores);
            var ret = new List<AulaOfertaVO>();

            if (alunos != null)
            {
                IOrderedEnumerable<IGrouping<long, DiarioTurmaAlunoVO>> gruposOferta = agruparAluosCurso ?
                    // Grupos ordenados por oferta
                    alunos.GroupBy(g => g.SeqCursoOfertaLocalidadeTurno).OrderBy(o => o.First().DescricaoCursoOfertaLocalidadeTurno) :
                    // Grupo único apenas com a mesma estrutura
                    alunos.GroupBy(g => 0L).OrderBy(o => 0L);

                foreach (var grupoOferta in gruposOferta)
                {
                    var apuracoesFrequencia = new List<ApuracaoFrequenciaVO>();
                    ret.Add(new AulaOfertaVO()
                    {
                        SeqCursoOfertaLocalidadeTurno = grupoOferta.Key,
                        DescricaoCursoOfertaLocalidadeTurno = agruparAluosCurso ? grupoOferta.First().DescricaoCursoOfertaLocalidadeTurno : null,
                        ApuracoesFrequencia = apuracoesFrequencia
                    });

                    foreach (var item in grupoOferta)
                    {
                        short faltas = 0;

                        // Caso não tenha histórico, o número de faltas é o somatório das faltas do aluno
                        if (!item.SeqHistoricoEscolar.HasValue)
                        {
                            // busca o total de faltas já existentes para o aluno
                            faltas = (short)ApuracaoFrequenciaDomainService.SearchProjectionBySpecification(new ApuracaoFrequenciaFilterSpecification
                            {
                                SeqDivisaoTurma = seqDivisaoTurma,
                                SeqAlunoHistoricoCicloLetivo = item.SeqAlunoHistoricoCicloLetivo,
                            }, x => x.NumeroFaltas).ToList().Sum(x => x);
                        }
                        else
                            faltas = item.Faltas ?? item.SomaFaltasApuracao ?? 0;

                        apuracoesFrequencia.Add(new ApuracaoFrequenciaVO
                        {
                            FaltasAtuais = faltas,
                            NomeAluno = item.NomeAluno,
                            NumeroRegistroAcademico = item.NumeroRegistroAcademico,
                            Seq = 0,
                            SeqAlunoHistoricoCicloLetivo = item.SeqAlunoHistoricoCicloLetivo,
                            SeqAula = 0,
                            AlunoComHistorico = item.SeqHistoricoEscolar.HasValue,
                            AlunoFormado = item.AlunoFormado
                        });
                    }
                }
            }

            return ret;
        }

        public void Excluir(long seq)
        {
            using (var transacao = SMCUnitOfWork.Begin())
            {
                // Recupera inicialmente os dados da divisão
                var dadosDivisao = SearchProjectionByKey(new SMCSeqSpecification<Aula>(seq), x => new
                {
                    x.SeqDivisaoTurma,
                    x.DivisaoTurma.SeqTurma,
                });

                // Recupera a aula com as apurações
                var aula = SearchByKey(new SMCSeqSpecification<Aula>(seq), x => x.ApuracoesFrequencia);

                // Recupera os alunos
                var alunos = TurmaDomainService.BuscarDiarioTurmaAluno(dadosDivisao.SeqTurma, dadosDivisao.SeqDivisaoTurma, null, null);

                // Para cada apuração com falta, atualiza o histórico
                foreach (var item in aula.ApuracoesFrequencia.Where(a => a.NumeroFaltas > 0))
                {
                    // Recupera o aluno
                    var aluno = alunos.FirstOrDefault(a => a.SeqAlunoHistoricoCicloLetivo == item.SeqAlunoHistoricoCicloLetivo && a.SeqHistoricoEscolar.HasValue);
                    if (aluno != null)
                    {
                        // Soma o total de faltas já lançadas para este aluno nesta turma, ignorando o item que estou excluindo
                        var totalFaltasAula = ApuracaoFrequenciaDomainService.SearchProjectionBySpecification(new ApuracaoFrequenciaFilterSpecification
                        {
                            SeqDivisaoTurma = dadosDivisao.SeqDivisaoTurma,
                            SeqAlunoHistoricoCicloLetivo = item.SeqAlunoHistoricoCicloLetivo,
                            SeqApuracaoFrequenciaDiferente = item.Seq
                        }, x => x.NumeroFaltas).ToList().Sum(x => x);

                        // Busca a carga horária 
                        decimal cargaHoraria = aluno.CargaHoraria.GetValueOrDefault();
                        if (aluno.CargaHorariaExecutada > 0 &&
                            aluno.CargaHorariaExecutada > aluno.CargaHoraria)
                            cargaHoraria = aluno.CargaHorariaExecutada;

                        // Atualiza o histórico escolar do aluno
                        var alunoHistoricoVO = aluno.Transform<HistoricoEscolarSituacaoFinalVO>();
                        alunoHistoricoVO.Faltas = (short)totalFaltasAula;
                        alunoHistoricoVO.CargaHoraria = (short)cargaHoraria;
                        var situacao = HistoricoEscolarDomainService.CalcularSituacaoFinal(alunoHistoricoVO);

                        // Calcula no novo percentual de frequencia
                        var percentualFrequenciaCalculada = HistoricoEscolarDomainService.CalcularPercentualFrequencia(cargaHoraria, (short)totalFaltasAula);

                        HistoricoEscolar historicoEscolar = new HistoricoEscolar
                        {
                            Seq = aluno.SeqHistoricoEscolar.Value,
                            SituacaoHistoricoEscolar = situacao,
                            Faltas = (short)totalFaltasAula,
                            PercentualFrequencia = percentualFrequenciaCalculada
                        };
                        HistoricoEscolarDomainService.UpdateFields(historicoEscolar, x => x.SituacaoHistoricoEscolar, x => x.Faltas, x => x.PercentualFrequencia);
                    }
                }

                // Deleta a aula
                DeleteEntity(aula);

                transacao.Commit();
            }
        }

        public long SalvarAula(AulaVO aulaVO)
        {
            var aulaDomain = aulaVO.Transform<Aula>(new
            {
                ApuracoesFrequencia = aulaVO.Ofertas.SelectMany(s => s.ApuracoesFrequencia).TransformList<ApuracaoFrequencia>()
            });
            using (var transacao = SMCUnitOfWork.Begin())
            {
                // Recupera inicialmente o sequencial da turma de uma divisão
                var dadosDivisao = DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(aulaDomain.SeqDivisaoTurma), x => new
                {
                    SeqDivisaoTurma = x.Seq,
                    x.SeqTurma,
                    x.DivisaoComponente.CargaHoraria
                });

                var diarioFechado = TurmaDomainService.DiarioTurmaFechado(new TurmaFilterSpecification { SeqDivisaoTurma = aulaDomain.SeqDivisaoTurma });
                if (diarioFechado.GetValueOrDefault())
                    throw new AulaDiarioTurmaFechadoException();

                // Recupera os alunos
                var alunos = TurmaDomainService.BuscarDiarioTurmaAluno(dadosDivisao.SeqTurma, dadosDivisao.SeqDivisaoTurma, null, null);

                // Percorre os alunos para validar o histórico e atualizar as faltas
                foreach (var item in alunos)
                {
                    // Recupera a apuração do aluno em questão
                    var apuracao = aulaDomain.ApuracoesFrequencia.FirstOrDefault(a => a.SeqAlunoHistoricoCicloLetivo == item.SeqAlunoHistoricoCicloLetivo);

                    if (apuracao == null)
                    {
                        continue;
                    }

                    // Consiste regra de máximo de faltas...

                    /*1) O número máximo de faltas já lançadas em aulas anteriores mais a falta que está sendo
                    * lançada não poderá ultrapassar o valor da carga horária da divisão do componente.
                    * Em caso de violação abortar operação e exibir a mensagem de erro:*/

                    // Soma o total de faltas já lançadas para este aluno nesta turma, ignorando o item que estou salvando no momento
                    var totalFaltasAula = ApuracaoFrequenciaDomainService.SearchProjectionBySpecification(new ApuracaoFrequenciaFilterSpecification
                    {
                        SeqDivisaoTurma = dadosDivisao.SeqDivisaoTurma,
                        SeqAlunoHistoricoCicloLetivo = apuracao.SeqAlunoHistoricoCicloLetivo,
                        SeqApuracaoFrequenciaDiferente = apuracao.Seq
                    }, x => x.NumeroFaltas).ToList().Sum(x => x);

                    // Soma as faltas do item que estou salvando
                    totalFaltasAula += apuracao.NumeroFaltas;

                    // Faz a consistência
                    if (totalFaltasAula > dadosDivisao.CargaHoraria)
                        throw new AulaNumeroMaximoFaltasMaiorCargaHorariaException(dadosDivisao.CargaHoraria.GetValueOrDefault());

                    // Atualiza o histórico escolar do aluno
                    if (item.SeqHistoricoEscolar.HasValue)
                    {
                        // Busca a carga horária 
                        decimal cargaHoraria = item.CargaHoraria.GetValueOrDefault();
                        if (item.CargaHorariaExecutada > 0 &&
                            item.CargaHorariaExecutada > item.CargaHoraria)
                            cargaHoraria = item.CargaHorariaExecutada;

                        var alunoHistoricoVO = item.Transform<HistoricoEscolarSituacaoFinalVO>();
                        alunoHistoricoVO.Faltas = (short)totalFaltasAula;
                        alunoHistoricoVO.CargaHoraria = (short)cargaHoraria;
                        var situacao = HistoricoEscolarDomainService.CalcularSituacaoFinal(alunoHistoricoVO);

                        // Calcula no novo percentual de frequencia
                        var percentualFrequenciaCalculada = HistoricoEscolarDomainService.CalcularPercentualFrequencia(cargaHoraria, (short)totalFaltasAula);

                        HistoricoEscolar historicoEscolar = new HistoricoEscolar
                        {
                            Seq = item.SeqHistoricoEscolar.Value,
                            SituacaoHistoricoEscolar = situacao,
                            Faltas = (short)totalFaltasAula,
                            PercentualFrequencia = percentualFrequenciaCalculada
                        };
                        HistoricoEscolarDomainService.UpdateFields(historicoEscolar, x => x.SituacaoHistoricoEscolar, x => x.Faltas, x => x.PercentualFrequencia);
                    }
                }

                SaveEntity(aulaDomain);

                transacao.Commit();

                return aulaDomain.Seq;
            }
        }

        /// <summary>
        /// Busca uma aula para edição. Considera como apuração os alunos da turma. Caso algum aluno tenha sido retirado da turma, este não aparecerá, mesmo que tenha apuração já cadastrada para ele.
        /// </summary>
        /// <param name="seq">Sequencial da aula</param>
        /// <param name="agruparAluosCurso">Quando setado os alunos são divididos por curso oferta localidade turno, caso contrário todos alunos são retornados num único “grupo”</param>
        /// <param name="seqsOrientadores">Sequenciais dos orientadores que estão chamando o recurso.</param>
        /// <returns>Aula para edição</returns>
        public AulaVO BuscarAula(long seq, bool agruparAluosCurso, List<long> seqsOrientadores)
        {
            // Busca os dados da aula
            var dados = SearchByKey(new SMCSeqSpecification<Aula>(seq), x => x.ApuracoesFrequencia);
            var ret = SMCMapperHelper.Create<AulaVO>(dados);
            ret.Ofertas = new List<AulaOfertaVO>();

            // Busca os alunos

            // Recupera inicialmente o sequencial da turma de uma divisão
            var seqs = SearchProjectionByKey(new SMCSeqSpecification<Aula>(seq), x => new
            {
                x.SeqDivisaoTurma,
                x.DivisaoTurma.SeqTurma
            });

            var alunos = TurmaDomainService.BuscarDiarioTurmaAluno(seqs.SeqTurma, seqs.SeqDivisaoTurma, null, seqsOrientadores);
            if (alunos != null)
            {
                IOrderedEnumerable<IGrouping<long, DiarioTurmaAlunoVO>> gruposOferta = agruparAluosCurso ?
                    // Grupos ordenados por oferta
                    alunos.GroupBy(g => g.SeqCursoOfertaLocalidadeTurno).OrderBy(o => o.First().DescricaoCursoOfertaLocalidadeTurno) :
                    // Grupo único apenas com a mesma estrutura
                    alunos.GroupBy(g => 0L).OrderBy(o => 0L);

                foreach (var grupoOferta in gruposOferta)
                {
                    var apuracoesFrequencia = new List<ApuracaoFrequenciaVO>();
                    ret.Ofertas.Add(new AulaOfertaVO()
                    {
                        SeqCursoOfertaLocalidadeTurno = grupoOferta.Key,
                        DescricaoCursoOfertaLocalidadeTurno = agruparAluosCurso ? grupoOferta.First().DescricaoCursoOfertaLocalidadeTurno : null,
                        ApuracoesFrequencia = apuracoesFrequencia
                    });

                    foreach (var item in grupoOferta)
                    {
                        // Procura a apuração para o aluno em questão
                        var apuracaoAtual = ret.ApuracoesFrequencia.FirstOrDefault(a => a.SeqAlunoHistoricoCicloLetivo == item.SeqAlunoHistoricoCicloLetivo);
                        if (apuracaoAtual == null)
                        {
                            apuracaoAtual = new ApuracaoFrequenciaVO
                            {
                                SeqAlunoHistoricoCicloLetivo = item.SeqAlunoHistoricoCicloLetivo,
                                SeqAula = seq,
                                AlunoFormado = item.AlunoFormado
                            };
                        }
                        apuracoesFrequencia.Add(apuracaoAtual);

                        short faltas = 0;

                        // Caso não tenha histórico, o número de faltas é o somatório das faltas do aluno
                        if (!item.SeqHistoricoEscolar.HasValue)
                        {
                            // busca o total de faltas já existentes para o aluno
                            faltas = (short)ApuracaoFrequenciaDomainService.SearchProjectionBySpecification(new ApuracaoFrequenciaFilterSpecification
                            {
                                SeqDivisaoTurma = seqs.SeqDivisaoTurma,
                                SeqAlunoHistoricoCicloLetivo = item.SeqAlunoHistoricoCicloLetivo,
                            }, x => x.NumeroFaltas).ToList().Sum(x => x);
                        }
                        else
                            faltas = item.Faltas ?? item.SomaFaltasApuracao ?? 0;

                        apuracaoAtual.FaltasAtuais = faltas - apuracaoAtual.NumeroFaltas;
                        apuracaoAtual.NomeAluno = item.NomeAluno;
                        apuracaoAtual.NumeroRegistroAcademico = item.NumeroRegistroAcademico;
                        apuracaoAtual.AlunoComHistorico = item.SeqHistoricoEscolar.HasValue;
                        apuracaoAtual.AlunoFormado = item.AlunoFormado;
                    }
                }
            }

            ret.ApuracoesFrequencia = ret.ApuracoesFrequencia?.Where(a => a.NumeroRegistroAcademico != 0).OrderBy(a => a.NomeAluno).ToList();

            return ret;
        }
    }
}