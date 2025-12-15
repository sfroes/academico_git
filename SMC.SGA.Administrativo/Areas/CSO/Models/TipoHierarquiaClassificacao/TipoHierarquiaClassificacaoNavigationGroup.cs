using SMC.Academico.Common.Areas.CSO.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TipoHierarquiaClassificacaoNavigationGroup : SMCNavigationGroup
    {
        public TipoHierarquiaClassificacaoNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                "Editar",
                "TipoHierarquiaClassificacao",
                new string[] { UC_CSO_001_06_02.MANTER_TIPO_HIERARQUIA_CLASSIFICACAO },
                parameters: new SMCNavigationParameter("Seq", "SeqTipoHierarquiaClassificacao")
            ).HideForModel<TipoHierarquiaClassificacaoDynamicModel>();

            this.AddItem("GRUPO_MontagemHierarquia",
                "VerificaConfiguracaoTipoClassificacao",
                "TipoHierarquiaClassificacao",
                new string[] { UC_CSO_001_06_02.MANTER_TIPO_HIERARQUIA_CLASSIFICACAO },
                parameters: new SMCNavigationParameter("SeqTipoHierarquiaClassificacao", "Seq")
            )
            .HideForModel<TipoClassificacaoDynamicModel>()
            .HideForModel<TipoHierarquiaClassificacaoDynamicModel>(SMCViewMode.Filter | SMCViewMode.List | SMCViewMode.Insert);
        }
    }
}