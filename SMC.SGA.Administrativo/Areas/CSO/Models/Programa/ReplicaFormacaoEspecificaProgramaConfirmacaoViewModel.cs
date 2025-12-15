using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class ReplicaFormacaoEspecificaProgramaConfirmacaoViewModel : SMCViewModelBase, ISMCTreeNode
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long? SeqPai { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }
    }
}