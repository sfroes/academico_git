using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteTermoIntercambioDuplicadoException : SMCApplicationException
    {
        public IngressanteTermoIntercambioDuplicadoException()
            : base(Resources.ExceptionsResource.ERR_IngressanteTermoIntercambioDuplicadoException)
        { }
    }
}