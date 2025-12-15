using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class CPFPassaporteTermoIntercambioLookupException : SMCApplicationException
    {
        public CPFPassaporteTermoIntercambioLookupException() 
            : base(Resources.ExceptionsResource.ERR_CPFPassaporteTermoIntercambioLookupException)
        { }
    }
}

