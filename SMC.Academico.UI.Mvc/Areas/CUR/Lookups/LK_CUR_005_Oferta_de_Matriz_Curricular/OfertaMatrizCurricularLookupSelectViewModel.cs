using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Lookups
{
    public class OfertaMatrizCurricularLookupSelectViewModel : SMCViewModelBase, ISMCLookupData
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string DescricaoMatrizCurricular { get; set; }

        public string DescricaoUnidade { get; set; }

        public string DescricaoLocalidade { get; set; }

        public string Codigo { get; set; }

        public long SeqMatrizCurricular { get; set; }

        public long SeqEntidadeLocalidade { get; set; }
    }
}