using SMC.Academico.ServiceContract.Areas.SRC.Data;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
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