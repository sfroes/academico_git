using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ConfiguracaoEtapaPaginaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqConfiguracaoEtapa { get; set; }

        public long SeqProcesso { get; set; }

        public long? SeqProcessoEtapa { get; set; }
    }
}
