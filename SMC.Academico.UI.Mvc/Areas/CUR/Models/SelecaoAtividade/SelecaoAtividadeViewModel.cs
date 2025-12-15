using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Models
{
    public class SelecaoAtividadeViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoMatricula { get; set; }

        [SMCHidden]
        public long SeqIngressante { get; set; }              

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }

        [SMCHidden]
        public List<SelecaoAtividadeOfertaViewModel> AtividadesAcademicaMatriculaItem { get; set; }
        
        [SMCHidden]
        public List<long?> SelectedValues { get; set; }

        [SMCHidden]
        public bool? ExibirCancelados { get; set; }

        [SMCHidden]
        public bool? ExibirPreRequisito { get; set; }
    }
}
