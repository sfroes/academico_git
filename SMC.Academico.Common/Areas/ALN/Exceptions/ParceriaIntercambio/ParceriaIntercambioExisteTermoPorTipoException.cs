using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ParceriaIntercambioExisteTermoPorTipoException : SMCApplicationException
    {
        public ParceriaIntercambioExisteTermoPorTipoException() 
            : base(Resources.ExceptionsResource.ERR_ParceriaIntercambioExisteTermoPorTipoException)
        { }
    }
}

