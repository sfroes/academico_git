using SMC.Academico.Common.Areas.CAM.Constants;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CicloLetivoNavigationGroup : SMCNavigationGroup
    {
        public CicloLetivoNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                         "Editar",
                         "CicloLetivo",
                         new string[] { UC_CAM_002_01_02.MANTER_CICLO_LETIVO },
                         parameters: new SMCNavigationParameter("seq", "Seq"));

            this.AddItem("GRUPO_Parametrizar_Tipo_Evento_Letivo",
                         "Index",
                         "CicloLetivoTipoEvento",
                         new string[] { UC_CAM_002_01_03.PESQUISAR_PARAMETRIZACAO_TIPO_EVENTO_LETIVO },
                         parameters: new SMCNavigationParameter("seq", "Seq"));
        }
    }
}