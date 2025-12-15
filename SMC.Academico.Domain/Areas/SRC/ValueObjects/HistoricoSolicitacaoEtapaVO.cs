using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class HistoricoSolicitacaoEtapaVO : ISMCMappable
    {
        public long SeqSolicitacaoServico { get; set; }
        public long SeqSolicitacaoServicoEtapa { get; set; }

        public PossuiBloqueio PossuiBloqueio { get; set; }

        public string Etapa { get; set; }
    }
}