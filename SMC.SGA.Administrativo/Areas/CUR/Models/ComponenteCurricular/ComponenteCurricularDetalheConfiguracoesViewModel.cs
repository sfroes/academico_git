using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularDetalheConfiguracoesViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        public long Seq { get; set; }

        [SMCDescription]
        public string DescricaoCompleta { get; set; }

        public SMCMasterDetailList<ComponenteCurricularDetalheDivisoesViewModel> Divisoes { get; set; }
    }
}