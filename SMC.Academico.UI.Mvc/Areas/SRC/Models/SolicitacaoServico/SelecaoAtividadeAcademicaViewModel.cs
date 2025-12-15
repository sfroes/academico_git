using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SelecaoAtividadeAcademicaViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.SELECAO_ATIVIDADE_ACADEMICA_MATRICULA;
                
        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public List<AtividadeAcademicaMatriculaItemViewModel> AtividadesAcademicaMatriculaItem { get; set; }

        [SMCHidden]
        public bool ExibirPertencePlanoEstudo { get; set; }

        [SMCHidden]
        public bool? ExibirCancelados { get; set; }

        [SMCHidden]
        public bool? ExibirPreRequisito { get; set; }        
    }
}