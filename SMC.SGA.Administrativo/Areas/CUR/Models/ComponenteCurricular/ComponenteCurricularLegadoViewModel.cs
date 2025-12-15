using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CUR.Models
{
    public class ComponenteCurricularLegadoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public int CodigoComponenteLegado { get; set; }

        [SMCHidden]
        public string BancoLegado { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid24_24)]
        public string DescricaoLegado { get => $"{CodigoComponenteLegado} - {BancoLegado}"; }
    }
}