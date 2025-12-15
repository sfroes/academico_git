using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeTipoAlteradoException : SMCApplicationException
    {
        public HierarquiaEntidadeTipoAlteradoException()
            : base(Resources.ExceptionsResource.ERR_HierarquiaEntidadeTipoAlteradoException)
        {
        }
    }
}
