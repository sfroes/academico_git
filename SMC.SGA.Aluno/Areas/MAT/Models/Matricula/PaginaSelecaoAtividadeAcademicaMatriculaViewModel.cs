using SMC.Academico.Common.Areas.ALN.Constants;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Aluno.Areas.MAT.Models.Matricula
{
    public class PaginaSelecaoAtividadeAcademicaMatriculaViewModel : MatriculaPaginaViewModelBase
    {
        public override string Token => MatriculaTokens.SELECAO_ATIVIDADE_ACADEMICA_MATRICULA;

        [SMCHidden]
        public long SeqIngressante { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public string backUrl { get; set; }

        [SMCHidden]
        public List<AtividadeAcademicaMatriculaItemViewModel> AtividadesAcademicaMatriculaItem { get; set; }

        [SMCHidden]
        public bool? ExibirCancelados { get; set; }

        [SMCHidden]
        public bool? ExibirPreRequisito { get; set; }

        [SMCHidden]
        public bool ExibirIntegralizacao { get; set; }
    }
}