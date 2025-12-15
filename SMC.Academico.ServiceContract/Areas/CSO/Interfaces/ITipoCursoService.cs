using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ITipoCursoService : ISMCService
    {
        /// <summary>
        /// Busca a lista de tipo de curso por nivel ensino para popular um Select
        /// </summary>
        /// <returns>Lista de níveis de ensino</returns>
        List<TipoCurso> BuscarTiposCursoPorNivelEnsinoSelect(long seqNivelEnsino);
    }
}
