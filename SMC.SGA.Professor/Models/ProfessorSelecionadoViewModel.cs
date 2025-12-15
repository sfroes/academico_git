using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Professor.Models
{
    public class ProfessorSelecionadoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqArquivoLogo { get; set; }
    }
}