using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc;

namespace SMC.SGA.Administrativo.Areas.ORG.Models
{
    public class HierarquiaEntidadeItemCabecalhoViewModel : SMCViewModelBase
    {
        public long Seq { get; set; }

        [SMCMapProperty("Descricao")]
        public string DescricaoCabecalho { get; set; }
    }
}