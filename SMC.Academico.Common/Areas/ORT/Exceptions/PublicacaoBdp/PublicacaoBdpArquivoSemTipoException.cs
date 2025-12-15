using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class PublicacaoBdpArquivoSemTipoException : SMCApplicationException
    {
        public PublicacaoBdpArquivoSemTipoException()
            : base(ExceptionsResource.ERR_PublicacaoBdpArquivoSemTipoException)
        {
        }
    }
}