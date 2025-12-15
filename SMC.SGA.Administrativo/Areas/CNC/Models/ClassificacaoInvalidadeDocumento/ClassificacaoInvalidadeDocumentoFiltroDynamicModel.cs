using SMC.Academico.Common.Areas.CNC.Enums;
using SMC.Academico.Common.Constants;
using SMC.Framework;
using SMC.Framework.UI.Mvc.DataAnnotations;
using SMC.Framework.UI.Mvc.Dynamic;

namespace SMC.SGA.Administrativo.Areas.CNC.Models
{
    public class ClassificacaoInvalidadeDocumentoFiltroDynamicModel : SMCDynamicFilterViewModel
    {
        [SMCFilter]
        [SMCSize(SMCSize.Grid4_24)]
        public long? Seq { get; set; }

        [SMCFilter]
        [SMCSize(SMCSize.Grid7_24)]
        public string Descricao { get; set; }

        [SMCFilter]
        [SMCSelect]
        [SMCSize(SMCSize.Grid7_24)]
        public TipoInvalidade? TipoInvalidade { get; set; }

        [SMCHidden]
        [SMCParameter(FILTER.INSTITUICAO_ENSINO, true)]
        public long SeqInstituicaoEnsino { get; set; }
    }
}