using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Domain.Models;
using SMC.Academico.ServiceContract.Areas.ORT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORT.Interfaces
{
    public interface ITrabalhoAcademicoService : ISMCService
    {
        #region Métodos Academico

        SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhosAcademicos(TrabalhoAcademicoFiltroData filtro);

        SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhosAcademicosLiberacaoConsulta(TrabalhoAcademicoFiltroData filtro);

        long SalvarTrabalhoAcademico(TrabalhoAcademicoData trabalhoAcademico);

        long SalvarAlteracoesLiberacaoConsultaBdp(TrabalhoAcademicoPublicacaoBdpData trabalhoAcademico);

        TrabalhoAcademicoData AlterarTrabalhoAcademico(long seq);

        void ExcluirTrabalhoAcademico(long seq);

        TrabalhoAcademicoPublicacaoBdpData AlterarTrabalhoAcademicoPublicacaoBdp(long seq);

        string FormatarNome(long seqAutor);

        CabecalhoPublicacaoBdpData BuscarCabecalhoPublicacaoBdp(long seq);

        AvaliacaoTrabalhoAcademicoCabecalhoData BuscarTrabalhoAcademicoCabecalho(long seq);

        string UrlPublicacao(long seqTrabalhoAcademico, string nomeArquivo);

        /// <summary>
        /// Busca o comprovante de entrega para cada autoria do trabalho academico
        /// </summary>
        /// UC_ORT_002_02_06 - Emitir de Comprovante de Entrega
        ///
        /// <param name="seq"></param>
        /// <returns></returns>
        List<ComprovanteEntregaTrabalhoAcademicoData> BuscarComprovantesEntregaTrabalhoAcademico(long seq);

        /// <summary>
        /// Busca o relatório de comprovante de entrega para cada autoria do trabalho academico
        /// </summary>
        /// UC_ORT_002_02_06 - Emitir de Comprovante de Entrega
        ///
        /// <param name="seq"></param>
        /// <returns></returns>
        byte[] BuscarRelatorioEntregaTrabalhoAcademico(long seq);

        #endregion Métodos Academico

        long BuscarSeqAlunoTrabalhoAcademico(long seqTrabalhoAcademico);

        DateTime? DataPublicacaoBdpTrabalhoAcademico(long seqTrabalhoAcademico);

        SituacaoAlunoFormacao? BuscarSituacaoAlunoFormacaoHistorico(long seqTrabalhoAcademico);

        /// <summary>
        /// Método que verifica se existe alguma avaliação, cadastrada para o trabalho acadêmico
        /// </summary>
        /// <param name="seqTrabalhoAcademico"></param>
        /// <returns>true, false</returns>
        bool ExisteAvaliacao(long seqTrabalhoAcademico);

        /// <summary>
        /// Verifica se a data de depósito é igual a data atual ou maior/igual que a data referente ao sexto dia útil do mês corrente.
        /// </summary>
        bool ValidarDataMinimaDepositoSecretaria(DateTime dataDepositoSecretaria);

        /// <summary>
        /// Recupera o Tipo de trabalho de conclusão e o seq divisão de componente deste trabalho.
        /// </summary>
        /// <param name="seqAluno">Sequencial do aluno</param>
        (long SeqTipoTrabalho, long? SeqDivisaoComponente) RecuperarDadosTipoTrabalhoAcademico(long seqAluno);

        /// <summary>
        /// Validar situação do Trabalho Academico.
        /// </summary>
        /// <param name="trabalho">Modelo de trabalho academico a ser salvo</param>
        /// <returns>Retorna se true se a situacao for valida</returns>
        bool ValidarSituacaoTrabalho(TrabalhoAcademicoData trabalho);


        /// <summary>
        /// Inclui autorização de novo deposito recebendo os parametros de trabalho
        /// </summary>
        /// <param name="seqTrabalhoAcademico"></param>
        /// <param name="justificativa"></param>
        /// <param name="dataAutorizacao"></param>
        /// <param name="arquivo"></param>
        void IncluirSegundoDeposito(long seqTrabalhoAcademico, string justificativa, DateTime dataAutorizacao, ArquivoAnexado arquivo);
        bool AtendeRegraHabilitarAgendamentoBanca(long seqTrabalhoAcademico);
        
        #region Métodos BDP

        SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhoAcademicosBDP(TrabalhoAcademicoFiltroData filtro);

        /// <summary>
        /// Buscar Trabalhos com futuras defesas
        /// </summary>
        /// <param name="filtro">Filtros da pesquisa</param>
        /// <returns>Lista paginada do trabalho</returns>
        SMCPagerData<TrabalhoAcademicoListaData> BuscarTrabalhoFuturasDefesasAcademicosBDP(TrabalhoAcademicoFiltroData filtro);

        VisualizarTrabalhoAcademicoData VisualizarTrabalhoAcademico(long seq);

		bool VerificaPublicacaoBdpIdioma(long seqTrabalhoAcademico);


		#endregion Métodos BDP
	}
}