using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class AssuntoComponenteMatrizViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqGrupoCurricularComponente { get; set; }

        [SMCHidden]
        public bool AssuntoComponente { get; set; } = true;

        [SMCHidden]
        public long SeqDivisaoMatrizCurricularComponente { get; set; }

        [ComponenteCurricularLookup]
        [SMCDependency(nameof(SeqGrupoCurricularComponente))]
        [SMCDependency(nameof(AssuntoComponente))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid24_24)]
        public ComponenteCurricularLookupViewModel ComponentesCurricularSubstitutos { get; set; }
    }
}