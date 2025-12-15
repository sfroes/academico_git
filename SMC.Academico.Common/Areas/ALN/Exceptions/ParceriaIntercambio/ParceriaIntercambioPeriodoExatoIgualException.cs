using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ParceriaIntercambioPeriodoExatoIgualException : SMCApplicationException
    {
        public ParceriaIntercambioPeriodoExatoIgualException() 
            : base(Resources.ExceptionsResource.ERR_ParceriaIntercambioPeriodoExatoIgualException)
        { }
    }
}

