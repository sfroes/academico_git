using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoSituacaoPendenteDeferidoAguardandoException : SMCApplicationException
    {
        public RegistroDocumentoSituacaoPendenteDeferidoAguardandoException(string documentos)
             : base(string.Format(ExceptionsResource.ERR_RegistroDocumentoSituacaoPendenteDeferidoAguardando, documentos))
        {}
    }
}