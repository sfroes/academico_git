using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class HistoricoSituacaoMatrizCurricularOfertaSituacaoException : SMCApplicationException
    {
        public HistoricoSituacaoMatrizCurricularOfertaSituacaoException()
            : base(ExceptionsResource.ERR_HistoricoSituacaoMatrizCurricularOfertaSituacaoException)
        {
        }
    }
}