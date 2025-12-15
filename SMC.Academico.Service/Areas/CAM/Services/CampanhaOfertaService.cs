using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class CampanhaOfertaService : SMCServiceBase, ICampanhaOfertaService
    {
        #region [ DomainService ]

        public CampanhaOfertaDomainService CampanhaOfertaDomainService
        {
            get { return this.Create<CampanhaOfertaDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca as oferta de campanha que atendam aos filtros informados
        /// </summary>
        /// <param name="filtros">Dados dos filtros</param>
        /// <returns>Oferta de campanhas que atendam aos filtros paginadas</returns>
        public SMCPagerData<CampanhaOfertaData> BuscarCampanhasOfertaLookup(CampanhaOfertaFiltroData filtros)
        {
            return this.CampanhaOfertaDomainService.BuscarCampanhasOfertaLookup(filtros.Transform<CampanhaOfertaFiltroVO>()).Transform<SMCPagerData<CampanhaOfertaData>>();
        }

        /// <summary>
        /// Busca as ofertas da campanha possíveis para associação pelo lookup de seleção de campanha_oferta.
        /// </summary>
        /// <param name="filtro">Filtro.</param>
        /// <returns></returns>
        public SMCPagerData<SelecaoOfertaCampanhaLookupListaData> BuscarCampanhasOfertaSelecaoLookup(SelecaoOfertaCampanhaLookupFiltroData filtro)
        {
            return CampanhaOfertaDomainService.BuscarCampanhasOfertaSelecaoLookup(filtro.Transform<SelecaoOfertaCampanhaLookupFiltroVO>())
                                               .Transform<SMCPagerData<SelecaoOfertaCampanhaLookupListaData>>();
        }

        /// <summary>
        /// Busca os registro de campanha_oferta da campanha.
        /// </summary>
        /// <param name="filtro">Filtro.</param>
        /// <returns>Dados encontrados.</returns>
        public SMCPagerData<CampanhaOfertaListaData> BuscarCampanhaOfertas(CampanhaOfertaFiltroTelaData filtro)
        {
            return CampanhaOfertaDomainService.BuscarCampanhaOfertas(filtro.Transform<CampanhaOfertaFiltroTelaVO>())
                                                .Transform<SMCPagerData<CampanhaOfertaListaData>>();
        }
        
        public long SalvarVagasCampanhaOferta(VagasCampanhaOfertaData data)
        {
            return CampanhaOfertaDomainService.SalvarVagasCampanhaOferta(data.Transform<VagasCampanhaOfertaVO>());
        }

        public List<SMCDatasourceItem> BuscarCampanhaOfertasConvocacoes(CampanhaOfertaFiltroTelaData data)
        {
            return CampanhaOfertaDomainService.BuscarCampanhaOfertasConvocacoes(data.Transform<CampanhaOfertaFiltroTelaVO>());
        }

        /// <summary>
        /// Associa as ofertas, passadas como parâmetro, a uma campanha
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Seq da campanha</returns>
        public long AssociarCampanhaOferta(CampanhaOfertaAssociacaoData data)
        {
            return CampanhaOfertaDomainService.AssociarCampanhaOferta(data.Transform<CampanhaOfertaAssociacaoVO>());
        }

        /// <summary>
        /// Antes de excluir ofertas da campanha, validar se não possuem associação a algum processo seletivo
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>True para exclusão; False caso possue restrição</returns>
        public bool ValidarExclusao(long seq)
        {
            return CampanhaOfertaDomainService.ValidarExclusao(seq);
        }

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
        public long Excluir(long seq)
        {
            return CampanhaOfertaDomainService.Excluir(seq);
        }
    }
}