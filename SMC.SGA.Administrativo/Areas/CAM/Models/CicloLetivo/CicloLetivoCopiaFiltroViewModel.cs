using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.Model;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    [SMCGroupedPropertyConfiguration(GroupId = "Destino", Size = SMCSize.Grid12_24, CssClass = "col-md-offset-1 col-lg-offset-1 col-sm-offset-1")]
    public class CicloLetivoCopiaFiltroViewModel : SMCPagerFilterData, ISMCMappable
    {
        [SMCGroupedProperty("Destino")]
        [SMCMask("0000")]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24)]
        public short? AnoDestino { get; set; }

        [SMCHidden]
        public List<long> SeqsCiclosLetivos { get; set; }
    }
}