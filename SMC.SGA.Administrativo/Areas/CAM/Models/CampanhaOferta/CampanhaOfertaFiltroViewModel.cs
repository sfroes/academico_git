using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CampanhaOfertaFiltroViewModel : SMCPagerViewModel
    {
        #region Datasources
        [SMCIgnoreProp]
        public List<SMCSelectListItem> TiposOfertas { get; set; }
        #endregion

        public SMCEncryptedLong SeqCampanha { get; set; }

        [SMCFilter]
        [SMCSelect(nameof(TiposOfertas))]
        [SMCSize(SMCSize.Grid12_24)]
        public long? SeqTipoOferta { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid12_24)]
        public string Oferta { get; set; }
    }
}