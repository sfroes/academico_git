using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class CarregarIngressantesViewModel : SMCViewModelBase
    {
        public long SeqChamada { get; set; }

        public long SeqAgendamento { get; set; }

        public long? SeqUltimoHistoricoAgendamento { get; set; }

        public string Campanha { get; set; }

        public string ProcessoSeletivo { get; set; }

        public string Chamada { get; set; }
    }
}