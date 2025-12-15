using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaCopiaOfertaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCampanhaOrigem { get; set; }

        public List<string> DesconsiderarTokensTipoOferta { get; set; }
    }
}