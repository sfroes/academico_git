using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Aluno.Areas.FIN.Models
{
    public class NegociacaoParcelaCartaoViewModel : SMCViewModelBase
    {
        public NegociacaoParcelaCartaoViewModel()
        {
            //FIX: Por enquanto a Origem é 1. Futuramente será outra.
            SeqOrigem = 1;
        }

        public int CodigoAluno { get; set; }

        public long CodigoPessoa { get; set; }

        public long SeqOrigem { get; set; }

        public short TipoAluno { get; set; }

        public short SituacaoAluno { get; set; }

        public string CpfAluno { get; set; }
    }
}