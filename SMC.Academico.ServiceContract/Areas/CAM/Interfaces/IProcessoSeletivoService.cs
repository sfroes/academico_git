using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IProcessoSeletivoService : ISMCService
    {
        List<SMCDatasourceItem> BuscarProcessosSeletivosSelect();

        /// <summary>
        /// Busca os processos letivos de uma campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos processos seletivos</returns>
        List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaSelect(long seqCampanha);

        /// <summary>
        /// Busca os processos letivos de uma campanha
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <returns>Dados dos processos seletivos</returns>
        List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaIngressoDiretoSelect(long seqCampanha);

        /// <summary>
        /// Busca os processos letivos de um tipo de processo seletivo
        /// </summary>
        /// <param name="seqCampanha">Sequencial da campanha</param>
        /// <param name="seqTipoProcessoSeletivo">Sequencial do tipo de processo seletivo</param>
        /// <returns>Dados dos processos seletivos</returns>
        List<SMCDatasourceItem> BuscarProcessosSeletivosPorCampanhaTipoProcessoSeletivoSelect(long seqCampanha, long? seqTipoProcessoSeletivo = null);

        /// <summary>
        /// Busca os processos seletivos.
        /// </summary>
        /// <param name="filtro"></param>
        /// <returns></returns>
        SMCPagerData<ProcessoSeletivoListaData> BuscarProcessosSeletivos(ProcessoSeletivoFiltroData filtro);

        ProcessoSeletivoData BuscarProcessosSeletivo(long seq);

        long SalvarProcessoSeletivo(ProcessoSeletivoData processo);

        ProcessoSeletivoCabecalhoData BuscarProcessosSeletivoCabecalho(long seqProcessoSeletivo);

        /// <summary>
        /// Método que lista os processos seletivos e suas respectivas convocações, da campanha
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <returns>List<ProcessoSeletivoData></returns>
        List<ProcessoSeletivoData> BuscarProcessosSeletivosConvocacao(long seqCampanha);

        /// <summary>
        /// Método que lista os processos seletivos e suas respectivas convocações, da campanha
        /// </summary>
        /// <param name="seqsProcessosSeletivos">Sequenciais dos processos seletivos</param>
        /// <returns>List<CampanhaCopiaProcessoSeletivoData></returns>
        List<CampanhaCopiaConvocacaoProcessoSeletivoData> BuscarConvocacoesProcessosSeletivos(long[] seqsProcessosSeletivos);

        List<CampanhaCopiaEtapaProcessoGPIData> BuscarEtapasProcessosGPI(long[] seqsProcessosSeletivos);

        void SalvarOfertaProcessoSeletivo(ProcessoSeletivoOfertaData data);

        ProcessoSeletivoData NovoProcessosSeletivo(long seqCampanha);
    }
}