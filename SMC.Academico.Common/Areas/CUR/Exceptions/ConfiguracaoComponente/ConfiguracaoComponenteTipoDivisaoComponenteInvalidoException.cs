using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteTipoDivisaoComponenteInvalidoException : SMCApplicationException
    {
        public ConfiguracaoComponenteTipoDivisaoComponenteInvalidoException()
            : base(ExceptionsResource.ERR_ConfiguracaoComponenteTipoDivisaoComponenteInvalidoException)
        {
        }
    }
}