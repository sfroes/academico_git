using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.SRC.Resources;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ExisteConfiguracaoComTipoNotificacaoException : SMCApplicationException
    {
        public ExisteConfiguracaoComTipoNotificacaoException()
         : base(ExceptionsResource.ERR_ExisteConfiguracaoComTipoNotificacao)
        {
        }
    }
}
