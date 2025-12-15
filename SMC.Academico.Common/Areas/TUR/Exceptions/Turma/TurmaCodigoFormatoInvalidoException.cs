using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaCodigoFormatoInvalidoException : SMCApplicationException
    {
        public TurmaCodigoFormatoInvalidoException()
            : base(ExceptionsResource.ERR_TurmaCodigoFormatoInvalidoException)
        {
        }
    }
}
