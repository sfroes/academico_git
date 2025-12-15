using SMC.Academico.Common.Areas.TUR.Exceptions;
using SMC.Academico.Domain.Areas.ALN.DomainServices;
using SMC.Academico.Domain.Areas.ALN.Specifications;
using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.DCT.DomainServices;
using SMC.Academico.Domain.Areas.MAT.DomainServices;
using SMC.Academico.Domain.Areas.TUR.Models;
using SMC.Academico.Domain.Areas.TUR.Specifications;
using SMC.Academico.Domain.Areas.TUR.ValueObjects;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Domain;
using SMC.Framework.Extensions;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Domain.Areas.TUR.DomainServices
{
    public class DivisaoTurmaColaboradorDomainService : AcademicoContextDomain<DivisaoTurmaColaborador>
    {
        #region [ DomainService ]

        private DivisaoTurmaDomainService DivisaoTurmaDomainService => Create<DivisaoTurmaDomainService>();

        private PlanoEstudoItemDomainService PlanoEstudoItemDomainService => Create<PlanoEstudoItemDomainService>();

        private SolicitacaoMatriculaItemDomainService SolicitacaoMatriculaItemDomainService => Create<SolicitacaoMatriculaItemDomainService>();

        private TurmaDomainService TurmaDomainService => Create<TurmaDomainService>();

        private HistoricoEscolarDomainService HistoricoEscolarDomainService => Create<HistoricoEscolarDomainService>();

        private ColaboradorDomainService ColaboradorDomainService => Create<ColaboradorDomainService>();

        #endregion [ DomainService ]

        #region [ Queries ]

        #region [ _buscarColaboradoresDivisaoRelatorioTurma ]

        private string _buscarColaboradoresDivisaoRelatorioTurma =
                        @"  SELECT  DT.seq_divisao_turma AS SeqDivisaoTurma,
                                    DTC.seq_atuacao_colaborador AS SeqPessoaAtuacaoColaborador,
		                            DTPDPC.nom_pessoa AS NomeColaborador,
		                            DTPDPC.nom_social AS NomeSocialColaborador
                              FROM TUR.divisao_turma DT
                             INNER JOIN TUR.divisao_turma_colaborador DTC ON DT.seq_divisao_turma = DTC.seq_divisao_turma
                             LEFT JOIN PES.pessoa_atuacao DTPAC ON DTC.seq_atuacao_colaborador = DTPAC.seq_pessoa_atuacao
                             LEFT JOIN PES.pessoa_dados_pessoais DTPDPC ON DTPAC.seq_pessoa_dados_pessoais = DTPDPC.seq_pessoa_dados_pessoais
                             WHERE DT.seq_divisao_turma IN ({0})";

        #endregion [ _buscarColaboradoresDivisaoRelatorioTurma ]

        #endregion [ Queries ]

        /// <summary>
        /// Buscar o tipo de organização e dados iniciais da divisão de turma para associar professor
        /// </summary>
        /// <param name="seqDivisao">Sequencial da divisão turma</param>
        /// <returns>Objeto com dados iniciais da divisão da turma</returns>
        public DivisaoTurmaColaboradorVO BuscarConfiguracaoDivisaoTurmaColaborador(long seqDivisao)
        {
            var tipoOrganizacao = DivisaoTurmaDomainService.BuscarTipoComponenteDivisaoTurma(seqDivisao);

            var registro = new DivisaoTurmaColaboradorVO();
            registro.DescricaoTipoOrganizacao = tipoOrganizacao.DescricaoTipoOrganizacao;
            registro.ExisteHistoricoEscolarAluno = this.VerificarExisteHistoricoEscolarTurma(seqDivisao);

            return registro;
        }

        /// <summary>
        /// Buscar a associação de colaborador com a divisão de turma
        /// </summary>
        /// <param name="seq">Sequencial da associacao de divisao com colaborador</param>
        /// <returns>Objeto que associa o colaborador a divisão com suas organizações</returns>
        public DivisaoTurmaColaboradorVO BuscarDivisaoTurmaColaborador(long seq)
        {
            var registro = this.SearchByKey(new SMCSeqSpecification<DivisaoTurmaColaborador>(seq)).Transform<DivisaoTurmaColaboradorVO>();

            var divisao = DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(registro.SeqDivisaoTurma), p => new { seqDivisao = p.Seq, seqTurma = p.SeqTurma });
            registro.SeqDivisao = divisao.seqDivisao;
            registro.SeqTurma = divisao.seqTurma;

            var tipoOrganizacao = DivisaoTurmaDomainService.BuscarTipoComponenteDivisaoTurma(registro.SeqDivisao);
            registro.DescricaoTipoOrganizacao = tipoOrganizacao.DescricaoTipoOrganizacao;
            registro.ExisteHistoricoEscolarAluno = this.VerificarExisteHistoricoEscolarTurma(registro.SeqDivisaoTurma);

            return registro;
        }

        /// <summary>
        /// Verifica se algum aluno da turma tem historico escolcar
        /// </summary>
        /// <param name="seqDivisao">Sequencial da divisao de turma</param>
        /// <returns>Boleano</returns>
        public bool VerificarExisteHistoricoEscolarTurma(long seqDivisao)
        {
            bool retorno = false;

            long seqTurma = DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisao), p => p.SeqTurma);

            var alunos = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, null, null, null);

            foreach (var item in alunos)
            {
                if (item.SeqHistoricoEscolar.HasValue)
                {
                    retorno = true;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Remover a associação do colaborador com a divisão de turma
        /// </summary>
        /// <param name="seq">Sequencial da associacao de divisao com colaborador</param>
        public void ExcluirDivisaoTurmaColaborador(long seq)
        {
            var seqDivisaoTurma = this.SearchProjectionByKey(seq, p => p.SeqDivisaoTurma);

            this.DeleteEntity(seq);

            AtualizarProfessoresHistoricoEscolar(seqDivisaoTurma);
        }

        /// <summary>
        /// Grava uma colaborador associado a divisão com suas respectivas organizações
        /// </summary>
        /// <param name="divisaoColaborador">Dados da divisão turma colaboradores</param>
        public void SalvarDivisaoTurmaColaborador(DivisaoTurmaColaboradorVO divisaoColaborador)
        {
            divisaoColaborador.SeqDivisaoTurma = divisaoColaborador.SeqDivisao;

            var registro = divisaoColaborador.Transform<DivisaoTurmaColaborador>();

            ValidarOrigemAvaliacaoDivisaoTurma(divisaoColaborador.SeqDivisaoTurma, divisaoColaborador.SeqColaborador);

            this.SaveEntity(registro);

            this.AtualizarProfessoresHistoricoEscolar(divisaoColaborador.SeqDivisao);
        }

        /// <summary>
        /// Atualiza os professores da turma ao historico escolar do aluno
        /// </summary>
        private void AtualizarProfessoresHistoricoEscolar(long seqDivisaoTurma)
        {
            long seqTurma = DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma), p => p.SeqTurma);
            var alunos = TurmaDomainService.BuscarDiarioTurmaAluno(seqTurma, null, null, null);
            //Verifica o aluno que tem historico escolar
            foreach (var item in alunos)
            {
                if (item.SeqHistoricoEscolar.HasValue)
                {
                    HistoricoEscolarDomainService.GravarHistoricoEscolarColaborador(item.SeqHistoricoEscolar.Value, seqTurma, item.SeqPlanoEstudo, item.TurmaOrientacao);
                }
            }
        }

        /// <summary>
        /// Valida se a quantidade de professores vinculados a divisão não ultrapassa a quantidade de professores
        /// parametrizada na Origem Avaliação.
        /// </summary>
        /// <param name="seqDivisaoTurma">Sequencial da divisão da turma</param>
        private void ValidarOrigemAvaliacaoDivisaoTurma(long seqDivisaoTurma, long seqColaborador)
        {
            /*
            Ao associar um professor à divisão da turma, verificar se a quantidade de professores parametrizada na origem avaliação da divisão está preenchida.
            Se preenchida, verificar se a quantidade de professores não está sendo ultrapassada.Em caso de violação, abortar operação e exibir a mensagem de erro:
            MENSAGEM: Operação não permitida. Somente { quantidade de professores }
            professor(es) pode(m) ser associado(s) à divisão da turma.
            Se não preenchida, não consistir.
            */
            var dadosDivisaoTurma = this.DivisaoTurmaDomainService.SearchProjectionByKey(new SMCSeqSpecification<DivisaoTurma>(seqDivisaoTurma), d => new
            {
                QuantidadeProfessoresOrigemAvaliacao = d.OrigemAvaliacao.QuantidadeProfessores,
                QuantidadeColaboradores = d.Colaboradores.Count,
                Colaboradores = d.Colaboradores
            });

            if (dadosDivisaoTurma.QuantidadeProfessoresOrigemAvaliacao != null)
            {
                bool colaboradorExistenteNaDivisaoTurma = dadosDivisaoTurma.Colaboradores.Any(c => c.SeqColaborador == seqColaborador);

                // Se o colaborador que está sendo salvo não estiver nos colaboradores cadastrados nos dados de divisão da turma,
                // então aí sim a contagem é feita
                if (!colaboradorExistenteNaDivisaoTurma)
                {
                    if (dadosDivisaoTurma.QuantidadeColaboradores + 1 > dadosDivisaoTurma.QuantidadeProfessoresOrigemAvaliacao)
                        throw new DivisaoTurmaColaboradorQuantidadeProfessoresInvalidaException(dadosDivisaoTurma.QuantidadeProfessoresOrigemAvaliacao.GetValueOrDefault());
                }
            }
        }

        /// <summary>
        /// Adicionar os professores orientadores as turmas da pessoa atuação
        /// </summary>
        /// <param name="seqsDivisoesColaboradores">Objeto somente com sequencial divisão e sequencial colaborador</param>
        public void AdicionarProfessorOrientador(List<DivisaoTurmaColaboradorVO> seqsDivisoesColaboradores)
        {
            foreach (var seqDivisaoColaborador in seqsDivisoesColaboradores)
            {
                if (seqDivisaoColaborador.SeqDivisao == 0)
                    seqDivisaoColaborador.SeqDivisao = seqDivisaoColaborador.SeqDivisaoTurma;

                if (seqDivisaoColaborador.SeqDivisaoTurma == 0)
                    seqDivisaoColaborador.SeqDivisaoTurma = seqDivisaoColaborador.SeqDivisao;

                var specDivisaoColaborador = new DivisaoTurmaColaboradorFilterSpecification()
                {
                    SeqDivisaoTurma = seqDivisaoColaborador.SeqDivisaoTurma,
                    SeqColaborador = seqDivisaoColaborador.SeqColaborador
                };

                if (this.Count(specDivisaoColaborador) == 0)
                {
                    var registroDivisaoColaborador = new DivisaoTurmaColaboradorVO()
                    {
                        SeqColaborador = seqDivisaoColaborador.SeqColaborador,
                        SeqDivisao = seqDivisaoColaborador.SeqDivisaoTurma,
                    };

                    this.SalvarDivisaoTurmaColaborador(registroDivisaoColaborador);
                }
            }
        }

        /// <summary>
        /// Excluir os professores orientadores as turmas da pessoa atuação
        /// </summary>
        /// <param name="seqSolicitacaoMatricula">Sequencial da solicitação de matricula</param>
        public void ExcluirProfessorOrientadorPorSolicitacaoMatricula(long seqSolicitacaoMatricula)
        {
            var solicitacaoItens = SolicitacaoMatriculaItemDomainService.BuscarSolicitacaoMatriculaItensPlano(seqSolicitacaoMatricula).Where(w => w.SeqDivisaoTurma.HasValue);

            foreach (var item in solicitacaoItens)
            {
                if ((item.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoSemSucesso && item.PertencePlanoEstudo.GetValueOrDefault() == false)
                   || (item.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.FinalizadoComSucesso && item.PertencePlanoEstudo.GetValueOrDefault() == true)
                   || item.ClassificacaoSituacaoFinal == ClassificacaoSituacaoFinal.Cancelado)
                {
                    var divisaoColaborador = PlanoEstudoItemDomainService.BuscarPlanoEstudoItemOrientacoesTurmas(null, item.SeqDivisaoTurma);

                    var seqsColaboradoresPlano = divisaoColaborador.SelectMany(s => s.Orientacao.OrientacoesColaborador.Select(o => o.SeqColaborador)).ToList();

                    if (seqsColaboradoresPlano.Count == 0)
                        continue;

                    var divisaoColaboradorSemPlanoEstudo = new DivisaoTurmaColaboradorFilterSpecification()
                    {
                        SeqDivisaoTurma = item.SeqDivisaoTurma,
                        SeqsColaboradorDiferente = seqsColaboradoresPlano,
                    };

                    var colaboradoresExcluir = this.SearchBySpecification(divisaoColaboradorSemPlanoEstudo);

                    foreach (var colaborador in colaboradoresExcluir)
                        this.DeleteEntity(colaborador);
                }
            }
        }

        /// <summary>
        /// Excluir os professores orientadores as turmas da pessoa atuação
        /// </summary>
        /// <param name="seqsDivisoesTurma">Sequenciais das divisões de turma</param>
        public void ExcluirProfessorOrientador(List<long> seqsDivisoesTurma, long seqPlanoEstudoIgnorar)
        {
            foreach (var item in seqsDivisoesTurma)
            {
                // Monta spec para recuperar todos os orientadores dos planos de estudo diferentes do que está sendo cancelado
                var spec = new PlanoEstudoItemFilterSpecification()
                {
                    SeqPlanoEstudoDiferente = seqPlanoEstudoIgnorar,
                    SeqDivisaoTurma = item,
                    OrientacaoTurma = true,
                    PlanoEstudoAtual = true,
                    SomenteTurma = true
                };

                // Recupera os colaboradores que fazem parte de orientação dos planos de estudo item
                var divisaoColaborador = PlanoEstudoItemDomainService.SearchProjectionBySpecification(spec, x => new
                {
                    SeqPlanoEstudoItem = x.Seq,
                    SeqsColaboradores = x.Orientacao.OrientacoesColaborador.Select(o => o.SeqColaborador)
                }).ToList();

                var seqsColaboradoresPlano = divisaoColaborador.SelectMany(s => s.SeqsColaboradores).Distinct().ToList();

                //var divisaoColaborador = PlanoEstudoItemDomainService.BuscarPlanoEstudoItemOrientacoesTurmas(null, item);
                //var seqsColaboradoresPlano = divisaoColaborador.SelectMany(s => s.Orientacao.OrientacoesColaborador.Select(o => o.SeqColaborador)).Distinct().ToList();
                //if (seqsColaboradoresPlano.Count == 0)
                //    continue;

                var divisaoColaboradorSemPlanoEstudo = new DivisaoTurmaColaboradorFilterSpecification()
                {
                    SeqDivisaoTurma = item,
                    SeqsColaboradorDiferente = seqsColaboradoresPlano,
                };

                var colaboradoresExluir = this.SearchBySpecification(divisaoColaboradorSemPlanoEstudo);

                foreach (var colaborador in colaboradoresExluir)
                    this.DeleteEntity(colaborador);
            }
        }

        /// <summary>
        /// Buscar a lista de colaboradores de todas as divisões de turma do relatório
        /// </summary>
        /// <param name="seqsDivisaoTurma">Sequenciais de divisões de turmas</param>
        /// <returns>Lista com todos colaboradores de todas as divisões de turmas</returns>
        public List<DivisaoTurmaRelatorioColaboradorVO> BuscarColaboradoresDivisaoRelatorioTurma(List<long> seqsDivisaoTurma)
        {
            var registros = RawQuery<DivisaoTurmaRelatorioColaboradorVO>(string.Format(_buscarColaboradoresDivisaoRelatorioTurma, string.Join(" , ", seqsDivisaoTurma)));

            return registros;
        }
    }
}