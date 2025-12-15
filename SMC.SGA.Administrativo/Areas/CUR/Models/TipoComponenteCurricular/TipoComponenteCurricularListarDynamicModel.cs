using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;
using System.Collections.Generic;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class TipoComponenteCurricularListarDynamicModel : SMCDynamicViewModel
    {
        [SMCKey]
        public override long Seq { get; set; }

        [SMCDescription]
        public string Descricao { get; set; }

        public string Sigla { get; set; }

        [SMCInclude("TiposDivisao.Modalidade")]
        public List<TipoComponenteCurricularListarTipoDivisaoViewModel> TiposDivisao { get; set; }
    }
}