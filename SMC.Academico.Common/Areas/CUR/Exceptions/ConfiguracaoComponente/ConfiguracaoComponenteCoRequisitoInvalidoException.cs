using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteCoRequisitoInvalidoException : SMCApplicationException
    {
        public ConfiguracaoComponenteCoRequisitoInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_ConfiguracaoComponenteCoRequisitoInvalidoException, registros))
        {
        }
    }
}
