using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeItemNavigationGroup : SMCNavigationGroup
    {
        public HierarquiaEntidadeItemNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                "Editar",
                "HierarquiaEntidade",
                new string[] { UC_ORG_001_07_02.MANTER_HIERARQUIA_ENTIDADE },
                parameters: new SMCNavigationParameter("seq", "SeqHierarquiaEntidade")
            ).HideForModel<HierarquiaEntidadeDynamicModel>();
        }
    }
}