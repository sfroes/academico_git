using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface ICampanhaOfertaService : ISMCService
    {
        /// <summary>
        /// Busca as oferta de campanha que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Oferta de campanhas que atendam aos filtros paginadas</returns>
        SMCPagerData<CampanhaOfertaData> BuscarCampanhasOfertaLookup(CampanhaOfertaFiltroData filtros);

        /// <summary>
        /// Busca as ofertas da campanha possíveis para associação pelo lookup de seleção de campanha_oferta.
        /// </summary>
        /// <param name="filtro">Filtro.</param>
        /// <returns></returns>
        SMCPagerData<SelecaoOfertaCampanhaLookupListaData> BuscarCampanhasOfertaSelecaoLookup(SelecaoOfertaCampanhaLookupFiltroData filtro);

        /// <summary>
        /// Busca os registro de campanha_oferta da campanha.
        /// </summary>
        /// <param name="filtro">Filtro.</param>
        /// <returns>Dados encontrados.</returns>
        SMCPagerData<CampanhaOfertaListaData> BuscarCampanhaOfertas(CampanhaOfertaFiltroTelaData filtro);

        /// <summary>
        /// Salva uma lista de vagas de campanha oferta.
        /// </summary>
        /// <param name="CampanhaOfertaFiltroTelaData">Dto</param>
        /// <returns>O sequencial da campanha.</returns>
        long SalvarVagasCampanhaOferta(VagasCampanhaOfertaData data);

        /// <summary>
        /// Busca as convocações que estão vinculadas a campanha e suas ofertas
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarCampanhaOfertasConvocacoes(CampanhaOfertaFiltroTelaData data);

        /// <summary>
        /// Associa as ofertas, passadas como parâmetro, a uma campanha
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Seq da campanha</returns>
        long AssociarCampanhaOferta(CampanhaOfertaAssociacaoData data);

        /// <summary>
        /// Antes de excluir ofertas da campanha, validar se não possuem associação a algum processo seletivo
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>True para exclusão; False caso possue restrição</returns>
        bool ValidarExclusao(long seq);

        /// <summary>
        /// RN_CAM_014 - Exclusão de Oferta da Campanha
        /// 1. Ao excluir ofertas da campanha, se a elas estiverem associadas a algum processo seletivo
        /// 1.1. Em caso afirmativo, excluir as ofertas das convocações e em seguida dos processos seletivos em
        /// que estiverem associadas.
        /// 1.2. Em caso negativo, abortar a operação.
        /// 2. Excluir as formações específicas associadas ao item de oferta, se existirem, em seguida excluir o
        /// item de oferta e em seguida a oferta da campanha.
        /// </summary>
        /// <param name="seq"></param>
        long Excluir(long seq);

    }
}