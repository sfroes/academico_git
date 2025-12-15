using SMC.Academico.Common.Areas.PES.Constants;
using SMC.Academico.Common.Areas.PES.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class TipoMensagemFiltroDynamicModel : SMCDynamicFilterViewModel
    { 

        [SMCSize(SMCSize.Grid12_24, SMCSize.Grid24_24, SMCSize.Grid24_24, SMCSize.Grid12_24)]
        [SMCMaxLength(100)]
        public string Descricao { get; set; }

        [SMCSelect]
        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        public CategoriaMensagem CategoriaMensagem { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect]
        public bool? PermiteCadastroManual { get; set; } 
    }
}