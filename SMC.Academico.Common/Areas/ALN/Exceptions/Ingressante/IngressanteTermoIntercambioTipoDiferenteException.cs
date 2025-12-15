using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteTermoIntercambioTipoDiferenteException : SMCApplicationException
    {
        public IngressanteTermoIntercambioTipoDiferenteException()
            : base(Resources.ExceptionsResource.ERR_IngressanteTermoIntercambioTipoDiferenteException)
        { }
    }
}