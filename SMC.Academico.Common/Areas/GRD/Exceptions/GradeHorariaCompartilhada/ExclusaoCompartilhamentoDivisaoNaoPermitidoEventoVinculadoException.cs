using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.GRD.Resources;

namespace SMC.Academico.Common.Areas.GRD.Exceptions
{
    public class ExclusaoCompartilhamentoDivisaoNaoPermitidoEventoVinculadoException : SMCApplicationException
    {
        public ExclusaoCompartilhamentoDivisaoNaoPermitidoEventoVinculadoException() :
           base(ExceptionsResource.ERR_ExclusaoCompartilhamentoDivisaoNaoPermitidoEventoVinculadoException)
        {
        }
    }
}
