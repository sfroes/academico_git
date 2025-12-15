using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class TipoDocumentoAcademicoServicoViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoDocumentoAcademico { get; set; }

        [SMCSelect(nameof(TipoDocumentoAcademicoDynamicModel.ServicosDataSource))]
        [SMCRequired]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqServico { get; set; }
    }
}