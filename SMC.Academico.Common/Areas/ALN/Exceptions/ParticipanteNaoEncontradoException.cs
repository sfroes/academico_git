using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.ALN.Resources;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ParticipanteNaoEncontradoException : SMCApplicationException
    {
        public ParticipanteNaoEncontradoException(string texto)
            : base(string.Format(ExceptionsResource.ERR_ParticipanteNaoEncontradoException, texto))
        {
        }
    }
}
