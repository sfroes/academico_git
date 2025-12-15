using SMC.Academico.ServiceContract.Areas.ORG.Data;
using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface ITipoHierarquiaEntidadeService : ISMCService
    {
        /// <summary>
        /// Busca os dados de um tipo de hierarquia de entidade
        /// </summary>
        /// <param name="seq">Sequencial do tipo de hierarquia de entidade</param>
        /// <returns>Informações do tipo de hierarquia de entidade</returns>
        TipoHierarquiaEntidadeData BuscarTipoHierarquiaEntidade(long seq);

        /// <summary>
        /// Busca o item de hierarquia de entidade a partir do sequencial
        /// </summary>
        /// <param name="seqTipoHierarquiaEntidadeItem">Sequencial da TipoHierarquiaEntidadeItem a ser retornado</param>
        /// <returns>TipoHierarquiaEntidadeItems que atendem ao filtro</returns>
        TipoHierarquiaEntidadeItemData BuscarTipoHierarquiaEntidadeItem(long seqTipoHierarquiaEntidadeItem);
    }
}
