using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.APR.Data
{
    public class EscalaApuracaoItemFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long? SeqEscalaApuracao { get; set; }

        public short? Percentual { get; set; }
    }
}