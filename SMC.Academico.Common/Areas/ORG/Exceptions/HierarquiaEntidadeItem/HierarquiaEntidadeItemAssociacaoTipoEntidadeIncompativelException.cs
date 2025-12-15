using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeItemAssociacaoTipoEntidadeIncompativelException : SMCApplicationException
    {
        public HierarquiaEntidadeItemAssociacaoTipoEntidadeIncompativelException()
            : base(Resources.ExceptionsResource.ERR_HierarquiaEntidadeItemAssociacaoTipoEntidadeIncompativelException)
        {
        }
    }
}
