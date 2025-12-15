using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ORG.Models;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.OFC.Models
{
    public class OfertaCursoCicloLetivoAssociacaoPoloViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqOfertaCursoCicloLetivo { get; set; }

        public List<SMCTreeViewNode<PoloItemArvoreViewModel>> Polos { get; set; }
    }
}