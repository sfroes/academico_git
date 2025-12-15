using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ICurriculoCursoOfertaGrupoService : ISMCService
    {
        /// <summary>
        /// Busca os Grupos Curriculares de um currículo curso oferta e popula um select
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial de currículo curso oferta</param>
        /// <param name="seqComponenteCurricular">Sequencial do componente curricular</param>
        /// <returns>Select com os grupos curriculares</returns>
        List<SMCDatasourceItem> BuscarGruposCurricularesCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta, long? seqComponenteCurricular = null);

        /// <summary>
        /// Busca os Grupos Curriculares de um currículo curso oferta
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial de currículo curso oferta</param>
        /// <returns>Array com dados dos grupos curriculares e componentes do currículo curso oferta</returns>
        GrupoCurricularListaData[] BuscarGruposCurricularesTreeCurriculoCursoOferta(long seqCurriculoCursoOferta);

        /// <summary>
        /// Exclui a associação com o grupo curricular (e sua hierarquia) e também as divisões de componentes filhos dos grupos
        /// nas matrizes associadas à oferta de curso excluída
        /// </summary>
        /// <param name="seq">Sequencial do grupo currícular curso oferta</param>
        void ExcluirCurriculoCursoOfertaGrupo(long seq);

        /// <summary>
        /// Busca as quantidades de créditos e carga horária disponíveis para associação de grupos à uma oferta de curso da matriz
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta</param>
        /// <returns>Quantidades disponíveis para associação de componentes currículares</returns>
        CurriculoCursoOfertaGrupoData BuscarQuantidadesDisponiveis(long seqCurriculoCursoOferta);

        /// <summary>
        /// Calcula o valor de um grupo curricular em carga horária e créditos
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequncial da oferta de curso pai do CurriculoCursoOfertaGrupo</param>
        /// <param name="seqGrupoCurricular">Sequencial do grupo curricular a ser calculado</param>
        /// <param name="incremental">Define se deverá desconsiderar o valor de subgrupos e componentes vinculadoas anteriormente</param>
        /// <returns>Valor de carga horária e créditos dos componentes associados ao grupo curricular e seus subgrupos</returns>
        CurriculoCursoOfertaGrupoValorData BuscarValorCurriculoCursoOfertaGrupo(long seqCurriculoCursoOferta, long seqGrupoCurricular, bool incremental);

        /// <summary>
        /// Salvar o curriculo curso oferta grupo
        /// </summary>
        /// <param name="curriculoCursoOfertaGrupo"></param>
        /// <returns>Sequencial do curriculo curso oferta grupo</returns>
        void SalvarCurriculoCursoOfertaGrupo(CurriculoCursoOfertaGrupoData curriculoCursoOfertaGrupo);

        /// <summary>
        /// Busca um grupo curricular curso oferta
        /// </summary>
        /// <param name="filtroData">Dados do filtro</param>
        /// <returns>Dados do grupo currícular curso oferta</returns>
        CurriculoCursoOfertaGrupoData BuscarCurriculoCursoOfertaGrupo(CurriculoCursoOfertaGrupoFiltroData filtroData);

        /// <summary>
        /// Busca os curriculo curso ofertas grupos com seus respectivos grupos curriculares de uma matriz curricular
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// /// <returns>Dados dos grupos curriculares</returns>
        List<SMCDatasourceItem> BuscarCurriculoCursoOfertasGruposSelect(long seq, long seqMatrizCurricular);

        /// <summary>
        /// Busca o tipo de configuração e as quantidades de um curriculo curso oferta grupo
        /// </summary>
        /// <param name="seq">Sequencial do curriculo curso oferta grupo</param>
        /// <returns>Dados do curriculo curso oferta grupo</returns>
        CurriculoCursoOfertaGrupoTipoConfiguracaoData BuscarCurriculoCursoOfertaGrupoComTipoConfiguracao(long seq);
    }
}