using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeNavigationGroup : SMCNavigationGroup
    {
        public HierarquiaEntidadeNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_MontagemHierarquia",
                "VisaoArvore",
                "HierarquiaEntidade",
                new string[] { UC_ORG_001_07_02.MANTER_HIERARQUIA_ENTIDADE },
                parameters: new SMCNavigationParameter("seqHierarquiaEntidade", "Seq")
            )
            .HideForModel<EntidadeDynamicModel>()
            .HideForModel<HierarquiaEntidadeDynamicModel>(SMCViewMode.Filter | SMCViewMode.List | SMCViewMode.Insert);
        }
    }
}