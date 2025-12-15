using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface ICampanhaService : ISMCService
    {
        /// <summary>
        /// Busca as camanhas que atendam aos filtros.
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        SMCPagerData<CampanhaData> BuscarCampanhas(CampanhaFiltroData filtros);

        /// <summary>
        /// Busca uma campanha pelo sequencial.
        /// </summary>
        /// <param name="seq">Sequencial da campanha.</param>
        /// <returns></returns>
        CampanhaData BuscarCampanha(long seq);

        /// <summary>
        /// Busca uma campanha pelo sequencial do processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo seletivo.</param>
        /// <returns></returns>
        CampanhaData BuscarCampanhaProcessoSeletivo(long seqProcessoSeletivo);

        /// <summary>
        /// Salva uma campanha.
        /// </summary>
        /// <param name="campanha">Dto</param>
        /// <returns>O sequencial da nova campanha.</returns>
        long SalvarCampanha(CampanhaData campanha);

        /// <summary>
        /// Busca as camanhas que atendam aos filtros para lookup.
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        SMCPagerData<CampanhaData> BuscarCampanhasLookup(CampanhaFiltroData filtros);

        /// <summary>
        /// Busca as camanhas que atendam aos filtros
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        List<SMCDatasourceItem> BuscarCampanhasSelect(CampanhaFiltroData filtros);

        /// <summary>
        /// Busca os candidatos que atendam aos filtros
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados dos candidatos da campanha encontrados</returns>
        SMCPagerData<CampanhaConsultarCandidatoListaData> BuscarCandidatosCampanha(CampanhaConsultarCandidatoFiltroData filtros);

        /// <summary>
        /// Busca o cabecalhgo da campanha
        /// </summary>
        /// <param name="seqCampanha">Seq da campanha</param>
        /// <returns>Cabeçalho com os dados da campanha</returns>
        CampanhaConsultarCandidatoCabecalhoData BuscarCabecalhoCampanha(long seqCampanha);

        /// <summary>
        /// Busca os registro de campanha_oferta da campanha.
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns>Dados encontrados.</returns>
        SMCPagerData<CampanhaCopiaOfertaListaData> BuscarCampanhaOfertas(CampanhaCopiaOfertaFiltroData filtro);

        /// <summary>
        /// Busca os registro de processo_seletivo da campanha.
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns>Dados encontrados.</returns>
        SMCPagerData<CampanhaCopiaProcessoSeletivoListaData> BuscarProcessosSeletivos(CampanhaCopiaProcessoSeletivoFiltroData filtro);

        /// <summary>
        /// Busca o registro de campanha origem para cópia de campanha
        /// </summary>
        /// <param name="SeqCampanhaOrigem">Sequencial da campanha origem</param>
        /// <returns>Campanha origem encontrada</returns>
        CampanhaCopiaData BuscarCampanhaOrigem(long SeqCampanhaOrigem);

        /// <summary>
        /// Salva uma cópia de campanha
        /// </summary>
        /// <param name="model">Modelo para ser gravado</param>
        /// <returns>Sequencial da campanha criada</returns>
        long SalvarCopiaCampanha(CampanhaCopiaData model);

        void ValidarCampanhaCopiaCampanha(long seqCampanhaOrigem, string descricaoCampanhaOrigem, List<long> seqsCiclosLetivosDestino);

        void ValidarOfertaCopiaCampanha(long seqCampanhaOrigem, List<long> seqsCiclosLetivosDestino, List<long> seqsOfertasSelecionadas);

        void ValidarProcessoSeletivoCopiaCampanha(List<long> seqsProcessosSeletivosSelecionados);

        void ValidarProcessoGPICopiaCampanha(List<long> seqsOfertas, List<CampanhaCopiaProcessoSeletivoListaData> processosSeletivosGPI);
    }
}