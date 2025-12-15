using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaSemPaginaCobrancaException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaSemPaginaCobrancaException()
            : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaSemPaginaCobrancaException)
        {
        }
    }
}
