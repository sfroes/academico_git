using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoEtapaFiltroData : SMCPagerFilterData, ISMCMappable
    {      
        public long SeqProcesso { get; set; }      
    }
}
