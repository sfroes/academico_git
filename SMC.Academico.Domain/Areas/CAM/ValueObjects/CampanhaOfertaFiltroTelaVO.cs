using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.Domain.Areas.CAM.ValueObjects
{
    public class CampanhaOfertaFiltroTelaVO : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCampanha { get; set; }

        public long? SeqTipoOferta { get; set; }

        public string Oferta { get; set; }

        public long[] SeqsCampanhaOfertas { get; set; }

        public long[] SeqsProcessosSeletivos { get; set; }        
    }
}
