using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Localidades.UI.Mvc.DataAnnotation;
using SMC.Localidades.UI.Mvc.Models;

namespace SMC.Academico.UI.Mvc.Areas.PES.Models
{
    public class PessoaTelefoneViewModel : SMCViewModelBase, ISMCSeq
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCHidden]
        public long SeqTelefone { get; set; }

        [SMCSize(SMCSize.Grid24_24)]
        //[Address(1, 1, AcceptForeignAddress = true, Correspondence = false, HideMasterDetailButtons = true)]
        [Phone(1, 1)]
        public PhoneList Telefone { get; set; }
    }
}