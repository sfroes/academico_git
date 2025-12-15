using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressantesOrientadorDiferenteException : SMCApplicationException
    {
        public IngressantesOrientadorDiferenteException()
            : base(Resources.ExceptionsResource.ERR_IngressantesOrientadorDiferente)
        { }
    }
}