using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.MAT.Models
{
    public class SolicitacaoMatriculaItemViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        public string Etapa { get; set; }
                
        public List<SolicitacaoMatriculaItemSituacaoViewModel> Turmas { get; set; }

        public List<SolicitacaoMatriculaItemSituacaoViewModel> Atividades { get; set; }
    }
}
