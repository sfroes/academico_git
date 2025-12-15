using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteTermoIntercambioVagaExcedidaException : SMCApplicationException
    {
        public IngressanteTermoIntercambioVagaExcedidaException()
            : base(Resources.ExceptionsResource.ERR_IngressanteTermoIntercambioVagaExcedidaException)
        { }
    }
}