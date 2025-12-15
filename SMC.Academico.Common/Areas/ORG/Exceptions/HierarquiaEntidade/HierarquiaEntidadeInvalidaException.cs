using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeInvalidaException : SMCApplicationException
    {
        public HierarquiaEntidadeInvalidaException()
            : base(Resources.ExceptionsResource.ERR_HierarquiaEntidadeInvalidaException)
        {
        }
    }
}
