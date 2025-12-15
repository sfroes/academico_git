using SMC.Academico.UI.Mvc.Areas.ALN.Lookups;
using SMC.Framework;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.ORT.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMC.SGA.Administrativo.Areas.ORT.Models
{
    public class TrabalhoAcademicoDivisaoComponenteViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCUnique]
        [SMCSelect(nameof(TrabalhoAcademicoDynamicModel.DivisoesComponentes), AutoSelectSingleItem = true, NameDescriptionField = nameof(DescricaoDivisaoComponente))]
        [SMCSize(SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24, SMCSize.Grid22_24)]
        public long SeqDivisaoComponente { get; set; }

        [SMCIgnoreProp]
        public string DescricaoDivisaoComponente { get; set; }

    }
}