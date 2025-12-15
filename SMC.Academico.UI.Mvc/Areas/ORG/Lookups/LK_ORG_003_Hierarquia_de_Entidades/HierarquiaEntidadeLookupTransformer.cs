using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class HierarquiaEntidadeLookupTransformer : ISMCTreeTransformer<HierarquiaEntidadeLookupFiltroViewModel>
    {
        public List<SMCTreeViewNode<object>> Transform(IEnumerable<object> source, HierarquiaEntidadeLookupFiltroViewModel filter)
        {
            return SMCTreeView.For(source).AllowCheck(x => (x as HierarquiaEntidadeLookupViewModel).TipoClassificacaoFolha);
        }
    }
}
