using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RestricaoSolicitacaoSimultaneaViewModel : SMCWizardViewModel, ISMCStatefulView
    {        
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqServico { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(ServicoViewModel.ServicosRestricao))]        
        [SMCSize(SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24)]
        public long SeqServicoRestricao { get; set; }
    }
}