using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class GrupoConfiguracaoComponenteDiferentesException : SMCApplicationException
    {
        public GrupoConfiguracaoComponenteDiferentesException()
            : base(ExceptionsResource.ERR_GrupoConfiguracaoComponenteDiferentesException)
        {
        }
    }
}