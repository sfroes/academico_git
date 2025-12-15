using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class OfertaCursoInvalidaException : SMCApplicationException
    {
        public OfertaCursoInvalidaException()
            : base(ExceptionsResource.ERR_OfertaCursoInvalidaException)
        {
        }
    }
}