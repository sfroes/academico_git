using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ALN.Resources;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class EmailParticipanteNaoEncontradoException : SMCApplicationException
    {
        public EmailParticipanteNaoEncontradoException(string texto)
            : base(string.Format(ExceptionsResource.ERR_EmailParticipanteNaoEncontradoException, texto))
        {
        }
    }
}
