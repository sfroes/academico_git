using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ITipoHierarquiaClassificacaoService : ISMCService
    {
        /// <summary>
        /// Busca os dados de um tipo de hierarquia de classificação
        /// </summary>
        /// <param name="seq">Sequencial do tipo de hierarquia de classificação</param>
        /// <returns>Informações do tipo de hierarquia de classificação</returns>
        TipoHierarquiaClassificacaoData BuscarTipoHierarquiaClassificacao(long seq);
    }
}
