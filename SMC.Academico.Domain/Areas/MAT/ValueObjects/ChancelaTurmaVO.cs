using SMC.Academico.Common.Areas.MAT.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Formularios.Common.Areas.TMP.Enums;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.MAT.ValueObjects
{
    public class ChancelaTurmaVO : ISMCMappable
    {
        public long SeqTurma { get; set; }

        public long SeqSolicitacaoMatriculaItem { get; set; }
        
        public string TurmaFormatado { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public TurmaOfertaMatricula Pertence { get; set; }

        public AssociacaoOfertaMatriz AssociacaoOfertaMatrizTipoTurma { get; set; }

        public List<ChancelaTurmaDivisoesVO> TurmaMatriculaDivisoes { get; set; }

        public bool? LegendaPertencePlanoEstudo { get; set; }

        public string ProgramaTurma { get; set; }

        public bool? ExibirEntidadeResponsavelTurma { get; set; }
        
        public long? SeqSituacaoFinalSemSucessoItemEtapaChancela { get; set; }

        public ClassificacaoSituacaoFinal? ClassificacaoSituacaoFinal { get; set; }
    }
}