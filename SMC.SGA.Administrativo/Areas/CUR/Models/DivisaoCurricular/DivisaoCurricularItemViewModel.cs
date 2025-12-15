using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class DivisaoCurricularItemViewModel : SMCViewModelBase, ISMCMappable
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCRequired]
        [SMCOrder(0)]
        [SMCSize(Framework.SMCSize.Grid4_24)]
        [SMCMask("000")]
        [SMCReadOnly]
        public short Numero { get; set; }

        [SMCRequired]
        [SMCOrder(1)]
        [SMCMaxLength(100)]
        [SMCSize(Framework.SMCSize.Grid16_24)]
        public string Descricao { get; set; }
    }
}