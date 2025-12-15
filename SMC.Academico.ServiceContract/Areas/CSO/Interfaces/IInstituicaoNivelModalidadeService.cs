using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IInstituicaoNivelModalidadeService : ISMCService
    {
        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição nível ensino
        /// </summary>
        /// <param name="seqInstituicaoNivelEnsino">Sequencial da instituição nível ensino</param>
        /// <returns>Lista de modalidades</returns>
        List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoNivelEnsinoSelect(long seqInstituicaoNivelEnsino);

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição nível ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial da instituição nível ensino</param>
        /// <returns>Lista de modalidades</returns>
        List<SMCDatasourceItem> BuscarModalidadesPorNivelEnsinoSelect(long seqNivelEnsino);

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição
        /// </summary>
        /// <returns>Lista de modalidades</returns>
        List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoSelect();

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o instituição logada
        /// </summary>
        /// <param name="seqInstituicao">Sequencial da instituição</param>
        /// <returns>Lista de modalidades</returns>
        List<SMCDatasourceItem> BuscarModalidadesPorInstituicaoLogadaSelect(long seqInstituicao);
    }
}