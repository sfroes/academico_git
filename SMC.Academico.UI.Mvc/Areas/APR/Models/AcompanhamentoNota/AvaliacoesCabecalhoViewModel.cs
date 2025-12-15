using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class AvaliacoesCabecalhoViewModel : SMCViewModelBase, ISMCMappable
    {
        public string Sigla { get; set; }
        public string Descricao { get; set; }
    }
}