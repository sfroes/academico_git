using SMC.Academico.Common.Areas.ALN.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ALN.Exceptions
{
    public class ExigeOfertaMatrizCurricularConcedeFormacaoException : SMCApplicationException
    {
        public ExigeOfertaMatrizCurricularConcedeFormacaoException()
        : base(ExceptionsResource.ERR_ExigeOfertaMatrizCurricularConcedeFormacaoException)
        {
        }
    }
}
 