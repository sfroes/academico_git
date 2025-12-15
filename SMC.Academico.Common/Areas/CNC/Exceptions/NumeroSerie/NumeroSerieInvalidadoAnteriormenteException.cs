using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class NumeroSerieInvalidadoAnteriormenteException : SMCApplicationException
    {
        public NumeroSerieInvalidadoAnteriormenteException()
            : base(ExceptionsResource.ERR_NumeroSerieInvalidadoAnteriormenteException)
        { }
    }
}