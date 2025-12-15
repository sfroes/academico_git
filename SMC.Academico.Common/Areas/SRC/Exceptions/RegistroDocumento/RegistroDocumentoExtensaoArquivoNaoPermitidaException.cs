using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoExtensaoArquivoNaoPermitidaException : SMCApplicationException
    {
        public RegistroDocumentoExtensaoArquivoNaoPermitidaException(string nomeArquivo)
            : base(string.Format(ExceptionsResource.ERR_RegistroDocumentoExtensaoArquivoNaoPermitida, nomeArquivo))
        {
        }
    }
}