using SMC.Academico.ServiceContract.Areas.CSO.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CSO.Interfaces
{
    public interface ITipoClassificacaoService : ISMCService
    {
        /// <returns>Lista de tipos de classificação</returns>
        /// <summary>
        /// Busca a lista de tipos de classificação da hierarquia escolhida para popular um Select
        /// </summary>
        /// <param name="seqTipoHierarquiaClassificacao">Sequencial do tipo de hierarquia de classificação</param>
        /// <param name="exclusivo"></param>
        /// <param name="seqClassificacaoSuperior"></param>
        /// <returns>Lista de tipos de classificação</returns>
        List<SMCDatasourceItem> BuscarTiposClassificacaoPorHierarquiaSelect(TipoClassificacaoPorHierarquiaFiltroData filtros);
    }
}