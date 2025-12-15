using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class CriterioAprovacaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public bool ApuracaoFrequencia { get; set; }

        public short? PercentualFrequenciaAprovado { get; set; }

        public bool ApuracaoNota { get; set; }

        public short? NotaMaxima { get; set; }

        public short? PercentualNotaAprovado { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        [SMCMapProperty("EscalaApuracao.Descricao")]
        public string DescricaoEscalaApuracao { get; set; }

        public TipoArredondamento? TipoArredondamento { get; set; }
    }
}
