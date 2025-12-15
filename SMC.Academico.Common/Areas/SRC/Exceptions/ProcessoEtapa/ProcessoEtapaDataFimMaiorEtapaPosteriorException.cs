using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataFimMaiorEtapaPosteriorException : SMCApplicationException
    {
        public ProcessoEtapaDataFimMaiorEtapaPosteriorException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataFimMaiorEtapaPosterior)
        {
        }
    }
}