using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeMesmoNomeException : SMCApplicationException
    {
        public EntidadeMesmoNomeException(string descricaoTipoEntidade)
            : base(string.Format(ExceptionsResource.ERR_EntidadeMesmoNomeException, descricaoTipoEntidade))
        {
        }
    }
}