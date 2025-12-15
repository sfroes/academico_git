using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoArquivosObrigatoriosException : SMCApplicationException
    {
        public RegistroDocumentoArquivosObrigatoriosException()
            : base(ExceptionsResource.ERR_RegistroDocumentoArquivosObrigatoriosException)

        {
        }
    }
}