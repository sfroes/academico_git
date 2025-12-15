using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.APR.ValueObjects
{
    public class CriterioAprovacaoVO : SMCPagerFilterData, ISMCMappable
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool ApuracaoFrequencia { get; set; }

        public short? PercentualFrequenciaAprovado { get; set; }

        public bool ApuracaoNota { get; set; }

        public short? NotaMaxima { get; set; }

        public short? PercentualNotaAprovado { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public string DescricaoEscalaApuracao { get; set; }

        public TipoArredondamento? TipoArredondamento { get; set; }

        public EscalaApuracaoVO EscalaApuracao { get; set; }
    }
}