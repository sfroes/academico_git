using SMC.Academico.Common.Areas.CAM.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CAM.Models
{
    public class InstituicaoTipoEventoParametroViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqInstituicaoTipoEvento { get; set; }

        [SMCDescription]
        [SMCSize(SMCSize.Grid10_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid10_24)]
        [SMCSelect]
        [SMCUnique]
        public TipoParametroEvento? TipoParametroEvento { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect]
        public bool? Obrigatorio { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid12_24, SMCSize.Grid6_24)]
        [SMCSelect]
        public bool? Ativo { get; set; }
    }
}