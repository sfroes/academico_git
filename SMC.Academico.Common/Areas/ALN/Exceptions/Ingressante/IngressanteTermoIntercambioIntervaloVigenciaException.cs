using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteTermoIntercambioIntervaloVigenciaException : SMCApplicationException
    {
        public IngressanteTermoIntercambioIntervaloVigenciaException()
            : base(Resources.ExceptionsResource.ERR_IngressanteTermoIntercambioIntervaloVigenciaException)
        { }
    }
}