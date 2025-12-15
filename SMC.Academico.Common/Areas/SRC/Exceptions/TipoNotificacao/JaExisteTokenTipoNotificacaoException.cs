using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class JaExisteTokenTipoNotificacaoException : SMCApplicationException
    {
        public JaExisteTokenTipoNotificacaoException()
          : base(ExceptionsResource.ERR_JaExisteTokenTipoNotificacaoException)
        {
        }
    }
}
