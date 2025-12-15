using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.Specifications;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using SMC.Framework.Specification;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class CampanhaService : SMCServiceBase, ICampanhaService
    {
        #region [ DomainServices ]

        private CampanhaDomainService CampanhaDomainService
        {
            get { return this.Create<CampanhaDomainService>(); }
        }

        private CicloLetivoDomainService CicloLetivoDomainService
        {
            get { return this.Create<CicloLetivoDomainService>(); }
        }

        private CampanhaOfertaDomainService CampanhaOfertaDomainService
        {
            get { return this.Create<CampanhaOfertaDomainService>(); }
        }

        private ProcessoSeletivoDomainService ProcessoSeletivoDomainService
        {
            get { return this.Create<ProcessoSeletivoDomainService>(); }
        }

        #endregion [ DomainServices ]

        public SMCPagerData<CampanhaData> BuscarCampanhas(CampanhaFiltroData filtros)
        {
            return CampanhaDomainService.BuscarCampanhas(filtros.Transform<CampanhaFilterSpecification>()).Transform<SMCPagerData<CampanhaData>>();
        }

        /// <summary>
        /// Busca uma campanha pelo sequencial.
        /// </summary>
        /// <param name="seq">Sequencial da campanha.</param>
        /// <returns></returns>
        public CampanhaData BuscarCampanha(long seq)
        {
            var spec = new SMCSeqSpecification<Campanha>(seq);
            return CampanhaDomainService.SearchProjectionByKey(spec,
                            x => new CampanhaData
                            {
                                Seq = x.Seq,
                                Descricao = x.Descricao,
                                SeqEntidadeResponsavel = x.SeqEntidadeResponsavel,
                                CiclosLetivos = x.CiclosLetivos.Select(f => new CampanhaCicloLetivoData
                                {
                                    Seq = f.SeqCicloLetivo,
                                    AnoCicloLetivo = f.CicloLetivo.Ano,
                                    NumeroCicloLetivo = f.CicloLetivo.Numero,
                                    Descricao = f.CicloLetivo.Descricao
                                }).ToList()
                            });
        }

        /// <summary>
        /// Busca uma campanha pelo sequencial do processo seletivo
        /// </summary>
        /// <param name="seqProcessoSeletivo">Sequencial do processo seletivo.</param>
        /// <returns></returns>
        public CampanhaData BuscarCampanhaProcessoSeletivo(long seqProcessoSeletivo)
        {
            return CampanhaDomainService.BuscarCampanhaProcessoSeletivo(seqProcessoSeletivo).Transform<CampanhaData>();
        }

        /// <summary>
        /// Salva uma campanha.
        /// </summary>
        /// <param name="campanha">Dto</param>
        /// <returns>O sequencial da nova campanha.</returns>
        public long SalvarCampanha(CampanhaData data)
        {
            var campanha = data.Transform<Campanha>();
            // Ajusta os sequenciais. Na tela, o lookup retorna o sequencial do ciclo_letivo e não campanha_ciclo_letivo .
            foreach (var cicloLetivo in campanha.CiclosLetivos)
            {
                cicloLetivo.SeqCicloLetivo = cicloLetivo.Seq;
                cicloLetivo.Seq = 0;
            }
            return CampanhaDomainService.SalvarCampanha(campanha);
        }

        /// <summary>
        /// Busca as camanhas que atendam aos filtros
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        public SMCPagerData<CampanhaData> BuscarCampanhasLookup(CampanhaFiltroData filtros)
        {
            return this.CampanhaDomainService.BuscarCampanhasLookup(filtros.Transform<CampanhaFilterSpecification>()).Transform<SMCPagerData<CampanhaData>>();
        }

        /// <summary>
        /// Busca as camanhas que atendam aos filtros
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados das campanhas encontradas</returns>
        public List<SMCDatasourceItem> BuscarCampanhasSelect(CampanhaFiltroData filtros)
        {
            return this.CampanhaDomainService.BuscarCampanhasSelect(filtros.Transform<CampanhaFilterSpecification>());
        }

        /// <summary>
        /// Busca os candidatos de uam campanha que atendam aos filtros
        /// </summary>
        /// <param name="filtros">Filtros da busca</param>
        /// <returns>Dados dos candidatos da campanha encontradas</returns>
        public SMCPagerData<CampanhaConsultarCandidatoListaData> BuscarCandidatosCampanha(CampanhaConsultarCandidatoFiltroData filtros)
        {
            var result = this.CampanhaDomainService.BuscarCandidatosCampanha(filtros.Transform<CampanhaConsultarCandidatoFiltroVO>());

            return result.Transform<SMCPagerData<CampanhaConsultarCandidatoListaData>>();
        }

        /// <summary>
        /// Busca o cabecalhgo da campanha
        /// </summary>
        /// <param name="seqCampanha">Seq da campanha</param>
        /// <returns>Cabeçalho com os dados da campanha</returns>
        public CampanhaConsultarCandidatoCabecalhoData BuscarCabecalhoCampanha(long seqCampanha)
        {
            var result = this.CampanhaDomainService.SearchByKey(new SMCSeqSpecification<Campanha>(seqCampanha));

            return result.Transform<CampanhaConsultarCandidatoCabecalhoData>();
        }

        /// <summary>
        /// Busca os registro de campanha_oferta da campanha.
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns>Dados encontrados</returns>
        public SMCPagerData<CampanhaCopiaOfertaListaData> BuscarCampanhaOfertas(CampanhaCopiaOfertaFiltroData filtro)
        {
            return CampanhaOfertaDomainService.BuscarCampanhaOfertas(filtro.Transform<CampanhaCopiaOfertaFiltroVO>())
                                              .Transform<SMCPagerData<CampanhaCopiaOfertaListaData>>();
        }

        public CampanhaCopiaData BuscarCampanhaOrigem(long SeqCampanhaOrigem)
        {
            return CampanhaDomainService.BuscarCampanhaOrigem(SeqCampanhaOrigem).Transform<CampanhaCopiaData>();
        }

        /// <summary>
        /// Busca os registro de processo_seletivo da campanha.
        /// </summary>
        /// <param name="filtro">Filtro</param>
        /// <returns>Dados encontrados</returns>
        public SMCPagerData<CampanhaCopiaProcessoSeletivoListaData> BuscarProcessosSeletivos(CampanhaCopiaProcessoSeletivoFiltroData filtro)
        {
            return ProcessoSeletivoDomainService.BuscarProcessosSeletivos(filtro.Transform<CampanhaCopiaProcessoSeletivoFiltroVO>())
                                              .Transform<SMCPagerData<CampanhaCopiaProcessoSeletivoListaData>>();
        }

        public void ValidarCampanhaCopiaCampanha(long seqCampanha, string descricaoCampanha, List<long> seqsCiclosLetivos)
        {
            CampanhaDomainService.ValidarCampanhaCopiaCampanha(seqCampanha, descricaoCampanha, seqsCiclosLetivos);
        }

        public void ValidarOfertaCopiaCampanha(long seqCampanha, List<long> seqsCiclosLetivos, List<long> seqsOfertas)
        {
            CampanhaDomainService.ValidarOfertaCopiaCampanha(seqCampanha, seqsCiclosLetivos, seqsOfertas);
        }

        public void ValidarProcessoSeletivoCopiaCampanha(List<long> seqsProcessosSeletivosSelecionados)
        {
            CampanhaDomainService.ValidarProcessoSeletivoCopiaCampanha(seqsProcessosSeletivosSelecionados);
        }

        public void ValidarProcessoGPICopiaCampanha(List<long> seqsOfertas, List<CampanhaCopiaProcessoSeletivoListaData> processosSeletivosGPI)
        {
            CampanhaDomainService.ValidarProcessoGPICopiaCampanha(seqsOfertas, processosSeletivosGPI.TransformList<CampanhaCopiaProcessoSeletivoListaVO>());
        }

        public long SalvarCopiaCampanha(CampanhaCopiaData model)
        {
            return CampanhaDomainService.SalvarCopiaCampanha(model.Transform<CampanhaCopiaVO>());
        }
    }
}