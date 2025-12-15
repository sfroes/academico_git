using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.CUR.Models
{
    public class ComponenteCurricularEmentaViewModel : SMCViewModelBase
    {
        public string CodigoComponente { get; set; }

        public string SiglaComponente { get; set; }

        public string DescricaoComponente { get; set; }

        public string DescricaoEmenta { get; set; }
    }
}

