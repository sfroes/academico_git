using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class GrupoCurricularLookupTransformer : ISMCTreeTransformer<GrupoCurricularLookupFiltroViewModel>
    {
        public List<SMCTreeViewNode<object>> Transform(IEnumerable<object> source, GrupoCurricularLookupFiltroViewModel filter)
        {
            SMCTreeView<object> retorno;
            if (filter.PermitirSelecionarGruposComComponentes)
            {
                retorno = SMCTreeView.For(source).AllowCheck(x => ((x as GrupoCurricularLookupViewModel).ContemComponentes && !(x as GrupoCurricularLookupViewModel).Folha));
            }
            else
            {
                retorno = SMCTreeView.For(source).AllowCheck(x => !(x as GrupoCurricularLookupViewModel).Folha);
            }
            return retorno;
        }
    }
}
