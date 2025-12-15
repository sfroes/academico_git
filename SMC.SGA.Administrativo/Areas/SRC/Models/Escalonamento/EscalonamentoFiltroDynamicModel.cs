using SMC.Academico.Common.Areas.SRC.Constants;
using SMC.Academico.ServiceContract.Areas.SRC.Interfaces;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.Security;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;
using System;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class EscalonamentoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        public long? SeqProcesso { get; set; }
    }
}