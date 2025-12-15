using SMC.Framework.Service;
using System.Collections.Generic;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ITipoDivisaoCurricularService : ISMCService
    {
        /// <summary>
        /// Busca a lista de tipo de divisao curricular de acordo com o nível de ensino
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Lista de tipos de divisão curricular</returns>
        List<SMCDatasourceItem> BuscarTiposDivisaoCurricularNivelEnsinoSelect(long seqNivelEnsino);
    }
}
