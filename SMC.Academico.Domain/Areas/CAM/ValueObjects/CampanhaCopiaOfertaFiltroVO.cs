using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaCopiaOfertaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCampanhaOrigem { get; set; }

        public List<string> DesconsiderarTokensTipoOferta { get; set; }
    }
}