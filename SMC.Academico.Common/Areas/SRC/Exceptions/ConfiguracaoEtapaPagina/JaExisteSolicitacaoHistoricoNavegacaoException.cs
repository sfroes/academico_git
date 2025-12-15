using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class JaExisteSolicitacaoHistoricoNavegacaoException : SMCApplicationException
    {
        public JaExisteSolicitacaoHistoricoNavegacaoException()
          : base(ExceptionsResource.ERR_JaExisteSolicitacaoHistoricoNavegacaoException)
        {
        }
    }
}
