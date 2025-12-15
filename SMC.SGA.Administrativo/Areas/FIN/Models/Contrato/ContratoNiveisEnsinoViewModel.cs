using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class ContratoNiveisEnsinoViewModel : SMCViewModelBase
    {
        [SMCRequired]
        [SMCSelect("NiveisDeEnsino", SortBy = SMCSortBy.Description, AutoSelectSingleItem = true)]
        [SMCSize(SMCSize.Grid20_24)]
        public long? SeqNivelEnsino { get; set; }
    }
}