using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class JaExisteTipoNotificacaoException : SMCApplicationException
    {
        public JaExisteTipoNotificacaoException()
          : base(ExceptionsResource.ERR_JaExisteTipoNotificacaoException)
        {
        }
    }
}
