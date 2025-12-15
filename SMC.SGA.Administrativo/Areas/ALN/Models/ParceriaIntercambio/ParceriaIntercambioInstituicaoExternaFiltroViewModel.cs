using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.ALN.Models
{
    public class ParceriaIntercambioInstituicaoExternaFiltroViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long? Seq { get; set; }

        [SMCHidden]
        [SMCSize(SMCSize.Grid18_24)]
        public string Nome { get; set; }

        [SMCHidden]
        public string Sigla { get; set; }

        [SMCDescription]
        public string Descricao
        {
            get
            {
                if (Nome != null || Sigla != null)
                {
                    return $"{Nome} - {Sigla}";
                }
                return null;
            }
        }
    }
}