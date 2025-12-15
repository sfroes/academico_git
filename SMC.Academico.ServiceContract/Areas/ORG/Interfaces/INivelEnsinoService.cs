using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface INivelEnsinoService : ISMCService
    {
        /// <summary>
        /// Busca os níveis de ensino
        /// </summary>
        /// <returns>Lista com os níveis de ensino</returns>
        List<NivelEnsinoHierarquiaData> BuscarNiveisEnsino();

        /// <summary>
        /// Busca os sequenciais dos níveis de ensino utilizados no stricto sensu
        /// </summary>
        /// <returns>Lista com os sequenciais dos níveis de ensino</returns>
        List<long> BuscarSeqsNiveisEnsinoStrictoSensu();

        /// <summary>
        /// Busca os níveis de ensino dado um ciclo letivo
        /// </summary>
        /// <param name="filtro">Filtro a ser considerado na busca</param>
        /// <returns>Lista dos níveis de ensino encontrados</returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoPorCicloLetivoSelect(long seqCicloLetivo);

        /// <summary>
        /// Busca o nivel de ensino pelo Seq
        /// </summary>
        /// <param name="seqNivelEnsino">Seq do nível de ensino</param>
        /// <returns>Nivel de ensino encontrado</returns>
        NivelEnsinoData BuscarNivelEnsino(long seqNivelEnsino);

        /// <summary>
        ///  Lista de Níveis de acordo com o que foi parametrizado para a Instituição da Parceria em questão
        ///  e a regra RN_USG_004 - Filtro por Nível de Ensino.
        /// </summary>
        /// <param name="seqParceriaIntercambio"></param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoParceriaIntercambioSelect(long seqParceriaIntercambio);

        /// <summary>
        /// Lista de Nívels de acordo com o que foi parametrizado para o Serviço, em Instituição Nivel Serviço
        /// </summary>
        /// <param name="seqServico"></param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoPorServicoSelect(long seqServico);
    }
}