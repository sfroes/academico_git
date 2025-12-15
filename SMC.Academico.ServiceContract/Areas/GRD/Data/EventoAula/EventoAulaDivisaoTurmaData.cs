using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.GRD.Enums;
using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.GRD.Data
{
    public class EventoAulaDivisaoTurmaData : ISMCMappable
    {
        public long Seq { get; set; }

        public int TurmaCodigo { get; set; }

        public short TurmaNumero { get; set; }

        public short Numero { get; set; }

        public short? CargaHorariaGrade { get; set; }

        public short NumeroGrupo { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string TipoDivisaoDescricao { get; set; }

        public TipoDistribuicaoAula? TipoDistribuicaoAula { get; set; }

        public TipoPulaFeriado? TipoPulaFeriado { get; set; }

        public bool? AulaSabado { get; set; }

        public DateTime? InicioPeriodoLetivo { get; set; }

        public DateTime? FimPeriodoLetivo { get; set; }

        public short? CargaHorariaLancada { get; set; }

        public long SeqCursoOfertaLocalidadeTurno { get; set; }

        public List<EventoAulaData> EventoAulas { get; set; }

        public string GrupoFormatado { get; set; }

        public string DescricaoDivisaoFormatada { get; set; }

        public short? QuantidadeSemanas { get; set; }

        /// <summary>
        /// Verdadeiro caso algum aluno da divisão já tenha histórico escolar
        /// </summary>
        public bool TemHistoricoEscolar { get; set; }

        public TipoOrigemAvaliacao TipoOrigemAvaliacao { get; set; }

        public long? SeqAgendaTurma { get; set; }

        public int? CodigoUnidadeSEO { get; set; }
        public List<GradeHorariaCompartilhadaData> Compartilhamentos { get; set; }
    }
}
