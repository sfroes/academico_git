using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class TipoEntidadeNaoConfiguradaException : SMCApplicationException
    {
        public TipoEntidadeNaoConfiguradaException(string tipoEntidade)
            : base(string.Format(ExceptionsResource.ERR_TipoEntidadeNaoConfiguradoException, tipoEntidade))
        {
        }
    }
}