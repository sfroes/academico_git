using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.Academico.UI.Mvc.Areas.SRC.Models
{
    public class SelecaoChancelaPlanoTurmaViewModel
    {
        public long SeqTurma { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoMatriculaItem { get; set; }
        
        [SMCSize(SMCSize.Grid9_24)]
        public List<SelecaoChancelaPlanoTurmaDivisoesViewModel> TurmaMatriculaDivisoes { get; set; }

        public string TurmaFormatado { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public AssociacaoOfertaMatriz AssociacaoOfertaMatrizTipoTurma { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public bool? LegendaPertencePlanoEstudo { get; set; }

        public string ProgramaTurma { get; set; }

        public bool? ExibirEntidadeResponsavelTurma { get; set; }

        public long? SeqSituacaoFinalSemSucessoItemEtapaChancela { get; set; }
    }
}