using SMC.Framework.Service;

namespace SMC.Academico.ServiceContract.Areas.ORG.Interfaces
{
    public interface ITipoHierarquiaEntidadeItemService : ISMCService
    {

        /// <summary>
        /// Verifica se um TipoHierarquiaEntidade possui ao menos um TipoHierarquiaEntidadeItem cadastrado
        /// </summary>
        /// <param name="SeqTipoHierarquiaEntidade"></param>
        /// <returns></returns>
        bool TipoHierarquiaEntidadePossuiFilhos(long SeqTipoHierarquiaEntidade);
    }
}
