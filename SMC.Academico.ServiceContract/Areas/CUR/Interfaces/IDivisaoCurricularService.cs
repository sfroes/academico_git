using SMC.Framework.Service;
using SMC.Academico.ServiceContract.Areas.CUR.Data;
using System.Collections.Generic;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IDivisaoCurricularService : ISMCService
    {
        /// <summary>
        /// Buscar a divisão curricular e o nivel de ensino
        /// </summary>
        /// <param name="seq"></param>
        /// <returns>Objeto divisão curricular</returns>
        DivisaoCurricularData BuscarDivisaoCurricular(long seq);

        /// <summary>
        /// Salvar uma divisão curricular
        /// </summary>
        /// <param name="divisaoCurricularData"></param>
        /// <returns>Sequencial da Divisão Curricular</returns>
        long SalvarDivisaoCurricular(DivisaoCurricularData divisaoCurricularData);

        /// <summary>
        /// Buscar lista de divisões curriculares cadastradas para o nível de ensino do curso 
        /// </summary>
        /// <param name="seqCurriculoCursoOferta">Sequencia do curriculo curso oferta</param>
        /// <returns>Lista de divisões curriculares do mesmo nível de ensino</returns>
        List<SMCDatasourceItem> BuscarDivisoesCurricularesPorCurriculoCursoOferta(long seqCurriculoCursoOferta);
    }
}
