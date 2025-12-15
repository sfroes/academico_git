using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Aluno.Models
{
    public class HomeTurmaDivisaoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public long? SeqOrigemMaterial { get; set; }

        public string DivisaoTurmaRelatorioDescricao { get; set; }
    }
}