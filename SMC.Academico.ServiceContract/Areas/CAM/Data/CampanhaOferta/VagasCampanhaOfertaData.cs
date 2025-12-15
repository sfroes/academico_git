using SMC.Framework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class VagasCampanhaOfertaData : ISMCMappable
    {
        public long SeqCampanha { get; set; }

        public List<CampanhaOfertaListaData> CampanhaOfertas { get; set; }

        //public long[] SeqsCampanhaOfertas { get; set; }

        public long[] SeqsProcessosSeletivos { get; set; }

        public long[] SeqsConvocacoes { get; set; }
    }
}
