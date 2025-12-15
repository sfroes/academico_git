using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class ProcessoSolicitacaoViewModel : SMCViewModelBase
    {
        public long SeqProcesso { get; set; }

        public string DescricaoProcesso { get; set; }
    }
}