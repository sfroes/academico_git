using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.Domain.Areas.ALN.ValueObjects
{
    public class TermoIntercambioArquivoVO : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTermoIntercambio { get; set; }

        public long SeqArquivoAnexado { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public string Observacao { get; set; }
    }
}
