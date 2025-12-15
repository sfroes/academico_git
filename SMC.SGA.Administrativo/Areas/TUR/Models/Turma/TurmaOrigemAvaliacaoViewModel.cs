using SMC.Academico.Common.Areas.APR.Enums;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.TUR.Models
{
    public class TurmaOrigemAvaliacaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long? SeqCriterioAprovacao { get; set; }

        public short? QuantidadeGrupos { get; set; }

        public short? QuantidadeProfessores { get; set; }

        public short? NotaMaxima { get; set; }

        public bool? ApurarFrequencia { get; set; }

        public long? SeqEscalaApuracao { get; set; }

        public long? SeqMatrizCurricularOferta { get; set; }

        public string MateriaLecionada { get; set; }

        public TipoOrigemAvaliacao TipoOrigemAvaliacao { get; set; }

        public string Descricao { get; set; }

        public bool? PermiteAvaliacaoParcial { get; set; }

        public bool? MateriaLecionadaObrigatoria { get; set; }
    }
}