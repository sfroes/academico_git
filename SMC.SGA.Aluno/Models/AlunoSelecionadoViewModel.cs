using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Aluno.Models
{
    public class AlunoSelecionadoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        public string Descricao { get; set; }

        public long? SeqArquivoLogo { get; set; }
    }
}