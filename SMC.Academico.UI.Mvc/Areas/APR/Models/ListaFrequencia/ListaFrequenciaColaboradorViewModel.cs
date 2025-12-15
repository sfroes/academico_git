using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.APR.Models
{
    public class ListaFrequenciaColaboradorViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        [SMCKey]
        public long SeqColaborador { get; set; }

        public string NomeColaborador { get; set; }

    }
}
