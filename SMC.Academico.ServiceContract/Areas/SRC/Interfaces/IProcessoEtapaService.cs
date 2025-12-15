using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.SRC.Interfaces
{
    public interface IProcessoEtapaService : ISMCService
    {
        ProcessoEtapaCabecalhoData BuscarCabecalhoProcessoEtapa(long seqProcessoEtapa);

        ProcessoEtapaData BuscarProcessoEtapa(long seqProcessoEtapa);

        /// <summary>
        /// Busca os dados para serem exibidos na mensagem de confirmação de encerramento de uma etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do processo Etapa a ser encerrado</param>
        /// <returns>Dados</returns>
        ProcessoEtapaData BuscarDadosEncerrarProcessoEtapa(long seqProcessoEtapa);

        List<SMCDatasourceItem> BuscarSituacoesEtapaPorProcessoEtapaSelect(long seqProcessoEtapa);

        List<SMCDatasourceItem> BuscarTiposPrazoAtendimentoEtapa(long seqProcessoEtapa);

        long SalvarProcessoEtapa(ProcessoEtapaData modelo);

        void ValidarModeloSalvar(ProcessoEtapaData modelo);

        bool ValidarAssertSalvar(ProcessoEtapaData modelo);

        bool ValidarAssertEscalonamentoBloqueiosEncerrarEtapa(long seqProcessoEtapa);

        /// <summary>
        /// Salva as etapas do SGF selecionadas na inclusão de um processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <param name="seqsEtapasSGF">Lista com os sequenciais das etapas do SGF selecionadas</param>
        void SalvarProcessoEtapaOrigemSGF(int seqProcesso, List<long> seqsEtapasSGF);

        /// <summary>
        /// Recupera o token do processo etapa para realiza validações na seleção de turma e de atividade
        /// </summary>
        /// <param name="seqProcessoEtapa"></param>
        /// <returns>token do processo etapa</returns>
        string BuscarTokenProcessoEtapa(long seqProcessoEtapa);

        /// <summary>
        /// Colocar Processo Etapa em manutencao
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do Processo Etapa</param>
        /// <returns>Modelo processo etapa</returns>
        ProcessoEtapaData ColocarProcessoEtapaManutencao(long seqProcessoEtapa);

        /// <summary>
        /// Liberar Processo Etapa
        /// </summary>
        /// <param name="seqProcessoEtapa">Sequencial do Processo Etapa</param>
        /// <returns>Modelo processo etapa</returns>
        ProcessoEtapaData LiberarProcessoEtapa(long seqProcessoEtapa);

        /// <summary>
        /// Buscar processo etapa por processo
        /// </summary>
        /// <param name="seqProcesso">Sequencial do processo</param>
        /// <returns>Lista dos processo etapas ordenado pelo campo ordem</returns>
        List<SMCDatasourceItem> BuscarProcessoEtapaPorProcessoSelect(long seqProcesso);

        /// <summary>
        /// Buscar processo etapa por serviço
        /// </summary>
        /// <param name="seqServico">Sequencial do serviço</param>
        /// <returns>Lista dos processo etapas ordenado pelo campo ordem</returns>
        List<SMCDatasourceItem> BuscarProcessoEtapaPorServicoSelect(long? seqServico);

        void EncerrarEtapa(long seqEtapaProcesso);

        bool ValidarDataEscalonamentoFinalProcesso(long seqProcessoEtapa);
    }
}