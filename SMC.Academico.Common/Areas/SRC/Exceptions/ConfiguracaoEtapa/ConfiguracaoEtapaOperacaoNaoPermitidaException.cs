using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ConfiguracaoEtapaOperacaoNaoPermitidaException : SMCApplicationException
    {
        public ConfiguracaoEtapaOperacaoNaoPermitidaException()
          : base(ExceptionsResource.ERR_ConfiguracaoEtapaOperacaoNaoPermitidaException)
        {
        }
    }
}
