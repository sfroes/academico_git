using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class OrigemAvaliacaoData : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }

        public long? SeqCriterioAprovacao { get; set; }

        public short? QuantidadeGrupos { get; set; }

        public short? QuantidadeProfessores { get; set; }

        public short? NotaMaxima { get; set; }

        public bool? ApurarFrequencia { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public EscalaApuracaoData EscalaApuracao { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public string MateriaLecionada { get; set; }

        public TipoOrigemAvaliacao TipoOrigemAvaliacao { get; set; }

        public string Descricao { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }

        public bool? MateriaLecionadaObrigatoria { get; set; }
    }
}
