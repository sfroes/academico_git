using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ConsultaDadosAlunoCiclosLetivosPlanoEstudoTrumasViewModel : SMCDynamicViewModel, ISMCTreeNode, ISMCSeq
    {
        public override long Seq { get; set; }

        public long? SeqPai { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public bool ExibirMenu { get; set; }
    }
}