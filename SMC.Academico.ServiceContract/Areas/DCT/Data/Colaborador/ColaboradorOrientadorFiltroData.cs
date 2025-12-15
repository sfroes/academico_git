using SMC.Framework.Mapper;
using SMC.Framework.Model;
using System.Collections.Generic;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorOrientadorFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqCursoOferta { get; set; }

        public long? SeqLocalidade { get; set; }

        public List<long> SeqEntidadeResponsavel { get; set; }
    }
}