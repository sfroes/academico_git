using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class HierarquiaClassificacaoNavigationGroup : SMCNavigationGroup
    {
        public HierarquiaClassificacaoNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                "Editar",
                "HierarquiaClassificacao",
                new string[] { UC_CSO_001_04_02.MANTER_HIERARQUIA_CLASSIFICACAO },
                parameters: new SMCNavigationParameter("Seq", "SeqHierarquiaClassificacao")
            ).HideForModel<HierarquiaClassificacaoDynamicModel>();

            this.AddItem("GRUPO_MontagemHierarquia",
                "Index",
                "Classificacao",
                new string[] { UC_CSO_001_04_02.MANTER_HIERARQUIA_CLASSIFICACAO },
                parameters: new SMCNavigationParameter("SeqHierarquiaClassificacao", "Seq")
            )
            .HideForModel<ClassificacaoDynamicModel>()
            .HideForModel<HierarquiaClassificacaoDynamicModel>(SMCViewMode.Filter | SMCViewMode.List | SMCViewMode.Insert);
        }
    }
}