using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IProgramaTipoAutorizacaoBdpService : ISMCService
    {

        /// <summary>
        /// Buscar os tipos de autorização da publicação bpd por programa
        /// </summary>
        /// <param name="seqPrograma">Sequencial do programa</param>
        /// <returns>Lista select dos tipos de autorização</returns>
        List<SMCDatasourceItem> BuscarTipoAutorizacaoPorProgramaSelect(long seqPrograma);
    }
}