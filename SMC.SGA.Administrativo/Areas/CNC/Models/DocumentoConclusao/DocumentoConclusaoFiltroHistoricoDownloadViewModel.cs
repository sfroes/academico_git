using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoFiltroHistoricoDownloadViewModel : SMCPagerViewModel, ISMCMappable
    {
        [SMCHidden]
        public long SeqSolicitacaoDocumentoConclusao { get; set; }

        [SMCHidden]
        public string TokenTipoDocumento { get; set; }

        #region Propriedades para ordenação default

        [SMCHidden]
        [SMCSortable(true, true)]
        public string DataInclusao { get; set; }

        #endregion
    }
}