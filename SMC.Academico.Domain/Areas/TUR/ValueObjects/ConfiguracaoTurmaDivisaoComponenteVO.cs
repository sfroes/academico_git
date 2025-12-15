using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Academico.Common.Areas.CUR.Enums;
using SMC.Framework;
using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class ConfiguracaoTurmaDivisaoComponenteVO : ISMCMappable, ISMCSeq
    {
        public long Seq { get; set; }
        public long SeqDivisaoComponente { get; set; }
        public short NumeroDivisaoComponente { get; set; }
        public long SeqOrigemAvaliacao { get; set; }
        public long SeqTurma { get; set; }
        public string Descricao { get; set; }
        public TipoGestaoDivisaoComponente TipoGestaoDivisaoComponente { get; set; }
        public long? SeqCriterioAprovacao { get; set; }
        public short? QuantidadeGrupos { get; set; }
        public short? QuantidadeProfessores { get; set; }
        public short? NotaMaxima { get; set; }
        public bool? ApurarFrequencia { get; set; }
        public bool? MateriaLecionadaObrigatoria { get; set; }
        public long? SeqEscalaApuracao { get; set; }
        public TipoEscalaApuracao? TipoEscalaApuracao { get; set; }
    }
}
