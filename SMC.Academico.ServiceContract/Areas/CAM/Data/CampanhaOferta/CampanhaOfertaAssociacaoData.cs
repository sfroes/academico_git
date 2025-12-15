using SMC.Framework;
using SMC.Framework.Mapper;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.CAM.Data
{
    public class CampanhaOfertaAssociacaoData : ISMCMappable
    {
        public long SeqCampanha { get; set; }

        public List<CampanhaOfertaData> OfertasCampanha { get; set; }

        public long[] SeqsProcessosSeletivos { get; set; }
        
        public long[] SeqsConvocacoes { get; set; }
    }
}