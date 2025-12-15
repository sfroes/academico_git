using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ConfiguracaoEtapaEtapaSemPrazoException : SMCApplicationException
    {
        public ConfiguracaoEtapaEtapaSemPrazoException()
           : base(ExceptionsResource.ERR_ConfiguracaoEtapaEtapaSemPrazoException)
        {
        }
    }
}
