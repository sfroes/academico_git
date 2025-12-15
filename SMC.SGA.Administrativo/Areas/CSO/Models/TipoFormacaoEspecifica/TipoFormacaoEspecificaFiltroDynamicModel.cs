using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TipoFormacaoEspecificaFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        public TipoFormacaoEspecificaFiltroDynamicModel()
        {
        }

        [SMCFilter(true, true)]
        [SMCKey]
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid2_24)]
        public long Seq { get; set; }

        [SMCDescription]
        [SMCFilter(true, true)]
        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid9_24)]
        public string Descricao { get; set; }

        [SMCFilter(true, true)]
        [SMCOrder(2)]
        [SMCRadioButtonList]
        [SMCSize(SMCSize.Grid4_24)]
        public bool? Ativo { get; set; }
       
    }
}