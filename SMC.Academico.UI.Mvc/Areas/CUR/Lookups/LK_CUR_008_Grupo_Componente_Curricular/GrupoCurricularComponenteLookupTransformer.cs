using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class GrupoCurricularComponenteLookupTransformer : ISMCTreeTransformer<GrupoCurricularComponenteLookupFiltroViewModel>
    {
        public List<SMCTreeViewNode<object>> Transform(IEnumerable<object> source, GrupoCurricularComponenteLookupFiltroViewModel filter)
        {
            // remove itens que não são selecionáveis e não tem filhos
            var dados = source.Where(s =>
            {
                var parsed = s as GrupoCurricularComponenteLookupViewModel;
                return parsed.Folha || source.Any(o => (o as GrupoCurricularComponenteLookupViewModel).SeqPai == parsed.Seq);
            }).ToList();

            return SMCTreeView.For(dados).AllowCheck(x => (x as GrupoCurricularComponenteLookupViewModel).Folha);
        }
    }
}