using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class GrupoConfiguracaoAssuntoComponenteException : SMCApplicationException
    {
        public GrupoConfiguracaoAssuntoComponenteException()
            : base(ExceptionsResource.ERR_GrupoConfiguracaoAssuntoComponenteException)
        {
        }
    }
}