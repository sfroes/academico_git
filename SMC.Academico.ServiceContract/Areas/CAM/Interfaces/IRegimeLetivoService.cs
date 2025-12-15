using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IRegimeLetivoService : ISMCService
    {
        /// <summary>
        /// Salva um regime letivo
        /// </summary>
        /// <param name="regimeLetivo">Dados do regime letivo a ser salva</param>
        /// <returns>Sequencial do regime letivo salva</returns>
        long SalvarRegimeLetivo(RegimeLetivoData regimeLetivo);

        /// <summary>
        /// Busca os regimes letivos disponíveis para programas stricto sensu
        /// </summary>
        /// <returns>Lista de regimes letivos</returns>
        List<SMCDatasourceItem> BuscarRegimesLetivosStrictoSelect();

        /// <summary>
        /// Busca a lista de regime letivo de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de regime letivo</returns>
        List<SMCDatasourceItem> BuscarRegimesLetivoPorNivelEnsinoSelect(long seqNivelEnsino);

        /// <summary>
        /// Busca todos os regimes letivos da intituição atual
        /// </summary>
        /// <returns>Lista com os dados de select dos regimes da instituição atual</returns>
        List<SMCDatasourceItem> BuscarRegimesLetivosInstituicaoSelect();
    }
}
