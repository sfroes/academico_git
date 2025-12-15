using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TipoNotificacaoAtributoAgendamentoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoNotificacao { get; set; }

        [SMCRequired]
        [SMCSelect]
        [SMCSize(SMCSize.Grid11_24, SMCSize.Grid24_24, SMCSize.Grid11_24, SMCSize.Grid11_24)]
        public AtributoAgendamento AtributoAgendamento { get; set; }
    }
}