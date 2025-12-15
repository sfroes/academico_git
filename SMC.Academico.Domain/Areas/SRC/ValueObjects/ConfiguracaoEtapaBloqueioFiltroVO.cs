using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.SRC.ValueObjects
{
    public class ConfiguracaoEtapaBloqueioFiltroVO : SMCPagerFilterData, ISMCMappable
    {
        public long SeqProcesso { get; set; }

        public long SeqProcessoEtapa { get; set; }

        public long SeqConfiguracaoEtapa { get; set; }
    }
}
