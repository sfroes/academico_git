using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteInativoException : SMCApplicationException
    {
        public ConfiguracaoComponenteInativoException()
            : base(ExceptionsResource.ERR_ConfiguracaoComponenteInativoException)
        {
        }
    }
}