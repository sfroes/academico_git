using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CSO.Lookups
{
    public class FormacaoEspecificaLookupTransformer : ISMCTreeTransformer<FormacaoEspecificaLookupFiltroViewModel>
    {
        public List<SMCTreeViewNode<object>> Transform(IEnumerable<object> source, FormacaoEspecificaLookupFiltroViewModel filter)
        {
            return SMCTreeView.For(source).AllowCheck(x => (x as FormacaoEspecificaLookupViewModel).Selecionavel.GetValueOrDefault());            
        }
    }
}