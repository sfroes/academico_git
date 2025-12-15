using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class AssociacaoIngressanteOfertaObrigatoriaException : SMCApplicationException
    {
        public AssociacaoIngressanteOfertaObrigatoriaException()
            : base(Resources.ExceptionsResource.ERR_AssociacaoIngressanteOfertaObrigatoriaException)
        { }
    }
}