using SMC.Academico.Common.Areas.ALN.Enums;
using SMC.Academico.ServiceContract.Areas.ALN.Interfaces;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.ALN.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelFormaIngressoConfirmacaoViewModel  
    {
        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string FormaIngressoConfirmacao { get; set; }

        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid12_24)]
        public string TipoFormaIngressoConfirmacao { get; set; }

    }
}