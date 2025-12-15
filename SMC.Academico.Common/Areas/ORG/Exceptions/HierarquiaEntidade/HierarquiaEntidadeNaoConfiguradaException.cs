using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeNaoConfiguradaException : SMCApplicationException
    {
        public HierarquiaEntidadeNaoConfiguradaException(string visao)
            : base(string.Format(Resources.ExceptionsResource.ERR_HierarquiaEntidadeNaoConfiguradaException, visao))
        {
        }
    }
}
