using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ParceriaIntercambioExisteTermoCadastradoException : SMCApplicationException
    {
        public ParceriaIntercambioExisteTermoCadastradoException() 
            : base(Resources.ExceptionsResource.ERR_ParceriaIntercambioExisteTermoCadastradoException)
        { }
    }
}

