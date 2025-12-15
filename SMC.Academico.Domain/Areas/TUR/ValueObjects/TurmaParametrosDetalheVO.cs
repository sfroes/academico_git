using SMC.Framework.Mapper;

namespace SMC.Academico.Domain.Areas.TUR.ValueObjects
{
    public class TurmaParametrosDetalheVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqConfiguracaoComponente { get; set; }

        public long SeqDivisaoComponente { get; set; }

        public string DivisaoComponenteDescricao { get; set; }

        public short? QuantidadeGrupos { get; set; }

        public short? QuantidadeProfessores { get; set; }

        public short? NotaMaxima { get; set; }

        public bool ApurarFrequencia { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public bool? MateriaLecionadaObrigatoria { get; set; }
    }
}
