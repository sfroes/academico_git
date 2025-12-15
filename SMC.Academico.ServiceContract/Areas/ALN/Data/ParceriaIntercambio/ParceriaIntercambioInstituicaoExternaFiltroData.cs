using SMC.Framework.DataAnnotations;
using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class ParceriaIntercambioInstituicaoExternaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        [SMCKeyModel]
        public long[] Seqs { get; set; }

        public long SeqParceriaIntercambio { get; set; }
    }
}
 