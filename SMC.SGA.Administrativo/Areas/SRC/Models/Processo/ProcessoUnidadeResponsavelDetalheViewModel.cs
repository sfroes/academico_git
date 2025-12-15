using SMC.Academico.Common.Areas.SRC.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.SRC.Models
{
    public class ProcessoUnidadeResponsavelDetalheViewModel : SMCViewModelBase
    {
        [SMCKey]
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqProcesso { get; set; }

        [SMCSize(SMCSize.Grid16_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid16_24)]
        [SMCSelect("EntidadesSuperiores")]
        [SMCRequired]
        public long SeqEntidadeResponsavel { get; set; }

        [SMCHidden]
        public string NomeEntidadeResponsavel { get; set; }

        [SMCSize(SMCSize.Grid6_24, SMCSize.Grid24_24, SMCSize.Grid20_24, SMCSize.Grid6_24)]
        [SMCRequired]
        [SMCSelect]
        public TipoUnidadeResponsavel TipoUnidadeResponsavel { get; set; }
    }
}