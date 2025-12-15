using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class ProcessoSeletivoListarViewModel : SMCViewModelBase, ISMCTreeNode
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        [SMCHidden]
        [SMCParameter]
        public long? SeqPai { get; set; }

    }
}