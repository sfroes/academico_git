using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ConsultaDadosAlunoCiclosLetivosPlanoEstudoViewModel : SMCViewModelBase
    {
        [SMCValueEmpty("-")]
        public string OfertaMatriz { get; set; }

        public List<ConsultaDadosAlunoCiclosLetivosPlanoEstudoTrumasViewModel> Turmas { get; set; }

        public List<SMCTreeViewNode<ConsultaDadosAlunoCiclosLetivosPlanoEstudoTrumasViewModel>> TurmasTree { get { return SMCTreeView.For(this.Turmas ?? new List<ConsultaDadosAlunoCiclosLetivosPlanoEstudoTrumasViewModel>()); } }

        public List<ConsultaDadosAlunoCiclosLetivosPlanoEstudoAtividadeViewModel> Atividades { get; set; }
    }
}