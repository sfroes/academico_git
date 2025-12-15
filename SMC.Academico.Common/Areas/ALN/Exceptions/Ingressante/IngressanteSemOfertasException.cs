using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class IngressanteSemOfertasException : SMCApplicationException
    {
        public IngressanteSemOfertasException()
            : base(ExceptionsResource.ERR_IngressanteSemOfertasException)
        { }
    }
}