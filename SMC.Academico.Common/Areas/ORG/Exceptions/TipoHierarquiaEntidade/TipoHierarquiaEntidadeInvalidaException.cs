using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class TipoHierarquiaEntidadeInvalidaException : SMCApplicationException
    {
        public TipoHierarquiaEntidadeInvalidaException()
            : base(Resources.ExceptionsResource.ERR_TipoHierarquiaEntidadeInvalidaException)
        {

        }
    }
}
