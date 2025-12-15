using SMC.Academico.UI.Mvc.Areas.CSO.Lookups;
using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.SGA.Administrativo.Areas.CAM.Controllers;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;
using SMC.SGA.Administrativo.Areas.FIN.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.FIN.Models
{
    public class ContratoOfertasCursoViewModel : SMCViewModelBase, ISMCStatefulView
    {
        [CursoLookup]
        [SMCHidden(SMCViewMode.List)]
        [SMCRequired]
        [SMCSize(SMCSize.Grid21_24)]
        public List<CursoLookupViewModel> SeqCurso { get; set; }
    }
}