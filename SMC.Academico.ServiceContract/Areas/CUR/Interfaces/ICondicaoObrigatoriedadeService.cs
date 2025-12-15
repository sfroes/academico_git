using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CUR.Interfaces
{
    public interface ICondicaoObrigatoriedadeService : ISMCService
    {
        /// <summary>
        /// Busca as condições de obrigatoriedade na instituição do nível de ensino informado
        /// </summary>
        /// <param name="seqNivelEnsino">Sequencial do nível de ensino</param>
        /// <returns>Dados das condições de obrigatoriedade</returns>
        List<SMCDatasourceItem> BuscarCondicoesObrigatoriedadePorNivelEnsino(long seqNivelEnsino);

        /// <summary>
        /// Busaca as condições de obigatoriedade configuradas nos grupos curriculares da currículo curso oferta da matriz informada
        /// </summary>
        /// <param name="seqMatrizCurricularOferta">Sequencial da oferta de matriz currícular</param>
        /// <returns>Condições de obrigatoriedade</returns>
        List<SMCDatasourceItem> BuscarCondicoesObrigatoriedadePorMatrizCurricularOferta(long seqMatrizCurricularOferta);
    }
}