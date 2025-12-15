using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaTipoPrazoPeriodoVigenciaException : SMCApplicationException
    {
        public ProcessoEtapaTipoPrazoPeriodoVigenciaException()
            : base(ExceptionsResource.ERR_ProcessoEtapaTipoPrazoPeriodoVigencia)
        {
        }
    }
}