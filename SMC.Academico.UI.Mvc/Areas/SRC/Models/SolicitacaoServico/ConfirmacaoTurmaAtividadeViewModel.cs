using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.UI.Mvc.Areas.MAT.Models;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class ConfirmacaoTurmaAtividadeViewModel : SolicitacaoServicoPaginaViewModelBase
    {
        [SMCHidden]
        public override string Token => TOKEN_SOLICITACAO_SERVICO.CONFIRMACAO_SOLICITACAO_MATRICULA;

        [SMCHidden]
        public override string ChaveTextoBotaoProximo => "Botao_Confirmar";

        [SMCMaxLength(255)]
        [SMCMultiline]
        [SMCRequired]
        public string Justificativa { get; set; }

        [SMCHidden]
        public long? SeqGrupoPrograma { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCHidden]
        public List<TurmaMatriculaItemViewModel> TurmasMatriculaItem { get; set; }

        [SMCHidden]
        public List<AtividadeAcademicaMatriculaItemViewModel> AtividadesAcademicaMatriculaItem { get; set; }

        [SMCHidden]
        public bool? ExigirCurso { get; set; }

        [SMCHidden]
        public bool? ExigirMatrizCurricularOferta { get; set; }

        [SMCHidden]
        public bool? ExibirCancelados { get; set; }

        [SMCHidden]
        public bool? TodosCancelados { get; set; }

        [SMCHidden]
        public bool? ExibirAtividade { get; set; }

        [SMCHidden]
        public string TokenServico { get; set; }
    }
}