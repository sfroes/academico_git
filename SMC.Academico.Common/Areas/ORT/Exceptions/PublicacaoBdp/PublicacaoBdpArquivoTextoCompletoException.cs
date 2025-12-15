using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpArquivoTextoCompletoException : SMCApplicationException
    {
        public PublicacaoBdpArquivoTextoCompletoException()
            : base(ExceptionsResource.ERR_PublicacaoBdpArquivoTextoCompletoException)
        {
        }
    }
}