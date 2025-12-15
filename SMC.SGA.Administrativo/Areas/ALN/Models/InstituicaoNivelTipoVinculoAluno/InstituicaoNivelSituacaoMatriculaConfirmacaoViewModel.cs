using SMC.Framework.UI.Mvc.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class InstituicaoNivelSituacaoMatriculaConfirmacaoViewModel
    {
        [SMCIgnoreProp]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public string SituacaoMatriculaConfirmacao { get; set; }
    }
}