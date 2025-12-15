using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CNC.Models.Titulacao
{
    public class TitulacaoDocumentoCompDynamicModel : SMCDynamicViewModel
    {
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }


        [SMCSelect("Titulacoes")]
        [SMCSize(SMCSize.Grid18_24)]
        public long? SeqTipoDocumento { get; set; }

    }
}