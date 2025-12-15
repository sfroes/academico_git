using SMC.Academico.Domain.Areas.APR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IAplicacaoAvaliacaoService : ISMCService
    {
        //SMCPagerData<AvaliacaoTrabalhoAcademicoListaData> BuscarAvaliacoesTrabalhoAcademico(AvaliacaoTrabalhoAcademicoFiltroData filtro);

        //SMCPagerData<AvaliacaoTrabalhoAcademicoListaData> BuscarAvaliacoesTrabalhoAcademico(long seq);

        List<AvaliacaoTrabalhoAcademicoListaData> BuscarListaComponenteAvaliacoesTrabalhoAcademico(long seqTrabalhoAcademico);

        AvaliacaoTrabalhoAcademicoBancaExaminadoraData BuscarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(AvaliacaoTrabalhoAcademicoFiltroData filtro);

        AvaliacaoTrabalhoAcademicoBancaExaminadoraData BuscarAvaliacoesTrabalhoAcademicoBancaExaminadoraInsert(AvaliacaoTrabalhoAcademicoBancaExaminadoraData model);

        List<BancasAgendadasData> BuscarBancasAgendadasPorPeriodo(BancasAgendadasFiltroData filtro);

        long SalvarAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(AvaliacaoTrabalhoAcademicoBancaExaminadoraData model);

        LancamentoNotaBancaExaminadoraData BuscarLancamentoNotaBancaExaminadoraInsert(AvaliacaoTrabalhoAcademicoFiltroData filtro);
        bool ApuracaoNota(long seqAplicacaoAvaliacao);

        bool CriterioAprovacaoAprovado(LancamentoNotaBancaExaminadoraData lancamento);

        AvaliacaoTrabalhoAcademicoBancaExaminadoraData BuscarDetalhesCancelamentoAplicacaoAvaliacaoBancaExaminadora(AvaliacaoTrabalhoAcademicoFiltroData filtro);

        void ExcluirAplicacaoAvaliacaoTrabalhoAcademicoBancaExaminadora(AvaliacaoTrabalhoAcademicoFiltroData filtro);

		bool ExibirMensagemAprovacaoPublicacaoBibliotecaObrigatoria(LancamentoNotaBancaExaminadoraData lancamentoNotaBancaExaminadoraVO);
        
        /// <summary>
        /// Buscar a proxima silga da avaliação
        /// </summary>
        /// <param name="filtro">Parametros de pesquisa</param>
        /// <returns>Sigla formatada</returns>
        string BuscarProximaSiglaAvaliacao(AplicacaoAvaliacaoFiltroData filtro);

        /// <summary>
        /// Buscar todas as aplicações avaliacoes
        /// </summary>
        /// <param name="seqOrigemAvaliacao">Parametros de pesquisa</param>
        /// <returns>Lista de aplicações avaliações</returns>
        List<AplicacaoAvaliacaoData> BuscarAplicacoesAvaliacoes(AplicacaoAvaliacaoFiltroData filtro);

        /// <summary>
        /// Busca a quantidade de avaliações aplicadas nas divisões de uma turma
        /// </summary>
        /// <param name="seqAlunoHistorico">Sequencial do aluno histórico</param>
        /// <param name="seqOrigemAvaliacao">Sequencial da origem de avaliação da turma do aluno</param>
        /// <returns>Quandidade de avaliações aplicadadas para as divisoes da origem informada</returns>
        int BuscarQuantidadeAvaliacoesAlunoPorOrigemTurma(long seqAlunoHistorico, long seqOrigemAvaliacao);       
    }
}
