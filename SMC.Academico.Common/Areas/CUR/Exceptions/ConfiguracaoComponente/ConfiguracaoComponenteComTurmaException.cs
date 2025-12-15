using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteComTurmaException : SMCApplicationException
    {
        public ConfiguracaoComponenteComTurmaException(string listaCodigos)
            : base(string.Format(ExceptionsResource.ERR_ConfiguracaoComponenteComTurmaException, listaCodigos))
        {
        }
    }
}