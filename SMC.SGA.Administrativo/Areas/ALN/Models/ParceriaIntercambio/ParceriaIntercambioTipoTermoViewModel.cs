using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioTipoTermoViewModel : SMCViewModelBase
    {
        #region DataSource

        [SMCDataSource]
        [SMCHidden]
        [SMCIgnoreProp]
        [SMCServiceReference(typeof(ITipoTermoIntercambioService), nameof(ITipoTermoIntercambioService.BuscarTiposTermosIntercambiosInstituicaoNivelSelect))]
        public List<SMCDatasourceItem> TiposTermosIntercambios { get; set; }

        #endregion

        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCSize(SMCSize.Grid12_24)]
        [SMCSelect(nameof(TiposTermosIntercambios), SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCRequired]
        [SMCUnique]
        public long SeqTipoTermoIntercambio { get; set; }

        [SMCRadioButtonList]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        public bool Ativo { get; set; }
    }
}