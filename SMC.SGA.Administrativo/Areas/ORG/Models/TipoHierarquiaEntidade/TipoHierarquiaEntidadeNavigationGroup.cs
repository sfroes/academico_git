using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoHierarquiaEntidadeNavigationGroup : SMCNavigationGroup
    {
        public TipoHierarquiaEntidadeNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                "Editar",
                "TipoHierarquiaEntidade",
                new string[] { UC_ORG_001_05_02.MANTER_TIPO_HIERARQUIA_ENTIDADE },
                parameters: new SMCNavigationParameter("Seq", "SeqTipoHierarquiaEntidade")
            ).HideForModel<TipoHierarquiaEntidadeDynamicModel>();

            this.AddItem("GRUPO_MontagemHierarquia",
                "VerificaConfiguracaoTipoEntidade",
                "TipoHierarquiaEntidade",
                new string[] { UC_ORG_001_05_03.MONTAR_HIERARQUIA_TIPO_ENTIDADE },
                parameters: new SMCNavigationParameter("SeqTipoHierarquiaEntidade", "Seq")//valor = "seq", nome param = "SeqTipoHierarquiaEntidade" }
            )
            .HideForModel<TipoHierarquiaEntidadeItemDynamicModel>()
            .HideForModel<TipoHierarquiaEntidadeDynamicModel>(SMCViewMode.Filter | SMCViewMode.List | SMCViewMode.Insert);
        }
    }
}