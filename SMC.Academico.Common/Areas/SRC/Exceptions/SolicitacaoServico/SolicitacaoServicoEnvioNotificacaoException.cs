using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoEnvioNotificacaoException : SMCApplicationException
    {
        public SolicitacaoServicoEnvioNotificacaoException(string token)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoEnvioNotificacaoException, token))
        { }
    }
}