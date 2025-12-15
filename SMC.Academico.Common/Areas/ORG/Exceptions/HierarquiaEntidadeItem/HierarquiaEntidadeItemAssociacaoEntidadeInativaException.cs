using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeItemAssociacaoEntidadeInativaException : SMCApplicationException
    {
        public HierarquiaEntidadeItemAssociacaoEntidadeInativaException()
            : base(Resources.ExceptionsResource.ERR_HierarquiaEntidadeItemAssociacaoEntidadeInativaException)
        {
        }
    }
}
