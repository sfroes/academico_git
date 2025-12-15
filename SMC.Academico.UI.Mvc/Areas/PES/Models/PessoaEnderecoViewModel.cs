using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models
{
    public class PessoaEnderecoViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCHidden]
        public long SeqEndereco { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        [Address(1, 1, AcceptForeignAddress = true, Correspondence = false, HideMasterDetailButtons = true)]
        public AddressList Endereco { get; set; }
    }
}