using SMC.Academico.Common.Areas.DCT.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.DCT.Models
{
    public class CategoriaInstituicaoEnsinoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCOrder(0)]
        [SMCSize(SMCSize.Grid3_24)]
        [SMCFilter]
        public long? Seq { get; set; }

        [SMCMaxLength(100)]
        [SMCOrder(1)]
        [SMCSize(SMCSize.Grid10_24)]
        [SMCFilter]
        public string Descricao { get; set; }
    }
}