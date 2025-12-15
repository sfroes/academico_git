using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.SRC.Models 
{
    public class ArquivoSecaoPaginaFiltroViewModel : SMCPagerViewModel, ISMCMappable
    {
        public long SeqConfiguracaoEtapaPagina { get; set; }

        public long SeqSecaoPaginaSgf { get; set; }
    }
}