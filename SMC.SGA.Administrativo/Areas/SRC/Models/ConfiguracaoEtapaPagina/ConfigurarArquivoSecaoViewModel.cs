using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ConfigurarArquivoSecaoViewModel : SMCViewModelBase, ISMCMappable
    {
        public ConfigurarArquivoSecaoViewModel()
        {
            Arquivos = new SMCMasterDetailList<ArquivoSecaoPaginaViewModel>();
        }

        [SMCHidden]
        public long SeqConfiguracaoEtapaPagina { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoPagina { get; set; }

        [SMCHidden]
        public long SeqSecaoPaginaSgf { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoSecao { get; set; }

        [SMCDetail(SMCDetailType.Tabular)]
        [SMCConditionalReadonly(nameof(CamposReadOnly), true, PersistentValue = true)]
        [SMCSize(SMCSize.Grid24_24)]
        public SMCMasterDetailList<ArquivoSecaoPaginaViewModel> Arquivos { get; set; }

        [SMCHidden]
        public bool CamposReadOnly { get; set; }
    }
}