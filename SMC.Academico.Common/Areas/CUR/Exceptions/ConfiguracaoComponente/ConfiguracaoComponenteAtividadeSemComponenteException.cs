using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteAtividadeSemComponenteException : SMCApplicationException
    {
        public ConfiguracaoComponenteAtividadeSemComponenteException()
            : base(ExceptionsResource.ERR_ConfiguracaoComponenteAtividadeSemComponenteException)
        {
        }
    }
}