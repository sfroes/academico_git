using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.Academico.UI.Mvc.Areas.CNC.Models
{
    public class IntegralizacaoMatrizGrupoBeneficioViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long SeqBeneficio { get; set; }

        [SMCDescription]
        public string DescricaoBeneficio { get; set; }       
    }
}
