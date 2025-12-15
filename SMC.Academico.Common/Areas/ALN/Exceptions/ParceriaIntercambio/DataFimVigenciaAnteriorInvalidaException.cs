using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class DataFimVigenciaAnteriorInvalidaException : SMCApplicationException
    {
        public DataFimVigenciaAnteriorInvalidaException() 
            : base(Resources.ExceptionsResource.ERR_DataFimVigenciaAnteriorInvalidaException)
        { }
    }
}

