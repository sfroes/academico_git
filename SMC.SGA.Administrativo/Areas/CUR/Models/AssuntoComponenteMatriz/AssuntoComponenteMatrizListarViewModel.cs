using SMC.Academico.UI.Mvc.Areas.CUR.Lookups;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class AssuntoComponenteMatrizListarViewModel : SMCViewModelBase
    {
        public long seqGrupoCurricularComponente { get; set; }
        public string DescricaoGrupoCurricular { get; set; }
        public long SeqDivisaoMatrizCurricularComponente { get; set; }
        public long SeqMatrizCurricular { get; set; }
        public long SeqCurriculoCursoOferta { get; set; }
        public List<ComponenteCurricularLookupViewModel> Assuntos { get; set; }
    }
}