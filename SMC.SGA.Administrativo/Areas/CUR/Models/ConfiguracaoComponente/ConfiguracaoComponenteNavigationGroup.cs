using SMC.Academico.Common.Areas.CUR.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponenteNavigationGroup : SMCNavigationGroup
    {
        public ConfiguracaoComponenteNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                "Editar",
                "ComponenteCurricular",
                new string[] { UC_CUR_002_01_02.MANTER_COMPONENTE_CURRICULAR },
                parameters: new SMCNavigationParameter("Seq", "SeqComponenteCurricular")
            );
        }
    }
}