using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IApuracaoAvaliacaoService : ISMCService
    {
        LancamentoNotaBancaExaminadoraData BuscarLancamentoNotaBancaExaminadoraInsert(LancamentoNotaBancaExaminadoraData model);

        long SalvarLancamentoNotaBancaExaminadora(LancamentoNotaBancaExaminadoraData model);

        void ExcluirNotaLancadaApuracaoAvaliacao(long seqAplicacaoAvaliacao);

        /// <summary>
        /// Busca todas aplicações de avaliação numa turma para uma origem
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação</param>
        /// <param name="seqProfessor">Sequencial do professor logado</param>
        /// <param name="administrativo">Indicativo que o relaótio foi solicitado pela secretraria</param>
        /// <returns>Dados para o lançamento</returns>
        LancamentoAvaliacaoData BuscarLancamentosAvaliacao(long seqOrigemAvaliacao, long seqProfessor, bool administrativo = false);

        void SalvarLancamentosAvaliacao(LancamentoAvaliacaoData data);

        /// <summary>
        /// Buscar todas a apuracoes do aluno nas divisões da turma vinculada à origem avaliação informada
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de apuracoes do aluno</returns>
        List<ApuracaoAvaliacaoData> BuscarApuracoesDasDivisioesPorAlunoTurma(long seqAlunoHistorico, long seqOrigemAvaliacao);

        /// <summary>
        /// Busca todas as apurações do aluno na turma
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de apuracoes do aluno</returns>
        List<ApuracaoAvaliacaoData> BuscarApuracoesPorAlunoOrigemAvaliacao(long seqAlunoHistorico, long seqOrigemAvaliacao);

        /// <summary>
        /// Preenche os históricos dos alunos e fecha o diário da turma
        /// </summary>
        /// <param name="model">Dados para o fechamento</param>
        void FecharLancamentoDiario(LancamentoNotaFechamentoDiarioData model);

        /// <summary>
        /// Calcula os totais e situação final para os alunos de uma turma
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial de origem de avaliação da turma</param>
        /// <param name="seqsAlunoHistorico">Sequenciais dos alunos</param>
        /// <param name="permiteAlunoSemNota">Configuração da turma permite aluno sem nota</param>
        /// <returns>Totais e situações finais dos alunos</returns>
        List<LancamentoNotaTurmaAlunoData> CalcularTotaisTurma(long seqOrigemAvaliacao, long[] seqsAlunoHistorico, bool permiteAlunoSemNota);

        /// <summary>
        /// Adiciona um arquivo anexado à uma apuração de uma aplicação de avaliação caso não tenha arquivo ainda.
        /// </summary>
        /// <param name="seqAplicacaoAvaliacao">Sequencial da aplicação de avaliação</param>
        /// <param name="arquivoAnexado">Arquivo anexado</param>
        void AdicionarArquivoAta(long seqAplicacaoAvaliacao, SMCUploadFile arquivoAnexado);
    }
}