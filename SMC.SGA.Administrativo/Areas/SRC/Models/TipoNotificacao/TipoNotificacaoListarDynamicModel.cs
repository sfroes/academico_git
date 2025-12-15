using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TipoNotificacaoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        [SMCSortable(true)]
        public override long Seq { get; set; }

        [SMCDescription]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        [SMCSortable(true)]
        public bool? PermiteAgendamento { get; set; }
    }
}