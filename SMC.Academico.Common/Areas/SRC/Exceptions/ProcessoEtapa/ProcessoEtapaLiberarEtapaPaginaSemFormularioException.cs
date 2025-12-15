using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaPaginaSemFormularioException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaPaginaSemFormularioException()
            : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaPaginaSemFormularioException)
        {
        }
    }
}
