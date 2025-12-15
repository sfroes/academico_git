using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class HistoricoEscolarFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqAluno { get; set; }

        public long? SeqOrigemAvaliacao { get; set; }
    }
}