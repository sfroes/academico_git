using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaSemParametrizacaoTaxaException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaSemParametrizacaoTaxaException()
            : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaSemParametrizacaoTaxaException)
        {
        }
    }
}
