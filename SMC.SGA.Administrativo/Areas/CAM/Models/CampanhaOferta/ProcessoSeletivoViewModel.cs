using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCParameter]
        [SMCOrder(0)]
        public long? Seq { get; set; }

        [SMCHidden]
        public long? SeqCampanha { get; set; }

        [SMCDescription]
        [SMCOrder(1)]
        public string Descricao { get; set; }

        [SMCOrder(1)]
        public List<ConvocacaoViewModel> Convocacoes { get; set; }      

    }
}