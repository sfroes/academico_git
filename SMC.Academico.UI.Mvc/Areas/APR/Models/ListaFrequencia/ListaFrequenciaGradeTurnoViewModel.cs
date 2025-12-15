using System;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class ListaFrequenciaGradeTurnoViewModel
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long SeqTurno { get; set; }

        public long HoraInicio { get; set; }

        public long HoraFim { get; set; }
                
        public DateTime? Data { get; set; }

        public int QuantidadeAulas { get; set; }
    }
}
