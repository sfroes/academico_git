using SMC.Framework;
using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;
using System;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class DocumentoConclusaoApostilamentoListarViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCParameter]
        [SMCSize(SMCSize.Grid4_24)]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqDocumentoConclusao { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string DescricaoTipoApostilamento { get; set; }

        [SMCSize(SMCSize.Grid10_24)]
        public string Descricao { get; set; }

        [SMCHidden]
        [SMCSortable(true, true, SMCSortDirection.Ascending)]
        public DateTime DataInclusao { get; set; }
    }
}