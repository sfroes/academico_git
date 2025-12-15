using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Models
{
    public class ProrrogarAlunosItemViewModel : SMCViewModelBase
    {
        [SMCSize(Framework.SMCSize.Grid6_24)]
        [SMCRequired]
        public long? CodigoMigracao { get; set; }
    }
}