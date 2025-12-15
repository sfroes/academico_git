using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class HistoricoSituacaoMatrizCurricularOfertaAtivaException : SMCApplicationException
    {
        public HistoricoSituacaoMatrizCurricularOfertaAtivaException()
            : base(ExceptionsResource.ERR_HistoricoSituacaoMatrizCurricularOfertaAtivaException)
        {
        }
    }
}