using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteDuplicadoException : SMCApplicationException
    {
        public IngressanteDuplicadoException(string acao)
            : base(string.Format(ExceptionsResource.ERR_IngressanteDuplicadoException, acao))
        { }
    }
}