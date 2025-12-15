using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaSGFViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]        
        public string Descricao { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid16_24)]
        public string DescricaoExibir { get { return this.Descricao; } }

        [SMCHidden]
        public bool Obrigatorio { get; set; }

        [SMCDisplay]
        [SMCSize(SMCSize.Grid4_24)]
        public bool ObrigatorioExibir { get { return this.Obrigatorio; } }

        [SMCSize(SMCSize.Grid4_24)]
        [SMCConditionalReadonly(nameof(Obrigatorio), SMCConditionalOperation.Equals, true, PersistentValue = true)]
        public bool? AssociarEtapa { get; set; }
    }
}