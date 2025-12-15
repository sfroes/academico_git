using System;

namespace SMC.SGA.Professor.Areas.APR.Models
{
    public class AvaliacaoGradeTurnoViewModel
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqTurno { get; set; }

        public long HoraInicio { get; set; }

        public long HoraFim { get; set; }

        public string Local { get; set; }

        public long? CodigoLocalSEF { get; set; }

        public DateTime? Data { get; set; }

        public int? CodigoUnidadeSEO { get; set; }
    }
}