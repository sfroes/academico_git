using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteBloqueioPessoaException : SMCApplicationException
    {
        public IngressanteBloqueioPessoaException()
            : base(Resources.ExceptionsResource.ERR_IngressanteBloqueioPessoaException)
        { }
    }
}