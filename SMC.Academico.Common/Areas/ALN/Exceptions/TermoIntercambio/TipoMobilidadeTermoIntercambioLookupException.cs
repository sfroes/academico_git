using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TipoMobilidadeTermoIntercambioLookupException : SMCApplicationException
    {
        public TipoMobilidadeTermoIntercambioLookupException() 
            : base(Resources.ExceptionsResource.ERR_TipoMobilidadeTermoIntercambioLookupException)
        { }
    }
}

