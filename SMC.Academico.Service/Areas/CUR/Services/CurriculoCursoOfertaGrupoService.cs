using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Academico.Domain.Areas.CUR.DomainServices;
using SMC.Academico.Domain.Areas.CUR.Models;
using SMC.Academico.Domain.Areas.CUR.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Academico.ServiceContract.Areas.CUR.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.Service.Areas.CUR.Services
{
    public class CurriculoCursoOfertaGrupoService : SMCServiceBase, ICurriculoCursoOfertaGrupoService
    {
        #region [ DomainService ]

        private CurriculoCursoOfertaGrupoDomainService CurriculoCursoOfertaGrupoDomainService
        {
            get { return this.Create<CurriculoCursoOfertaGrupoDomainService>(); }
        }

        private GrupoCurricularDomainService GrupoCurricularDomainService
        {
            get { return this.Create<GrupoCurricularDomainService>(); }
        }

        #endregion [ DomainService ]

        /// <summary>
        /// Busca os Grupos Curriculares de um currículo curso oferta e popula um select
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial de currículo curso oferta</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Select com os grupos curriculares</returns>
        public List<SMCDatasourceItem> BuscarGruposCurricularesCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta, long? seqComponenteCurricular = null)
        {
           return GrupoCurricularDomainService.BuscarGruposCurricularesCurriculoCursoOfertaSelect(seqCurriculoCursoOferta, seqComponenteCurricular);
        }

        /// <summary>
        /// Busca os Grupos Curriculares de um currículo curso oferta
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial de currículo curso oferta</param>
        /// <returns>Array com dados dos grupos curriculares e componentes do currículo curso oferta</returns>
        public GrupoCurricularListaData[] BuscarGruposCurricularesTreeCurriculoCursoOferta(long seqCurriculoCursoOferta)
        {
            var grupoCurricularVO = GrupoCurricularDomainService.BuscarGruposCurricularesTreeCurriculoCursoOferta(seqCurriculoCursoOferta);
            return grupoCurricularVO.TransformToArray<GrupoCurricularListaData>();
        }

        /// <summary>
        /// Exclui a associação com o grupo curricular (e sua hierarquia) e também as divisões de componentes filhos dos grupos
        /// nas matrizes associadas à oferta de curso excluída
        /// </summary>
        /// <param name="seq">Sequencial do grupo currícular curso oferta</param>
        public void ExcluirCurriculoCursoOfertaGrupo(long seq)
        {
            CurriculoCursoOfertaGrupoDomainService.ExcluirCurriculoCursoOfertaGrupo(seq);
        }

        /// <summary>
        /// Busca as quantidades de créditos e carga horária disponíveis para associação de grupos à uma oferta de curso da matriz
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta</param>
        /// <returns>Quantidades disponíveis para associação de componentes currículares</returns>
        public CurriculoCursoOfertaGrupoData BuscarQuantidadesDisponiveis(long seqCurriculoCursoOferta)
        {
            return this.CurriculoCursoOfertaGrupoDomainService.BuscarQuantidadesDisponiveis(seqCurriculoCursoOferta).Transform<CurriculoCursoOfertaGrupoData>();
        }

        /// <summary>
        /// Calcula o valor de um grupo curricular em carga horária e créditos
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequncial da oferta de curso pai do CurriculoCursoOfertaGrupo</param>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular a ser calculado</param>
        /// <param name="incremental">Define se deverá desconsiderar o valor de subgrupos e componentes vinculadoas anteriormente</param>
        /// <returns>Valor de carga horária e créditos dos componentes associados ao grupo curricular e seus subgrupos</returns>
        public CurriculoCursoOfertaGrupoValorData BuscarValorCurriculoCursoOfertaGrupo(long seqCurriculoCursoOferta, long seqGrupoCurricular, bool incremental)
        {
            return this.CurriculoCursoOfertaGrupoDomainService.BuscarValorCurriculoCursoOfertaGrupo(seqCurriculoCursoOferta, seqGrupoCurricular, incremental).Transform<CurriculoCursoOfertaGrupoValorData>();
        }

        /// <summary>
        /// Salvar o curriculo curso oferta grupo
        /// </summary>
        /// <param name="curriculoCursoOfertaGrupo"></param>
        /// <returns>Sequencial do curriculo curso oferta grupo</returns>
        public void SalvarCurriculoCursoOfertaGrupo(CurriculoCursoOfertaGrupoData curriculoCursoOfertaGrupo)
        {
            var curriculoCursoOfertaGrupoModelo = curriculoCursoOfertaGrupo.Transform<CurriculoCursoOfertaGrupo>();
            curriculoCursoOfertaGrupoModelo.Obrigatorio = curriculoCursoOfertaGrupo.Obrigatorio == CurriculoCursoOfertaGrupoComponenteObrigatorio.Obrigatorios;

            CurriculoCursoOfertaGrupoDomainService.SalvarCurriculoCursoOfertaGrupo(curriculoCursoOfertaGrupoModelo);
        }

        /// <summary>
        /// Busca um grupo curricular curso oferta
        /// </summary>
        /// <param name="filtroData">Dados do filtro</param>
        /// <returns>Dados do grupo currícular curso oferta</returns>
        public CurriculoCursoOfertaGrupoData BuscarCurriculoCursoOfertaGrupo(CurriculoCursoOfertaGrupoFiltroData filtroData)
        {
            var curriculoCursoOfertaGrupoModelo = this.CurriculoCursoOfertaGrupoDomainService.BuscarCurriculoCursoOfertaGrupo(filtroData.Transform<CurriculoCursoOfertaGrupoFiltroVO>());
            var curriculoCursoOfertaGrupoData = curriculoCursoOfertaGrupoModelo.Transform<CurriculoCursoOfertaGrupoData>();

            if (curriculoCursoOfertaGrupoModelo != null)
            {
                curriculoCursoOfertaGrupoData.Obrigatorio = curriculoCursoOfertaGrupoModelo.Obrigatorio ?
            CurriculoCursoOfertaGrupoComponenteObrigatorio.Obrigatorios : CurriculoCursoOfertaGrupoComponenteObrigatorio.Optativos;

                curriculoCursoOfertaGrupoData.QuantidadesDisponiveis = this.CurriculoCursoOfertaGrupoDomainService
                    .BuscarQuantidadesDisponiveis(curriculoCursoOfertaGrupoModelo.SeqCurriculoCursoOferta)
                    .Transform<CurriculoCursoOfertaGrupoData>()
                    .QuantidadesDisponiveis;
            }

            return curriculoCursoOfertaGrupoData;
        }

        /// <summary>
        /// Busca os curriculo curso ofertas grupos com seus respectivos grupos curriculares de uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// /// <returns>Dados dos grupos curriculares</returns>
        public List<SMCDatasourceItem> BuscarCurriculoCursoOfertasGruposSelect(long seq, long seqMatrizCurricular)
        {
            return CurriculoCursoOfertaGrupoDomainService.BuscarCurriculoCursoOfertasGruposSelect(seq, seqMatrizCurricular);
        }

        /// <summary>
        /// Busca o tipo de configuração e as quantidades de um curriculo curso oferta grupo
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        /// <returns>Dados do curriculo curso oferta grupo</returns>
        public CurriculoCursoOfertaGrupoTipoConfiguracaoData BuscarCurriculoCursoOfertaGrupoComTipoConfiguracao(long seq)
        {
            var curriculoCursoOfertaGrupoVO = CurriculoCursoOfertaGrupoDomainService.BuscarCurriculoCursoOfertaGrupoComTipoConfiguracao(seq);
            return curriculoCursoOfertaGrupoVO.Transform<CurriculoCursoOfertaGrupoTipoConfiguracaoData>();
        }
    }
}