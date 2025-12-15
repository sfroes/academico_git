using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class TurmasSemVagasException : SMCApplicationException
    {
        public TurmasSemVagasException(string ofertas)
            : base(string.Format(ExceptionsResource.ERR_TurmasSemVagasException, ofertas))
        { }
    }
}
