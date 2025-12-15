using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.MAT.Models
{
    public class ChancelaAtividadeViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long SeqItem { get; set; }

        [SMCSize(SMCSize.Grid20_24)]
        public string Descricao { get; set; }

        [SMCSelect("SituacoesItens", StorageType = SMCStorageType.TempData)]
        [SMCSize(SMCSize.Grid4_24)]
        public long SeqSituacaoItemMatricula { get; set; }
    }
}