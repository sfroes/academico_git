using SMC.Academico.Common.Areas.CNC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CNC.Exceptions
{
    public class NumeroSerieJaUtilizadoException : SMCApplicationException
    {
        public NumeroSerieJaUtilizadoException()
            : base(ExceptionsResource.ERR_NumeroSerieJaUtilizadoException)
        { }
    }
}