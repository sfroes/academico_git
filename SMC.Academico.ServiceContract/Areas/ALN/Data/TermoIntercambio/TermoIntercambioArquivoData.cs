using SMC.Framework.Mapper;
using SMC.Framework.Model;

namespace SMC.Academico.ServiceContract.Areas.ALN.Data
{
    public class TermoIntercambioArquivoData : ISMCMappable
    {
        public long Seq { get; set; }

        public long SeqTermoIntercambio { get; set; }

        public long SeqArquivoAnexado { get; set; }

        public SMCUploadFile ArquivoAnexado { get; set; }

        public string Observacao { get; set; }
    }
}
