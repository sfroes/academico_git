using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class CondicaoPagamentoTermoAderidoException : SMCApplicationException
    {
        public CondicaoPagamentoTermoAderidoException()
            : base(ExceptionsResource.ERR_CondicaoPagamentoTermoAderidoException)
        {
        }
    }
}