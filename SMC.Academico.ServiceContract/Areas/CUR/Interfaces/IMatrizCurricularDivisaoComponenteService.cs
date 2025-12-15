using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface IMatrizCurricularDivisaoComponenteService : ISMCService
    {
        /// <summary>
        /// Busca a matriz curricular divisão componente cadastrada e configurada
        /// </summary>
        /// <param name="seqMatrizCurricularDivisaoComponente">Sequencial da matriz curricular divisão componente</param>
        /// <returns>Registro da matriz curricular divisão configurado</returns>
        MatrizCurricularDivisaoComponenteData BuscarConfiguracaoMatrizCurricular(long seqMatrizCurricularDivisaoComponente);

        /// <summary>
        /// Busca as divisoes de uma divisão matriz curricular compoenete
        /// </summary>
        /// <param name="seq">Sequencial da  divisão matriz curricular compoenete</param>
        /// <returns>ados da matriz curricular divisão componente</returns>
        List<MatrizCurricularDivisaoComponenteData> BuscarDivisaoMatrizCurricularComponenteDivisoes(long seq);
    }
}
