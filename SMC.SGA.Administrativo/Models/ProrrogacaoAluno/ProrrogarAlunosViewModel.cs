using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Models
{
    public class ProrrogarAlunosViewModel
    {
        [SMCDetail(SMCDetailType.Tabular, min: 1)]
        [SMCSize(Framework.SMCSize.Grid24_24)]
        public SMCMasterDetailList<ProrrogarAlunosItemViewModel> CodigosMigracao { get; set; }
    }
}