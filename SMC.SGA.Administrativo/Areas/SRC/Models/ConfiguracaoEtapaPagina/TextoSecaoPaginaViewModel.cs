using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class TextoSecaoPaginaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqConfiguracaoEtapaPagina { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoPagina { get; set; }

        [SMCHidden]
        public long SeqSecaoPaginaSgf { get; set; }

        [SMCSize(SMCSize.Grid8_24)]
        public string DescricaoSecao { get; set; }

        [SMCHidden]
        public string TokenSecao { get; set; }

        [SMCHtml]
        [SMCMultiline]
        [SMCSize(SMCSize.Grid24_24)]
        [SMCConditionalReadonly(nameof(CamposReadyOnly), true, PersistentValue = true)]
        public string Texto { get; set; }

        [SMCHidden]
        public bool CamposReadyOnly { get; set; }
    }
}