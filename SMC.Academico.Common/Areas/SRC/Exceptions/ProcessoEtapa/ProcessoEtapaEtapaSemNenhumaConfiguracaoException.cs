using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaEtapaSemNenhumaConfiguracaoException : SMCApplicationException
    {
        public ProcessoEtapaEtapaSemNenhumaConfiguracaoException()
            : base(ExceptionsResource.ERR_ProcessoEtapaEtapaSemNenhumaConfiguracao)
        {
        }
    }
}