using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpArquivoTipoException : SMCApplicationException
    {
        public PublicacaoBdpArquivoTipoException()
            : base(ExceptionsResource.ERR_PublicacaoBdpArquivoTipoException)
        {
        }
    }
}