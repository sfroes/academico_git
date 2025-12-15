using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeMesmaSiglaException : SMCApplicationException
    {
        public EntidadeMesmaSiglaException(string descricaoTipoEntidade)
            : base(string.Format(ExceptionsResource.ERR_EntidadeMesmaSiglaException, descricaoTipoEntidade))
        {
        }
    }
}