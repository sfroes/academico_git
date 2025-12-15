using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.APR.Includes;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.APR.Models;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.DCT.Specifications;
using SMC.Academico.Domain.Areas.DCT.ValueObjects;
using SMC.Academico.Domain.Areas.TUR.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Framework;
using SMC.Framework.Domain;
using SMC.Framework.Model;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.APR.DomainServices
{
    public class OrigemAvaliacaoDomainService : AcademicoContextDomain<OrigemAvaliacao>
    {
        #region [Domain Service]

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private AlunoHistoricoDomainService AlunoHistoricoDomainService => Create<AlunoHistoricoDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        #endregion

        #region [Service]



        #endregion

        /// <summary>
        /// Buscar a descrição da origem de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequncial da origem de avaliação</param>
        /// <returns>Descrição da origem de avaliação</returns>
        public string BuscarDescricaoOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            string retorno = string.Empty;
            var dadosOrigemAvaliacao = SearchProjectionByKey(new SMCSeqSpecification<OrigemAvaliacao>(seqOrigemAvaliacao), p => new
            {
                p.TipoOrigemAvaliacao,
                p.Descricao
            });

            /*
            NV01 -Apresentar a descrição da origem de avaliação que foi recebida como parâmetro.
            Se a descrição estiver nula, antes de apresentar o cabeçalho, salvar a descrição da origem de avaliação,
            considerando a seguinte regra
            Se o tipo da origem de avaliação que foi recebido como parâmetro for:
            Divisão de turma - RN_TUR_026 - Exibição Descrição · Divisão Turma
            Turma - RN_TUR_025 - Exibição Descrição Turma 
            */

            if (dadosOrigemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                //RN_TUR_025 - Exibição Descrição Turma - já levando em consideração que não tem aluno vinculado
                long seqTurma = TurmaDomainService.SearchProjectionBySpecification(new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => p.Seq).FirstOrDefault();
                retorno = TurmaDomainService.BuscarDescricaoTurmaConcatenado(seqTurma);
            }
            else if (dadosOrigemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                //RN_TUR_026 - Exibição Descrição · Divisão Turma
                long seqDivisaoTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => p.Seq).FirstOrDefault();
                retorno = DivisaoTurmaDomainService.ObterDescricaoDivisaoTurma(seqDivisaoTurma);
            }

            if (retorno.Trim().ToLower() != dadosOrigemAvaliacao.Descricao?.Trim().ToLower())
            {
                OrigemAvaliacao origemAvaliacao = new OrigemAvaliacao() { Seq = seqOrigemAvaliacao, Descricao = retorno };
                UpdateFields(origemAvaliacao, p => p.Descricao);
            }

            return retorno;
        }

        /// <summary>
        /// Buscar origem avaliacao
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Origem avaliação</returns>
        public OrigemAvaliacao BuscarOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            var includes = IncludesOrigemAvaliacao.EscalaApuracao | IncludesOrigemAvaliacao.CriterioAprovacao_EscalaApuracao_Itens;

            OrigemAvaliacao origemAvaliacao = SearchByKey(new SMCSeqSpecification<OrigemAvaliacao>(seqOrigemAvaliacao), includes);

            return origemAvaliacao;
        }

        /// <summary>
        /// Diario aberto ou fechado baseado na origem de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Boleano</returns>
        public bool DiarioAbertoPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            bool retorno = true;

            var dadosOrigemAvaliacao = SearchProjectionByKey(new SMCSeqSpecification<OrigemAvaliacao>(seqOrigemAvaliacao), p => new
            {
                p.TipoOrigemAvaliacao
            });

            if (dadosOrigemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                retorno = TurmaDomainService.SearchProjectionBySpecification(new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao },
                    p => p.HistoricosFechamentoDiario.Count > 0 ? p.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false).FirstOrDefault();
            }
            else if (dadosOrigemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                retorno = DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao },
                    p => p.Turma.HistoricosFechamentoDiario.Count > 0 ? p.Turma.HistoricosFechamentoDiario.OrderByDescending(h => h.DataInclusao).FirstOrDefault().DiarioFechado : false).FirstOrDefault();
            }

            return retorno;
        }

        /// <summary>
        /// Buscar alunos baseado na origem avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de alunos com sequencial histiorico aluno e nome</returns>
        public List<SMCDatasourceItem> BuscarAlunosPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            /*Listar os alunos que estão matriculados na turma e / ou divisâo de turma que está ligada à origem de avaliação da
                aplicação de avaliação recebida como parâmetro
                Caso o tipo da origem de avaliação seja:
                          Divisão de turma - Listar os alunos que estão matriculados na divisão de turma da origem · de avaliação em
              questão.
              · Turma - Listar os alunos que estão matriculados em qualquer divisão de turma da origem de avaliação em
              questão(realizar um distinct).
              Para cada aluno na listagem, apresentar o número de registro acadêmico e o nome do aluno de acordo com a
              RN_PES_037 - Nome e Nome Social - Visão Aluno.Apresentar o nome em UPPER CASE.*/
            List<SMCDatasourceItem> retorno = new List<SMCDatasourceItem>();
            List<DiarioTurmaAlunoVO> alunos = new List<DiarioTurmaAlunoVO>();
            long seqTurma = 0;
            long? seqDivisaoTurma = null;

            TipoOrigemAvaliacao tipoOrigemAvaliacao = SearchProjectionByKey(new SMCSeqSpecification<OrigemAvaliacao>(seqOrigemAvaliacao), p => p.TipoOrigemAvaliacao);

            if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                seqTurma = TurmaDomainService.SearchProjectionBySpecification(new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => p.Seq).FirstOrDefault();
            }
            else if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                var seqs = DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => new
                {
                    SeqTurma = p.SeqTurma,
                    SeqDivisaoTurma = p.Seq
                }).FirstOrDefault();
                seqTurma = seqs.SeqTurma;
                seqDivisaoTurma = seqs.SeqDivisaoTurma;
            }

            alunos = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, seqDivisaoTurma ?? null, null, null);

            foreach (var item in alunos)
            {
                retorno.Add(new SMCDatasourceItem
                {
                    Seq = item.SeqAlunoHistorico,
                    Descricao = $"{item.NumeroRegistroAcademico} - {item.NomeAluno}"
                });
            }

            return retorno;
        }

        /// <summary>
        /// Busca os dados de orientação de uma origem de avalação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação</param>
        /// <returns>Sequencial da turma, tipo da origem, se é turma de orientação e colaboradores responsáveis</returns>
        public (long seqTurma, TipoOrigemAvaliacao tipoOrigemAvaliacao, bool orientacao, long[] seqsColaboradoresResposavelTurma) BuscarDadosOrientacao(long seqOrigemAvaliacao)
        {
            TipoOrigemAvaliacao tipoOrigemAvaliacao = BuscarOrigemAvaliacao(seqOrigemAvaliacao).TipoOrigemAvaliacao;
            (long seqTurma, bool orientacao, IEnumerable<long> seqsColaboradores) dadosTurma = (0, false, new long[0]);

            if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                dadosTurma = TurmaDomainService.SearchProjectionBySpecification(new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => new
                {
                    p.Seq,
                    Orientacao = p.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.DivisoesComponente.Any(a => a.TipoDivisaoComponente.GeraOrientacao),
                    SeqsColaborador = p.Colaboradores.Select(s => s.SeqColaborador)
                }).ToList().Select(s => (s.Seq, s.Orientacao, s.SeqsColaborador)).FirstOrDefault();
            }
            else if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.DivisaoTurma)
            {
                dadosTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao }, p => new
                {
                    p.SeqTurma,
                    Orientacao = p.Turma.ConfiguracoesComponente.FirstOrDefault(f => f.Principal).ConfiguracaoComponente.DivisoesComponente.Any(a => a.TipoDivisaoComponente.GeraOrientacao),
                    SeqsColaborador = p.Turma.Colaboradores.Select(s => s.SeqColaborador)
                }).ToList().Select(s => (s.SeqTurma, s.Orientacao, s.SeqsColaborador)).FirstOrDefault();
            }

            return (dadosTurma.seqTurma, tipoOrigemAvaliacao, dadosTurma.orientacao, dadosTurma.seqsColaboradores.ToArray());
        }

        /// <summary>
        /// Buscar professores de uma origem avaliacao
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de professores responsaveis da turma</returns>
        public List<TurmaCabecalhoResponsavelVO> BuscarProfessoresPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            List<TurmaCabecalhoResponsavelVO> retorno = new List<TurmaCabecalhoResponsavelVO>();

            var origemAvaliacao = this.SearchProjectionByKey(seqOrigemAvaliacao, p => new
            {
                p.TipoOrigemAvaliacao
            });

            var result = new List<ColaboradoresVO>();

            if (origemAvaliacao.TipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                var spec = new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao };
                var seqsColaborador = this.TurmaDomainService.SearchProjectionByKey(spec, p => p.Colaboradores.Select(s => s.SeqColaborador).ToList());
                if (seqsColaborador.Count() > 0)
                {
                    var turma = this.ColaboradorDomainService.SearchProjectionBySpecification(new ColaboradorFilterSpecification { Seqs = seqsColaborador.ToArray() }, s => new ColaboradoresVO
                    {
                        SeqColaborador = s.Seq,
                        SeqDadosPessoais = s.DadosPessoais.Seq,
                        Nome = s.DadosPessoais.Nome,
                        NomeSocial = s.DadosPessoais.NomeSocial
                    }).ToList();
                    result = turma;
                }
            }
            else
            {
                var spec = new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao };
                var seqsColaborador = this.DivisaoTurmaDomainService.SearchProjectionByKey(spec, p => p.Colaboradores.Select(s => s.SeqColaborador).ToList());
                if (seqsColaborador.Count() > 0)
                {
                    var divisaoTurma = this.ColaboradorDomainService.SearchProjectionBySpecification(new ColaboradorFilterSpecification { Seqs = seqsColaborador.ToArray() }, s => new ColaboradoresVO
                    {
                        SeqColaborador = s.Seq,
                        SeqDadosPessoais = s.DadosPessoais.Seq,
                        Nome = s.DadosPessoais.Nome,
                        NomeSocial = s.DadosPessoais.NomeSocial
                    }).ToList();
                    result = divisaoTurma;
                }
            }

            if (result != null && result.Count > 0)
            {
                result.SMCForEach(f =>
                {
                    var nomeCompleto = string.IsNullOrEmpty(f.NomeSocial) ? f.Nome : $"{f.NomeSocial} ({f.Nome})";

                    // RN_PES_023 - Nome e Nome Social - Visão Administrativo
                    retorno.Add(new TurmaCabecalhoResponsavelVO() { SeqColaborador = f.SeqColaborador, NomeColaborador = nomeCompleto });
                });
            }

            return retorno;
        }

        /// <summary>
        /// Buscar origem avaliação por divisão
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequecial divisão de turma</param>
        /// <returns></returns>
        public long BuscarOrigemAvaliacaoPorDivisaoTurma(long seqDivisaoTurma)
        {
            return DivisaoTurmaDomainService.SearchProjectionByKey(seqDivisaoTurma, p => p.SeqOrigemAvaliacao);
        }

        /// <summary>
        /// Buscar descrição do ciclo letivo pela origem avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns></returns>
        public string BusacarDescricaoCicloLetivoPorOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            string retorno = "";

            TipoOrigemAvaliacao tipoOrigemAvaliacao = this.SearchProjectionByKey(seqOrigemAvaliacao, p => p.TipoOrigemAvaliacao);

            if (tipoOrigemAvaliacao == TipoOrigemAvaliacao.Turma)
            {
                var spec = new TurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao };
                retorno = TurmaDomainService.SearchProjectionBySpecification(spec, p => p.CicloLetivoInicio.Descricao).FirstOrDefault();
            }
            else
            {
                var spec = new DivisaoTurmaFilterSpecification() { SeqOrigemAvaliacao = seqOrigemAvaliacao };
                long seqTurma = DivisaoTurmaDomainService.SearchProjectionBySpecification(spec, p => p.SeqTurma).FirstOrDefault();
                retorno = TurmaDomainService.SearchProjectionByKey(seqTurma, p => p.CicloLetivoInicio.Descricao);
            }

            return retorno;
        }

        /// <summary>
        /// houve alteração na origem de avaliação
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Boleano</returns>
        public bool AlteracaoManualOrigemAvaliacao(long seqOrigemAvaliacao)
        {
            var dadosOrigemAvaliacao = SearchProjectionByKey(new SMCSeqSpecification<OrigemAvaliacao>(seqOrigemAvaliacao),
                p => new { p.OcorreuAlteracaoManual });

            return dadosOrigemAvaliacao.OcorreuAlteracaoManual.HasValue ? dadosOrigemAvaliacao.OcorreuAlteracaoManual.Value : false;
        }
    }
}
