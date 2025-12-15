using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.UI.Mvc.Areas.FIN.Lookups
{
    public class PessoaJuridicaLookupViewModel : SMCViewModelBase
    {
        [SMCKey]
        public long? Seq { get; set; }

        [SMCDescription]
        [SMCSortable(true)]
        public string RazaoSocial { get; set; }

        [SMCCnpj]
        [SMCSortable(true)]
        public string Cnpj { get; set; }

        [SMCSortable(true)]
        public string NomeFantasia { get; set; }
    }
}
