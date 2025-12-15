using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IInstituicaoNivelTipoOfertaCursoService : ISMCService
    {

        /// <summary>
        /// Busca a lista de Tipo Oferta para o Curso e Instituicao para popular um Select
        /// </summary>
        /// <param name="seqCurso">Sequencial do curso</param>
        /// <returns>Lista de Tipo Oferta</returns>
        List<SMCDatasourceItem> BuscarInstituicaoNivelTipoOfertaCursoSelect(long seqCurso);

    }
}
