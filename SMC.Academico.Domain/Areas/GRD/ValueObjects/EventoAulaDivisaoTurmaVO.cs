using SMC.Framework;
using SMC.Framework.Mapper;
using System;
using SMC.Academico.Common.Areas.GRD.Enums;
using System.Collections.Generic;
using SMC.Academico.Common.Areas.APR.Enums;

namespace SMC.Academico.Domain.Areas.GRD.ValueObjects
{
    public class EventoAulaDivisaoTurmaVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long TurmaSeq { get; set; }

        public int TurmaCodigo { get; set; }

        public short TurmaNumero { get; set; }

        public short Numero { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public short NumeroGrupo { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string TipoDivisaoDescricao { get; set; }

        public string DescricaoDivisaoTurma { get; set; }

        public TipoDistribuicaoAula? TipoDistribuicaoAula { get; set; }

        public TipoPulaFeriado? TipoPulaFeriado { get; set; }

        public bool? AulaSabado { get; set; }

        public DateTime? InicioPeriodoLetivo { get; set; }

        public DateTime? FimPeriodoLetivo { get; set; }

        public short? CargaHorariaLancada { get; set; }

        public long? SeqCursoOfertaLocalidadeTurno { get; set; }

        public List<EventoAulaVO> EventoAulas { get; set; }

        public string GrupoFormatado { get; set; }

        public string DescricaoDivisaoFormatada {get; set; }

        public short? QuantidadeSemanas { get; set; }

        /// <summary>
        /// Verdadeiro caso algum aluno da divisão já tenha histórico escolar
        /// </summary>
        public bool TemHistoricoEscolar { get; set; }

        public TipoOrigemAvaliacao TipoOrigemAvaliacao { get; set; }

        public long? SeqAgendaTurma { get; set; }

        /// <summary>
        /// Verdadeiro caso tenha laguma configuração de grade
        /// </summary>
        public bool TemConfiguracaoGrade { get; set; }

        public int? CodigoUnidadeSEO { get; set; }
        public List<GradeHorariaCompartilhadaVO> Compartilhamentos { get; set; }
    }
}
