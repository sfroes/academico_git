using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaPaginaSemEntidadeCompartilhadaException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaPaginaSemEntidadeCompartilhadaException()
            : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaPaginaSemEntidadeCompartilhadaException)
        {
        }
    }
}
