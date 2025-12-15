using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.APR.Interfaces
{
    public interface IInstituicaoNivelEscalaApuracaoService : ISMCService
    {
        /// <summary>
        /// Recupera todas as EscalaAprovacao vincluladas aos níveis de ensino da instituição logada
        /// e marcadas para serem utilizadas na apuração final
        /// </summary>
        /// <returns>Dados das EscalaAprovacao que atendam aos critérios</returns>
        List<SMCDatasourceItem> BuscarEscalaApuracaoFinalDaInstituicao();
    }
}
