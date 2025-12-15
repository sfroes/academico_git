using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class RequisitoListarMatrizCurricularViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public string CodigoMatrizCurricular { get; set; }

        [SMCHidden]
        public string DescricaoMatrizCurricular { get; set; }

        [SMCHidden]
        public string DescricaoComplementarMatrizCurricular { get; set; }

        [SMCDescription]
        public string DescricaoMatrizFormatada
        {
            get
            {
                var result = $"{DescricaoMatrizCurricular}";

                if (!string.IsNullOrEmpty(DescricaoComplementarMatrizCurricular))
                    result += $" - {DescricaoComplementarMatrizCurricular}";

                return result;
            }
        }
    }
}