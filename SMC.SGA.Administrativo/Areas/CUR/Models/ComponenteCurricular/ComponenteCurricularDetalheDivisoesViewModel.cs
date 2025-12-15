using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularDetalheDivisoesViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string DescricaoCompleta { get; set; }

        public string CargaHorariaGradeFormatada { get; set; }
    }
}