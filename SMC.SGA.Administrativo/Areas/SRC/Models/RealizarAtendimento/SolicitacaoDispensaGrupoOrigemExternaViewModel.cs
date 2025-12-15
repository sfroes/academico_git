using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class SolicitacaoDispensaGrupoOrigemExternaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public string Descricao { get; set; }

        [SMCHidden]
        public long SeqSolicitacaoDispensaGrupo { get; set; }

        [SMCSelect("ItensOrigensExternas", NameDescriptionField = nameof(Descricao))]
        [SMCSize(SMCSize.Grid22_24)]
        [SMCRequired]
        public long SeqSolicitacaoDispensaOrigemExterna { get; set; }
    }
}