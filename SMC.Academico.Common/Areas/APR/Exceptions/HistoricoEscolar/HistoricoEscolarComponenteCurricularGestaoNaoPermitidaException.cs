using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class HistoricoEscolarComponenteCurricularGestaoNaoPermitidaException : SMCApplicationException
    {
        public HistoricoEscolarComponenteCurricularGestaoNaoPermitidaException()
            : base(ExceptionsResource.ERR_HistoricoEscolarComponenteCurricularGestaoNaoPermitidaException)
        {
        }
    }
}