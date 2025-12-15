using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class ClassificacaoLookupTransformer : ISMCTreeTransformer<ClassificacaoLookupFiltroViewModel>
    {
        public List<SMCTreeViewNode<object>> Transform(IEnumerable<object> source, ClassificacaoLookupFiltroViewModel filter)
        {
            return SMCTreeView.For(source).AllowCheck(x => (x as ClassificacaoLookupViewModel).TipoClassificacaoSelecionavel.GetValueOrDefault());
        }
    }
}
