using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IDivisaoMatrizCurricularService : ISMCService
    {
        /// <summary>
        /// Busca as divisões de matrizes curriculares com as descrições
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularDescricaoSelect(long seqMatrizCurricular);

        /// <summary>
        /// Busca as divisões de matrizes curriculares com os tipos na descrição
        /// </summary>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularTipoSelect(long seqMatrizCurricular);

        /// <summary>
        /// Busca as divisões de matrizes curriculares com os tipos na descrição de acordo com o tipo de requisito (Pré ou Có)
        /// </summary>       
        /// <param name="tipoRequisito">Tipo do requisito selecionado</param>
        /// <param name="seqMatrizCurricular">Sequencial da matriz curricular selecionada</param>
        /// <param name="seqDivisaoMatrizCurricular">Sequencial da divisão matriz curricular selecionada</param>
        /// <returns>Dados das divisões de matrizes curriculares</returns>
        List<SMCDatasourceItem> BuscarDivisoesMatrizCurricularTipoPorTipoRequisitoSelect(TipoRequisito tipoRequisito, long seqMatrizCurricular, long? seqDivisaoMatrizCurricular);
    }
}
