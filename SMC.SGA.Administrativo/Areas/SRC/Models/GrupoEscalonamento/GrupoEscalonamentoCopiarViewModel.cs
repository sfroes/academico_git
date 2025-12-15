using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoEscalonamentoCopiarViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long SeqGrupoEscalonamento { get; set; }

        [SMCReadOnly]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid18_24)]
        public string DescricaoGrupoEscalonamento { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid24_24)]
        public List<GrupoEscalonamentoCopiarListarItemViewModel> Itens { get; set; }

        [SMCReadOnly]
        [SMCSize(SMCSize.Grid6_24)]
        public short? NumeroDivisaoParcelas { get; set; }

        [SMCRequired]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid24_24)]
        public string Descricao { get; set; }

        [SMCCssClass("smc-sga-mensagem smc-sga-mensagem-informativa")]
        [SMCHideLabel]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCDisplay]
        public string Mensagem { get; set; }
    }
}