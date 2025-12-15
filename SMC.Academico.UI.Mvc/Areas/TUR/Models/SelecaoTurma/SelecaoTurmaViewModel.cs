using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.TUR.Models
{
    public class SelecaoTurmaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqSolicitacaoMatricula { get; set; }

        [SMCHidden]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public long? SeqProcesso { get; set; }

        [SMCHidden]
        public long? SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }

        [SMCHidden]
        public string CicloLetivoDescricao { get; set; }

        public string TurmaDescricao { get; set; }

        [SMCConditionalDisplay(nameof(ExibirFiltroCurriculo), SMCConditionalOperation.Equals, true)]
        public bool SomenteObrigatorio { get; set; }
        
        [SMCHidden]
        public List<SelecaoTurmaOfertaViewModel> TurmasMatrizOferta { get; set; }

        [SMCHidden]
        public string TurmasMatrizOfertaString { get; set; }

        [SMCHidden]
        public List<long?> SelectedValues { get; set; }        

        [SMCHidden]
        public bool? ExigirCurso { get; set; }

        [SMCHidden]
        public bool? ExigirMatrizCurricularOferta { get; set; }

        [SMCHidden]
        public bool? ExibirCancelados { get; set; }

        [SMCHidden]
        public bool? TodosCancelados { get; set; }

        [SMCHidden]
        public bool? ExibirPreRequisito { get; set; }

        [SMCHidden]
        public long? SeqPrograma { get; set; }

        [SMCHidden]
        public bool? ExibirFiltroCurriculo { get; set; }
    }
}
