using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ITipoFormacaoEspecificaService : ISMCService
    {
        /// <summary>
        /// Busca um tipo de formação específica pelo sequencial
        /// </summary>
        /// <param name="seq">Sequencial do tipo de formação específica</param>
        /// <returns>Tipo de formação específica do sequencial solicitado</returns>
        TipoFormacaoEspecificaData BuscarTipoFormacaoEspecifica(long seq);

        /// <summary>
        /// Busca a lista de formações específicas para popular um Select
        /// </summary>
        /// <param name="classeTipoFormacao">Classe do tipo de formação específica</param>
        /// <returns>Lista de tipos de formação especifica</returns>
        List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaSelect(TipoFormacaoEspecificaFiltroData filtro);

        /// <summary>
        /// Busca a lista de formações específicas para popular um Select por nível de ensino e instituição
        /// </summary>
        /// <param name="filtro">Filtro da Instituição nível por tipo de formação específica</param>
        /// <returns>Lista de tipos de formação especifica</returns>
        List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaPorNivelEnsinoSelect(TipoFormacaoEspecificaPorNivelEnsinoFiltroData filtro);

        /// <summary>
        /// Busca a lista de formações epecíficas de um tipo de entidade num nível da hierarquia
        /// </summary>
        /// <param name="seqTipoEntidade">Sequencial do tipo da entidade</param>
        /// <param name="seqFormacaoEspecificaSuperior">Sequencial da formação específica superior</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarTipoFormacaoEspecificaEntidadeSelect(long seqTipoEntidade, long? seqFormacaoEspecificaSuperior);

        /// <summary>
        /// Grava um tipo de formação específica
        /// </summary>
        /// <param name="modelo">Tipo de formação específica a ser gravada</param>
        /// <returns>Sequencial do tipo de formação específica gravada</returns>
        long Salvar(TipoFormacaoEspecificaData modelo);
    }
}