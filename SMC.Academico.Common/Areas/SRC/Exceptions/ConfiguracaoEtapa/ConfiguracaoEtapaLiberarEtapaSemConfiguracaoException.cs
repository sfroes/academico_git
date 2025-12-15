using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ConfiguracaoEtapaLiberarEtapaSemConfiguracaoException : SMCApplicationException
    {
        public ConfiguracaoEtapaLiberarEtapaSemConfiguracaoException()
           : base(ExceptionsResource.ERR_ConfiguracaoEtapaLiberarEtapaSemConfiguracaoException)
        {
        }
    }
}
