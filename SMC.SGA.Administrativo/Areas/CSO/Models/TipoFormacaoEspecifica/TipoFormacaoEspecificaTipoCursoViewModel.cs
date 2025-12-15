using SMC.Academico.Common.Areas.CSO.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.CSO.Models
{
    public class TipoFormacaoEspecificaTipoCursoViewModel : SMCViewModelBase
    {
        [SMCHidden]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqTipoFormacaoEspecifica { get; set; }

        [SMCSelect]
        [SMCRequired]
        [SMCSize(SMCSize.Grid8_24, SMCSize.Grid24_24, SMCSize.Grid8_24, SMCSize.Grid8_24)]
        public TipoCurso? TipoCurso { get; set; }
    }
}