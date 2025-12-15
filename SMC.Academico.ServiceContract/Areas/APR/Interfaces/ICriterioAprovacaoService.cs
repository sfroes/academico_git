using SMC.Academico.ServiceContract.Areas.APR.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface ICriterioAprovacaoService : ISMCService
    {
        /// <summary>
        /// Busca o critério de aprovação selecionado
        /// </summary>
        /// <param name="seq">Sequencial do critério de aprovação</param>
        /// <returns>Dados dos critério de aprovação</returns>
        CriterioAprovacaoData BuscarCriterioAprovacao(long seq);

        /// <summary>
        /// Busca os critérios de aprovação que sejam do nível de ensino do curso do curriculo curso oferta informado
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do currículo curso oferta do curso com o nível de ensino em questão</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        List<SMCDatasourceItem> BuscarCriteriosAprovacaoNivelEnsinoPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta);

        /// <summary>
        /// Busca os critérios de aprovação que sejam do nível de ensino da configuração do componente
        /// </summary>
        /// <param name="seqConfiguracaoComponente">Sequencial da configuração do componente com o nível de ensino em questão</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        List<SMCDatasourceItem> BuscarCriteriosAprovacaoNivelEnsinoPorConfiguracaoComponenteSelect(long seqConfiguracaoComponente);

        long SalvarCriterioAprovacao(CriterioAprovacaoData criterioAprovacao);

        /// <summary>
        /// Recupera os critérios de aprovação pelos filtros informados
        /// </summary>
        /// <param name="filtroData">Dados dos filtros</param>
        /// <returns>Dados dos critérios de aprovação</returns>
        List<SMCDatasourceItem> BuscarCriteriosAprovacaoSelect(CriterioAprovacaoFiltroData filtroData);
    }
}