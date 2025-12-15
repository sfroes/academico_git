using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Framework.Model;
using SMC.Framework.Service;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Interfaces
{
    public interface ITipoOfertaService : ISMCService
    {
        long SalvarTipoOferta(TipoOfertaData tipoOferta);

        List<SMCDatasourceItem> BuscarTiposOfertaSelect();

        /// <summary>
        /// Mesmo método BuscarTiposOfertaSelect(), porém, com DataAttributes com tipo formacao especifica (true|false)
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <returns>Lista de todos os tipos de ofertas</returns>
        List<SMCDatasourceItem> BuscarTiposOfertaDataAttribute();

        List<SMCDatasourceItem> BuscarTiposOfertaDaCampanhaSelect(long seqCampanha);

        List<SMCDatasourceItem> BuscarTiposOfertaPorProcessoSeletivoSelect(long seqProcessoSeletivo);

        TipoOfertaSelecaoOfertaData BuscarTipoOfertaSelecaoOfertaCampanha(long seqTipoOferta, long seqCampanha);
    }
}