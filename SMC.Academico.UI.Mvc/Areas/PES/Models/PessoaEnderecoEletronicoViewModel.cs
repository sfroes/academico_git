using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models
{
    public class PessoaEnderecoEletronicoViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCHidden]
        public long SeqEnderecoEletronico { get; set; }

        [SMCHidden]
        public bool BloquearEdicao { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        public EnderecoEletronicoViewModel EnderecoEletronico { get; set; }
    }
}