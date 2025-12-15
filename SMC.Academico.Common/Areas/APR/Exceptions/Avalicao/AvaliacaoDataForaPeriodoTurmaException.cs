using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AvaliacaoDataForaPeriodoTurmaException : SMCApplicationException
    {
        public AvaliacaoDataForaPeriodoTurmaException()
            : base(ExceptionsResource.ERR_AvaliacaoDataForaPeriodoTurmaException)
        {
        }
    }
}