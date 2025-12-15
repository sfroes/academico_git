using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.DCT.Data
{
    public class ColaboradorInstituicaoExternaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long? SeqColaborador { get; set; }
    }
}