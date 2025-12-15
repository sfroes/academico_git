using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class TermoAdesaoRequeridoException : SMCApplicationException
    {
        public TermoAdesaoRequeridoException()
            : base(ExceptionsResource.ERR_TermoAdesaoRequeridoException)
        {
        }
    }
}