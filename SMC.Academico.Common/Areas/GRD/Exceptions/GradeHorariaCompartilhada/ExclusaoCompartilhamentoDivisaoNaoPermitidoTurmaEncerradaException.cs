using SMC.Framework.Exceptions;
using SMC.Academico.Common.Areas.GRD.Resources;

namespace SMC.Academico.Common.Areas.GRD.Exceptions
{
    public class ExclusaoCompartilhamentoDivisaoNaoPermitidoTurmaEncerradaException : SMCApplicationException
    {
        public ExclusaoCompartilhamentoDivisaoNaoPermitidoTurmaEncerradaException() :
           base(ExceptionsResource.ERR_ExclusaoCompartilhamentoDivisaoNaoPermitidoTurmaEncerradaException)
        {
        }
    }
}
