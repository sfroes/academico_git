using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class TipoVinculoAlunoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCRequired]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        //FIX: Verificar como obter o include do modelo principal
        public SMCMasterDetailList<FormaIngressoViewModel> FormasIngresso { get; set; }
    }
}