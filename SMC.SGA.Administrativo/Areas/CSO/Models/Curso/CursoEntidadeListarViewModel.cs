using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoEntidadeListarViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCDescription]
        public string DescricaoItemSuperior { get; set; }
    }
}