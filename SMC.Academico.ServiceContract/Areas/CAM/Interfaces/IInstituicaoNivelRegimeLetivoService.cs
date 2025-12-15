using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface IInstituicaoNivelRegimeLetivoService : ISMCService
    {
        /// <summary>
        /// Busca a lista de níveis de ensino da instituição de ensino logada no regime informado
        /// </summary>
        /// <param name="seqRegimeLetivo">Sequencial do regime letivo</param>
        /// <returns>Lista de níveis de ensino com sequencial do NivelEnsino</returns>
        List<SMCDatasourceItem> BuscarNiveisEnsinoDoRegimeSelect(long seqRegimeLetivo);
    }
}
