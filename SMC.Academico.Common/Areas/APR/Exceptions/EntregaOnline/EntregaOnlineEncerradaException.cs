using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class EntregaOnlineEncerradaException : SMCApplicationException
    {
        public EntregaOnlineEncerradaException()
            : base(ExceptionsResource.ERR_EntregaOnlineEncerradaException)
        {
        }
    }
}