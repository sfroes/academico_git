using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IInstituicaoNivelCriterioAprovacaoService : ISMCService
    {
        /// <summary>
        /// Recupera todos critérios de aprovação associados à uma escala do nível informado na instituição atual ou
        /// que não possuam escalas de apuração associadas
        /// </summary>
        /// <param name="seqInstituicaoNivel">Sequencial do nível de ensino</param>
        /// <returns>Dados dos Critérios de Aprovação que atendam aos critérios</returns>
        List<SMCDatasourceItem> BuscarCriteriosAprovacaoDaInstituicaoNivelSelect(long seqInstituicaoNivel);
    }
}
