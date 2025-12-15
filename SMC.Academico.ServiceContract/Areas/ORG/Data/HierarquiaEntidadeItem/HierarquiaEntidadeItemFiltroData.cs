using SMC.Academico.Common.Areas.ORG.Enums;
using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ORG.Data
{
    public class HierarquiaEntidadeItemFiltroData : SMCPagerFilterData, ISMCMappable
    {        
        [SMCKeyModel]
        public long? Seq { get; set; }
     
        public TipoVisao? TipoVisaoHierarquia { get; set; }

    }
}
