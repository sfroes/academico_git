using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class CondicaoPagamentoRequeridoException : SMCApplicationException
    {
        public CondicaoPagamentoRequeridoException()
            : base(ExceptionsResource.ERR_CondicaoPagamentoRequeridoException)
        {
        }
    }
}