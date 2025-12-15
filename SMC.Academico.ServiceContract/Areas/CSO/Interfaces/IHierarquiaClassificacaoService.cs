using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IHierarquiaClassificacaoService : ISMCService
    {
        /// <summary>
        /// Busca os dados de uma hierarquia de classificação
        /// </summary>
        /// <param name="seq">Sequencial de hierarquia de classificação</param>
        /// <returns>Informações de hierarquia de classificação</returns>
        HierarquiaClassificacaoData BuscarHierarquiaClassificacao(long seq);



        /// <summary>
        /// Efetua uma busca para retornar o select de HierarquiaClassificacao
        /// </summary>
        /// <param name="seqHierarquiaClassificacao">seqHierarquiaClassificacao pai</param>
        /// <returns></returns>
        List<SMCDatasourceItem> BuscarHierarquiaClassificacaoSelect(long seqTipoHierarquiaClassificacao);
    }
}
