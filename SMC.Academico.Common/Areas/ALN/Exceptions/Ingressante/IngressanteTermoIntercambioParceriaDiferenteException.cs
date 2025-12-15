using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteTermoIntercambioParceriaDiferenteException : SMCApplicationException
    {
        public IngressanteTermoIntercambioParceriaDiferenteException()
            : base(Resources.ExceptionsResource.ERR_IngressanteTermoIntercambioParceriaDiferenteException)
        { }
    }
}