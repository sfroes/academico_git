using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.FIN.Lookups
{
    public class PessoaJuridicaLookupFiltroViewModel : SMCLookupFilterViewModel
    {
        [SMCDescription]
        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24)]
        public string RazaoSocial { get; set; }

        [SMCSize(SMCSize.Grid6_24)]
        [SMCCnpj]
        public string Cnpj { get; set; }

        [SMCMaxLength(100)]
        [SMCSize(SMCSize.Grid6_24)]
        public string NomeFantasia { get; set; }
    }
}
