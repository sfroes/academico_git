using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class VigenciaInvalidaException : SMCApplicationException
    {
        public VigenciaInvalidaException(string tipo)
            : base(string.Format(Resources.ExceptionsResource.ERR_VigenciaInvalidaException, tipo))
        { }
    }
}

