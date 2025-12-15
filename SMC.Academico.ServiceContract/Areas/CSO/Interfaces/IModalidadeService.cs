using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IModalidadeService : ISMCService
    {
        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curriculo curso oferta da tabela curso oferta localidade
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencial do curriculo curso oferta</param>
        /// <returns>Lista de modalidades</returns>
        List<SMCDatasourceItem> BuscarModalidadesPorCurriculoCursoOfertaSelect(long seqCurriculoCursoOferta);

        /// <summary>
        /// Busca modalidades para a listagem de acordo com o curso oferta da tabela curso oferta localidade
        /// </summary>
        /// <param name="seqCursoOferta">Sequencial do curso oferta</param>
        /// <returns>Lista de modalidades</returns>
        List<SMCDatasourceItem> BuscarModalidadesPorCursoOfertaSelect(long seqCursoOferta);
    }
}
