using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class DetalheDivisaoTurmaGradeVO : ISMCMappable
    {
        public long SeqCursoOfertaLocalidade { get; set; }
        public string CodigoTurma { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoTipoTurma { get; set; }

        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public string TurmaDescricaoFormatado { get; set; }

        public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

        public string DescricaoLocalidade { get; set; }

        public short? QuantidadeVagasOcupadas { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public int CargaHorariaLancada { get; set; }

        public long? SeqAgendaTurma { get; set; }

        public DateTime DataInicioPeriodoLetivo { get; set; }

        public DateTime DataFimPeriodoLetivo { get; set; }

        public TipoDistribuicaoAula? TipoDistribuicaoAula { get; set; }

        public bool? AulaSabado { get; set; }

        public TipoPulaFeriado? TipoPulaFeriado { get; set; }

        public string DescricaoTabelaHorario { get; set; }

        public List<DetalheDivisaoTurmaGradeProfessorVO> Professores { get; set; }

        public List<DetalheDivisaoTurmaGradeLocalVO> LocaisAula { get; set; }
    }
}
