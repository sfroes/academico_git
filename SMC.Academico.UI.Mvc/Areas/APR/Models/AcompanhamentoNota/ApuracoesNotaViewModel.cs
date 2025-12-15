using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class ApuracoesNotaViewModel : SMCViewModelBase, ISMCMappable
    {
        public decimal? Nota { get; set; }
    }
}