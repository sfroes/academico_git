using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class IngressanteHistoricoSituacaoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqIngressante { get; set; }

        public SituacaoIngressante SituacaoIngressante { get; set; }
    }
}