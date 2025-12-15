using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponeteMatrizListarViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }
        public long SeqMatrizCurricular { get; set; }
        public long SeqCurriculoCursoOferta { get; set; }
        public long SeqComponenteCurricular { get; set; }
        public string DescricaoComponente { get; set; }
        public List<DivisaoMatrizCurricularComponenteListarItemViewModel> DivisaoMatrizCurricularComponentes { get; set; }
        public List<string> DescricoesGrupoCurricularComponente { get; set; }
    }
}