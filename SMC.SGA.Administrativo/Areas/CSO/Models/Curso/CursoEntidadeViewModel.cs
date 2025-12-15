using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.SGA.Administrativo.Areas.CSO.Controllers;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class CursoEntidadeViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoEntidade { get; set; }

        [SMCHidden]
        public long SeqEntidade { get; set; }

        [SMCHidden]
        public long SeqHierarquiaEntidade { get; set; }

        [SMCHidden]
        public long SeqTipoHierarquiaEntidadeItem { get; set; }

        [SMCRequired]
        [SMCSelect(nameof(CursoDynamicModel.EntidadesSuperior), SortBy = SMCSortBy.Description)]
        [SMCSize(SMCSize.Grid12_24)]
        public long SeqItemSuperior { get; set; }
    }
}