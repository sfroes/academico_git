using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;
using System;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class ApuracaoFrequenciaGradeData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqAlunoHistoricoCicloLetivo { get; set; }
        public long SeqEventoAula { get; set; }
        public Frequencia? Frequencia { get; set; }
        public string Observacao { get; set; }
        public string DataObservacao { get; set; }
        public OcorrenciaFrequencia? OcorrenciaFrequencia { get; set; }
        public string DescricaoTipoMensagem { get; set; }

    }
}
