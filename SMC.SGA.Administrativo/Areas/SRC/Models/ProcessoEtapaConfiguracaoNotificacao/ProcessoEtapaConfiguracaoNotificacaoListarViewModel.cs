using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaConfiguracaoNotificacaoListarViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public bool ProcessoEncerrado { get; set; }

        public List<ProcessoEtapaViewModel> Etapas { get; set; }
    }
}