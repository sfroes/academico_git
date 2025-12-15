using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteAtividadeSemMatrizException : SMCApplicationException
    {
        public ConfiguracaoComponenteAtividadeSemMatrizException()
            : base(ExceptionsResource.ERR_ConfiguracaoComponenteAtividadeSemMatrizException)
        {
        }
    }
}