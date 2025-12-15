using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class ConfiguracaoAvaliacaoPpaTurmaListarDynamicViewModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        public long SeqTurma { get; set; }

        public int? CodigoInstrumento { get; set; }
    }
}