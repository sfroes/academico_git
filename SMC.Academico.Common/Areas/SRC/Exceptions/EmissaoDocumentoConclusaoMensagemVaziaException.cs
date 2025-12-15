using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class EmissaoDocumentoConclusaoMensagemVaziaException : SMCApplicationException
    {
        public EmissaoDocumentoConclusaoMensagemVaziaException() : base(ExceptionsResource.ERR_EmissaoDocumentoConclusaoMensagemVaziaException)
        { }
    }
}
