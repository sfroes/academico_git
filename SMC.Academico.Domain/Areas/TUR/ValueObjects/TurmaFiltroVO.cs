using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? Seq { get; set; }
        public long? SeqCicloLetivoInicio { get; set; }
        public long? SeqCicloLetivoFim { get; set; }
        public string CodigoFormatado { get; set; }
        public string DescricaoConfiguracao { get; set; }
        public SituacaoTurma? SituacaoTurmaAtual { get; set; }
        public SituacaoTurmaDiario? SituacaoTurmaDiario { get; set; }
        public bool? TurmaComOrientacao { get; set; }
        public long? SeqNivelEnsino { get; set; }
        public List<long> SeqsEntidadesResponsaveis { get; set; }
        public long? SeqCursoOferta { get; set; }
        public bool? TurmasPeriodoEncerrado { get; set; }
        public bool? TurmaSituacaoNaoCancelada { get; set; }
    }
}
