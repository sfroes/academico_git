using SMC.Academico.Common.Areas.TUR.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.TUR.Data
{
    public class TurmaFiltroData : SMCPagerFilterData, ISMCMappable
    {
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
		[SMCKeyModel]
        public long? Seq { get; set; }
        public long? SeqDivisaoTurma { get; set; }
        public bool? TurmasPeriodoEncerrado { get; set; }
        public bool? TurmaSituacaoNaoCancelada { get; set; }
    }
}
