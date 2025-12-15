using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.GRD.Models
{
    public class DetalheDivisaoTurmaGradeViewModel : SMCViewModelBase
    {
        public string CodigoTurma { get; set; }

        public string DescricaoCicloLetivoInicio { get; set; }

        public string DescricaoTipoTurma { get; set; }

        [SMCValueEmpty("-")]
        public SituacaoTurma? SituacaoTurmaAtual { get; set; }

        public string TurmaDescricaoFormatado { get; set; }

        public List<string> DescricoesCursoOfertaLocalidadeTurno { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoLocalidade { get; set; }

        [SMCValueEmpty("-")]
        public short? QuantidadeVagasOcupadas { get; set; }

        [SMCValueEmpty("-")]
        public short? CargaHorariaGrade { get; set; }

        [SMCValueEmpty("-")]
        public int CargaHorariaLancada { get; set; }

        public DateTime DataInicioPeriodoLetivo { get; set; }

        public DateTime DataFimPeriodoLetivo { get; set; }

        [SMCValueEmpty("-")]
        public TipoDistribuicaoAula? TipoDistribuicaoAula { get; set; }

        [SMCValueEmpty("-")]
        public bool? AulaSabado { get; set; }

        [SMCValueEmpty("-")]
        public TipoPulaFeriado? TipoPulaFeriado { get; set; }

        [SMCValueEmpty("-")]
        public string DescricaoTabelaHorario { get; set; }

        public List<DetalheDivisaoTurmaGradeProfessorViewModel> Professores { get; set; }

        public List<DetalheDivisaoTurmaGradeLocalViewModel> LocaisAula { get; set; }
    }
}