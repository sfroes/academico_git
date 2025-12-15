using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.SRC.Controllers;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class GrupoDocumentoRequeridoItemViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqGrupoDocumentoRequerido { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(GrupoDocumentoRequeridoViewModel.DocumentosRequeridos))]
        [SMCDependency(nameof(GrupoDocumentoRequeridoViewModel.UploadObrigatorio), nameof(GrupoDocumentoRequeridoController.BuscarDocumentosRequeridosSelect), "GrupoDocumentoRequerido", true, new string[] { nameof(GrupoDocumentoRequeridoViewModel.SeqConfiguracaoEtapa), nameof (GrupoDocumentoRequeridoItemViewModel.SeqDocumentoRequerido) })]
        [SMCSize(SMCSize.Grid22_24, SMCSize.Grid24_24, SMCSize.Grid22_24, SMCSize.Grid22_24)]
        public long SeqDocumentoRequerido { get; set; }
    }
}