using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class NumeroDeDocumentosExcedidoException : SMCApplicationException
    {
        public NumeroDeDocumentosExcedidoException(string tipoDocumento)
            : base(string.Format(ExceptionsResource.ERR_NumeroDeDocumentosExcedidoException, tipoDocumento))
        { }
    }
}
