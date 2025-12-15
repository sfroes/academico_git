using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ConvocacaoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCOrder(0)]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqProcessoSeletivo { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        public string Descricao { get; set; }
    }
}