using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class ConfiguracaoAvaliacaoPPAConcluinteInvalidaException : SMCApplicationException
    {
        public ConfiguracaoAvaliacaoPPAConcluinteInvalidaException()
            : base(ExceptionsResource.ERR_ConfiguracaoAvaliacaoPPAConcluinteInvalidaException)
        {
        }
    }
}