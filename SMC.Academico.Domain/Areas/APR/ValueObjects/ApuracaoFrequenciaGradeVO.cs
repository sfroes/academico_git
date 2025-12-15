using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class ApuracaoFrequenciaGradeVO : ISMCSeq, ISMCMappable
    {
        public long Seq { get; set; }
        public long SeqAlunoHistoricoCicloLetivo { get; set; }
        public long SeqEventoAula { get; set; }
        public Frequencia? Frequencia { get; set; }
        public string Observacao { get; set; }
        public string DataObservacao { get; set; }
        public long? SeqMensagem { get; set; }
        public DateTime? DataRetificacao { get; set; }
        public string ObservacaoRetificacao { get; set; }
        public OcorrenciaFrequencia? OcorrenciaFrequencia { get; set; }
        public DateTime DataInclusao { get; set; }
        public long? SeqSolicitacao { get; set; }
        public string DescricaoTipoMensagem { get; set; }
    }
}
