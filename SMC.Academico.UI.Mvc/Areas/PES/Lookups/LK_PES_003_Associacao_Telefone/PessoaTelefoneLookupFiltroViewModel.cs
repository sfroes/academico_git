using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.PES.Lookups
{
    public class PessoaTelefoneLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCSelect]
        [SMCSize(Framework.SMCSize.Grid6_24)]
        public TipoTelefone? TipoTelefone { get; set; }
    }
}