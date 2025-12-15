using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class EnvioNotificacaoDestinatarioColaboradorCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Nome { get; set; }

        public string CpfOuPassaporte { get; set; }

    }
}