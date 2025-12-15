using SMC.Academico.Common.Areas.TUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.TUR.Exceptions.Turma
{
    public class TurmaDataFimPeriodoLetivoException : SMCApplicationException
    {
        public TurmaDataFimPeriodoLetivoException()
            : base(ExceptionsResource.ERR_TurmaDataFimPeriodoLetivoException)
        {
        }
    }
}
