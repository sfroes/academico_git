using SMC.Academico.Common.Areas.ORG.Constants;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class EntidadeHistoricoSituacaoNavigationGroup : SMCNavigationGroup
    {
        public EntidadeHistoricoSituacaoNavigationGroup(SMCViewModelBase model)
            : base(model)
        {
            this.AddItem("GRUPO_Alterar",
                "Editar",
                "Entidade",
                new string[] { UC_ORG_001_06_02.MANTER_ENTIDADE },
                parameters: new SMCNavigationParameter("seq", "SeqEntidade"));
        }
    }
}