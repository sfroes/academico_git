using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class TipoNotificacaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Token { get; set; }

        public bool PermiteAgendamento { get; set; }

        public string Descricao { get; set; }

        public string Observacao { get; set; }
    }
}