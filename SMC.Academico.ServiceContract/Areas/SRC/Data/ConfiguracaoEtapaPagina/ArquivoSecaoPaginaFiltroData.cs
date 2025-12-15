using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.SRC.Data
{
    public class ArquivoSecaoPaginaFiltroData : SMCPagerFilterData, ISMCMappable
    {
        public long SeqConfiguracaoEtapaPagina { get; set; }
        
        public long SeqSecaoPaginaSgf { get; set; }
    }
}
