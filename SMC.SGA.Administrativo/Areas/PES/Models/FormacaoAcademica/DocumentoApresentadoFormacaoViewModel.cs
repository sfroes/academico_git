using SMC.Framework.Mapper;
using SMC.Framework.UI.Mvc.DataAnnotations;

namespace SMC.SGA.Administrativo.Areas.PES.Models
{
    public class DocumentoApresentadoFormacaoViewModel
    {
        public long? Seq { get; set; }

        public long? SeqFormacaoAcademica { get; set; }

        [SMCKey]
        public long? SeqTitulacaoDocumentoComprobatorio { get; set; }
    }
}