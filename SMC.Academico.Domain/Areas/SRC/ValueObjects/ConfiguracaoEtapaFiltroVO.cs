using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoEtapaFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqProcesso { get; set; }       
    }
}
