using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Html;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class RecuperarPaginaEtapaViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCHidden]
        public long SeqConfiguracaoEtapa { get; set; }

        //[SMCSize(SMCSize.Grid24_24)]
        //[SMCOrientation(SMCOrientation.Vertical)]
        //[SMCCheckBoxList("PaginasDisponiveis")]
        //[SMCRequired]
        //public List<long> SeqPaginas { get; set; }

        //public List<SMCDatasourceItem> PaginasDisponiveis { get; set; }

        [SMCDetail]
        public SMCMasterDetailList<RecuperarPaginaEtapaItemViewModel> PaginasDisponiveis { get; set; }
    }
}