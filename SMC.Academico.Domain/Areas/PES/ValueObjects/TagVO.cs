using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.PES.ValueObjects
{
    public class TagVO : ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public TipoTag? TipoTag { get; set; }

        public bool HabilitarTipoPreenchimentoTag { get; set; }

        public TipoPreenchimentoTag? TipoPreenchimentoTag { get; set; }

        public bool HabilitarQueryOrigem { get; set; }

        public string QueryOrigem { get; set; }

        public string InformacaoTag { get; set; }
        public bool MensagemAutomaticoParaManual { get; set; }
        public bool MensagemManualParaAutomatico { get; set; }
    }
}
