using SMC.DadosMestres.Common.Areas.PES.Enums;
using SMC.Framework;
using SMC.Framework.UI.Mvc;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Aluno.Areas.SRC.Models
{
    public class EntregaDocumentoDigitalFiliacaoPaginaViewModel : SMCViewModelBase
    {
        [SMCHidden]
        [SMCKey]
        public long Seq { get; set; }

        [SMCHidden]
        public long SeqPessoa { get; set; }

        [SMCSize(SMCSize.Grid4_24, SMCSize.Grid24_24, SMCSize.Grid6_24, SMCSize.Grid4_24)]
        public TipoParentesco TipoParentesco { get; set; }

        [SMCSize(SMCSize.Grid18_24, SMCSize.Grid24_24, SMCSize.Grid14_24, SMCSize.Grid18_24)]
        public string Nome { get; set; }
    }
}