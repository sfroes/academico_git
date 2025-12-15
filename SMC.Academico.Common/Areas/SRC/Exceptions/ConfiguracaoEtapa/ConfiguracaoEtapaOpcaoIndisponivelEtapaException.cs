using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ConfiguracaoEtapaOpcaoIndisponivelEtapaException : SMCApplicationException
    {
        public ConfiguracaoEtapaOpcaoIndisponivelEtapaException()
          : base(ExceptionsResource.ERR_ConfiguracaoEtapaOpcaoIndisponivelEtapaException)
        {
        }
    }
}
