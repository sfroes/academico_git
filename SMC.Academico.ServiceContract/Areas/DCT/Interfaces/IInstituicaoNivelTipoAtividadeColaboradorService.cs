using SMC.Academico.ServiceContract.Areas.DCT.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Interfaces
{
    public interface IInstituicaoNivelTipoAtividadeColaboradorService : ISMCService
    {
        /// <summary>
        /// Retorna os tipos de atividade configurados para os colaboradoes na instituição logada
        /// </summary>
        /// <param name="filtroData">Dados do filtro</param>
        /// <returns>Dados das atividades configuradas para os colaboradoes na instituição logada</returns>
        List<SMCDatasourceItem> BuscarTiposAtividadeColaboradorSelect(InstituicaoNivelTipoAtividadeColaboradorFiltroData filtroData);
    }
}