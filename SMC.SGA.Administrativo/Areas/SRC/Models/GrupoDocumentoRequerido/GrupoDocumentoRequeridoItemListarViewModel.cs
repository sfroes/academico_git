using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoDocumentoRequeridoItemListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqTipoDocumento { get; set; }        

        [SMCSize(SMCSize.Grid10_24)]
        public string DescricaoTipoDocumento { get; set; }
    }
}