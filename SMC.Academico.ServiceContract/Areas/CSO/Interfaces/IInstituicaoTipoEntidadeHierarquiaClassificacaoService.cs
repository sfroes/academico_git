using SMC.Academico.ServiceContract.Areas.CSO.Data.InstituicaoTipoEntidadeHierarquiaClassificacao;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface IInstituicaoTipoEntidadeHierarquiaClassificacaoService : ISMCService
    {
        /// <summary>
        /// Busca todas os InstituicaoTipoEntidadeHierarquiaClassificacao referentes à InstituicaoTipoEntidade
        /// </summary>
        /// <param name="SeqInstituicaoTipoEntidade">Seq da InstituicaoTipoEntidade</param>
        /// <returns></returns>
        List<InstituicaoTipoEntidadeHierarquiaClassificacaoData> BuscarInstituicaoTipoEntidadeHierarquiasClassificacao(long SeqInstituicaoTipoEntidade);
    }
}
