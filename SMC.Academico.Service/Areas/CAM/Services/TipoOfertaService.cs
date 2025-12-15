using System;
using System.Collections.Generic;
using SMC.Academico.Domain.Areas.CAM.DomainServices;
using SMC.Academico.Domain.Areas.CAM.Models;
using SMC.Academico.Domain.Areas.CAM.ValueObjects;
using SMC.Academico.ServiceContract.Areas.CAM.Data;
using SMC.Academico.ServiceContract.Areas.CAM.Interfaces;
using SMC.Framework.Extensions;
using SMC.Framework.Model;
using SMC.Framework.Service;

namespace SMC.Academico.Service.Areas.CAM.Services
{
    public class TipoOfertaService : SMCServiceBase, ITipoOfertaService
    {
        #region [ Serviços ]

        private TipoOfertaDomainService TipoOfertaDomainService
        {
            get { return Create<TipoOfertaDomainService>(); }
        }

        #endregion [ Serviços ]

        public long SalvarTipoOferta(TipoOfertaData tipoOferta)
        {
            var tipoOfertaDominio = tipoOferta.Transform<TipoOferta>();
            return TipoOfertaDomainService.SalvarTipoOferta(tipoOfertaDominio);
        }

        public List<SMCDatasourceItem> BuscarTiposOfertaSelect()
        {
            return TipoOfertaDomainService.BuscarTiposOfertaSelect();
        }

        /// <summary>
        /// Mesmo método BuscarTiposOfertaSelect(), porém, com DataAttributes com tipo formacao especifica (true|false)
        /// </summary>
        /// <param name="seqCampanha"></param>
        /// <returns>Lista de todos os tipos de ofertas</returns>
        public List<SMCDatasourceItem> BuscarTiposOfertaDataAttribute()
        {
            return TipoOfertaDomainService.BuscarTiposOfertaDataAttribute();
        }

        public List<SMCDatasourceItem> BuscarTiposOfertaDaCampanhaSelect(long seqCampanha)
        {
            return TipoOfertaDomainService.BuscarTiposOfertaDaCampanhaSelect(seqCampanha);
        }

        public List<SMCDatasourceItem> BuscarTiposOfertaPorProcessoSeletivoSelect(long seqProcessoSeletivo)
        {
            return TipoOfertaDomainService.BuscarTiposOfertaPorProcessoSeletivoSelect(seqProcessoSeletivo);
        }

        public TipoOfertaSelecaoOfertaData BuscarTipoOfertaSelecaoOfertaCampanha(long seqTipoOferta, long seqCampanha)
        {
            return TipoOfertaDomainService.BuscarTipoOfertaSelecaoOfertaCampanha(seqTipoOferta,seqCampanha).Transform<TipoOfertaSelecaoOfertaData>();
        }
    }
}