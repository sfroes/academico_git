using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.ORG.Lookups
{
    public class NivelEnsinoLookupTransformer : ISMCTreeTransformer<object>
    {
        public List<SMCTreeViewNode<object>> Transform(IEnumerable<object> source, object filter)
        {
            return SMCTreeView.For(source).AllowCheck(x => (x as NivelEnsinoLookupViewModel).Folha);
        }
    }
}
