using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class ConfiguracaoComponenteGerarOrientacaoInvalidoException : SMCApplicationException
    {
        public ConfiguracaoComponenteGerarOrientacaoInvalidoException(string registros)
            : base(string.Format(ExceptionsResource.ERR_ConfiguracaoComponenteGerarOrientacaoInvalidoException, registros))
        {
        }
    }
}
