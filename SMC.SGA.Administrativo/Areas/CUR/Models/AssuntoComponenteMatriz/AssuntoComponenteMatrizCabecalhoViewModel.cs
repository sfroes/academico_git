using SMC.Framework.UI.Mvc;
using SMC.SGA.Administrativo.Areas.CUR.Views.AssuntoComponenteMatriz.App_LocalResources;
using System.Collections.Generic;
using System.Linq;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class AssuntoComponenteMatrizCabecalhoViewModel : SMCViewModelBase
    {
        public string DescricaoMatriz { get; set; }
        public string DescricaoConfiguracaoCompoente { get; set; }
    }
}