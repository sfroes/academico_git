using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using SMC.Framework.UI.Mvc.Html;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ConfiguracaoComponeteMatrizCabecalhoViewModel : SMCViewModelBase
    {
        public string DescricaoMatriz { get; set; }
        public int TotalComponentes { get; set; }
        public int TotalComponentesComConfiguracao { get; set; }
    }
}