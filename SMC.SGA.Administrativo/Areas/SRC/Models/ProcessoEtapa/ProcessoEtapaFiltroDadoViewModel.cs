using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoEtapaFiltroDadoViewModel
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcessoEtapa { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid10_24)]
        public FiltroDado FiltroDado { get; set; }
    }
}