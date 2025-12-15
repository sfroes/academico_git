using SMC.Academico.ServiceContract.Areas.CUR.Data;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class PeriodicoListarDynamicModel : SMCDynamicViewModel
    {
        [SMCHidden]
        [SMCKey]
        public override long Seq { get; set; }

        [SMCOrder(1)]
        [SMCSortable(true, true)]
        public string Descricao { get; set; }

        public ClassificacaoPeriodicoData ClassificacaoPeriodico { get; set; }

        public List<QualisPeriodicoViewModel> QualisPeriodico { get; set; }
    }
}