using SMC.Academico.Domain.Areas.APR.DomainServices;
using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.Domain.Areas.ORT.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.APR.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORG.Interfaces;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.APR.Services
{
    public class ApuracaoAvaliacaoService : SMCServiceBase, IApuracaoAvaliacaoService
    {
        #region DomainServices

        private ApuracaoAvaliacaoDomainService ApuracaoAvaliacaoDomainService { get => Create<ApuracaoAvaliacaoDomainService>(); }

        #endregion Domain Services

        #region [ Service ]

        private IInstituicaoNivelCalendarioService InstituicaoNivelCalendarioService { get => Create<IInstituicaoNivelCalendarioService>(); }

        #endregion [ Service ]

        public LancamentoNotaBancaExaminadoraData BuscarLancamentoNotaBancaExaminadoraInsert(LancamentoNotaBancaExaminadoraData model)
        {
            var entity = ApuracaoAvaliacaoDomainService.BuscarLancamentoNotaBancaExaminadoraInsert(model.Seq)
                .Transform<LancamentoNotaBancaExaminadoraData>();

            return entity;
        }



        public long SalvarLancamentoNotaBancaExaminadora(LancamentoNotaBancaExaminadoraData model)
        {
            var entity = ApuracaoAvaliacaoDomainService.SalvarLancamentoNotaBancaExaminadora(model.Transform<LancamentoNotaBancaExaminadoraVO>());

            return entity;
        }

        /// <summary>
        /// Caso o usuário clique em "Sim":  
        /// Se houver somente uma avaliação com apuração · registrada para
        /// o componente curricular em questão:
        ///     · Excluir a apuração registrada para a avaliação.
        ///     · Excluir do histórico escolar do aluno o registro relativo ao componente curricular em questão.
        /// · Se houver mais de uma avaliação com apuração registrada para o componente curricular em questão:
        ///     · Atualizar no histórico escolar do aluno o registro relativo ao componente curricular em questão com a
        ///     nota/conceito da avaliação com apuração registrada que não será excluída
        ///     · Excluir a apuração registrada para a avaliação.
        /// · Excluir o registro de publicação do trabalho no BDP e a situação deste associada.
        /// </summary>
        /// <param name="seqAplicacaoAvaliacao"></param>
        public void ExcluirNotaLancadaApuracaoAvaliacao(long seqAplicacaoAvaliacao)
        {
            ApuracaoAvaliacaoDomainService.ExcluirNotaLancadaApuracaoAvaliacao(seqAplicacaoAvaliacao);
        }

        /// <summary>
        /// Busca todas aplicações de avaliação numa turma para uma origem
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação</param>
        /// <param name="seqProfessor">Sequencial do professor logado</param>
        /// <param name="administrativo">Indicativo que o relaótio foi solicitado pela secretraria</param>
        /// <returns>Dados para o lançamento</returns>
        public LancamentoAvaliacaoData BuscarLancamentosAvaliacao(long seqOrigemAvaliacao, long seqProfessor, bool administrativo = false)
        {
            return ApuracaoAvaliacaoDomainService.BuscarLancamentosAvaliacao(seqOrigemAvaliacao, seqProfessor, administrativo)
                                                 .Transform<LancamentoAvaliacaoData>();
        }

        public void SalvarLancamentosAvaliacao(LancamentoAvaliacaoData data)
        {
            var model = data.Transform<LancamentoAvaliacaoVO>();
            ApuracaoAvaliacaoDomainService.SalvarLancamentosAvaliacao(model);
        }

        /// <summary>
        /// Buscar todas a apuracoes do aluno nas divisões da turma vinculada à origem avaliação informada
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de apuracoes do aluno</returns>
        public List<ApuracaoAvaliacaoData> BuscarApuracoesDasDivisioesPorAlunoTurma(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            return ApuracaoAvaliacaoDomainService.BuscarApuracoesDasDivisioesPorAlunoTurma(seqAlunoHistorico, seqOrigemAvaliacao).TransformList<ApuracaoAvaliacaoData>();
        }

        /// <summary>
        /// Busca todas as apurações do aluno na turma
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial aluno historico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial origem avaliação</param>
        /// <returns>Lista de apuracoes do aluno</returns>
        public List<ApuracaoAvaliacaoData> BuscarApuracoesPorAlunoOrigemAvaliacao(long seqAlunoHistorico, long seqOrigemAvaliacao)
        {
            return ApuracaoAvaliacaoDomainService.BuscarApuracoesPorAlunoOrigemAvaliacao(seqAlunoHistorico, seqOrigemAvaliacao).TransformList<ApuracaoAvaliacaoData>();
        }

        /// <summary>
        /// Preenche os históricos dos alunos e fecha o diário da turma
        /// </summary>
        /// <param name="model">Dados para o fechamento</param>
        public void FecharLancamentoDiario(LancamentoNotaFechamentoDiarioData model)
        {
            ApuracaoAvaliacaoDomainService.FecharLancamentoDiario(model.Transform<LancamentoNotaFechamentoDiarioVO>());
        }

        /// <summary>
        /// Calcula os totais e situação final para os alunos de uma turma
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Sequencial de origem de avaliação da turma</param>
        /// <param name="seqsAlunoHistorico">Sequenciais dos alunos</param>
        /// <param name="permiteAlunoSemNota">Configuração da turma permite aluno sem nota</param>
        /// <returns>Totais e situações finais dos alunos</returns>
        public List<LancamentoNotaTurmaAlunoData> CalcularTotaisTurma(long seqOrigemAvaliacao, long[] seqsAlunoHistorico, bool permiteAlunoSemNota)
        {
            return ApuracaoAvaliacaoDomainService.CalcularTotaisTurma(seqOrigemAvaliacao, seqsAlunoHistorico, permiteAlunoSemNota).TransformList<LancamentoNotaTurmaAlunoData>();
        }

        public void AdicionarArquivoAta(long seqAplicacaoAvaliacao, SMCUploadFile arquivoAnexado)
        {
            ApuracaoAvaliacaoDomainService.AdicionarArquivoAta(seqAplicacaoAvaliacao, arquivoAnexado);
        }
    }
}
