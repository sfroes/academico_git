using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaSelecaoTurmaMatriculaViewModel : MatriculaPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => MatriculaTokens.SELECAO_TURMA_MATRICULA;

        public string TurmaDescricao { get; set; }

        [SMCHidden]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public List<TurmaMatriculaItemViewModel> TurmasMatriculaItem { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }

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
        public bool ExibirIntegralizacao { get; set; }
    }
}