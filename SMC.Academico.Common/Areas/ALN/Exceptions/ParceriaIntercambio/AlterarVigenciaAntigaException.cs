using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AlterarVigenciaAntigaException : SMCApplicationException
    {
        public AlterarVigenciaAntigaException() 
            : base(Resources.ExceptionsResource.ERR_AlterarVigenciaAntigaException)
        { }
    }
}

