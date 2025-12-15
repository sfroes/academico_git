using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeItemAssociacaoEntidadeExternadaException : SMCApplicationException
    {
        public HierarquiaEntidadeItemAssociacaoEntidadeExternadaException()
            : base(Resources.ExceptionsResource.ERR_HierarquiaEntidadeItemAssociacaoEntidadeExternadaException)
        {
        }
    }
}
