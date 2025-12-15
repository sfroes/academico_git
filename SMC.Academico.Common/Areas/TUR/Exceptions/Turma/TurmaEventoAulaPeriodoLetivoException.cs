using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions
{
    public class TurmaEventoAulaPeriodoLetivoException : SMCApplicationException
    {
        public TurmaEventoAulaPeriodoLetivoException()
            : base(ExceptionsResource.ERR_TurmaEventoAulaPeriodoLetivoException)
        {
        }
    }
}