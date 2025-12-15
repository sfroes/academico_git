using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class HierarquiaEntidadeItemExclusaoExternalizadaException : SMCApplicationException
    {
        public HierarquiaEntidadeItemExclusaoExternalizadaException(string entidade)
            : base(string.Format(Resources.ExceptionsResource.ERR_HierarquiaEntidadeItemExclusaoExternalizadaException, entidade))
        { }
    }
}