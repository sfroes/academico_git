using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class PessoaJuridicaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid7_24)]
        [SMCSortable(true, true)]
        [SMCMaxLength(100)]
        [SMCDescription]
        [SMCRequired]
        [SMCFilter(true, true)]
        public string RazaoSocial { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid4_24, SMCSize.Grid4_24)]
        [SMCCnpj]
        [SMCReadOnly(SMCViewMode.Edit)]
        [SMCRequired]
        [SMCFilter(true, true)]
        public string Cnpj { get; set; }

        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid5_24)]
        [SMCMaxLength(100)]
        [SMCFilter]
        public string NomeFantasia { get; set; }

    }
}