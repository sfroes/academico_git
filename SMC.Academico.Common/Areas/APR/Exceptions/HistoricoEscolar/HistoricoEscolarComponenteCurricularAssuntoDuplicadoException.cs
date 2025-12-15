using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class HistoricoEscolarComponenteCurricularAssuntoDuplicadoException : SMCApplicationException
    {
        public HistoricoEscolarComponenteCurricularAssuntoDuplicadoException()
            : base(ExceptionsResource.ERR_HistoricoEscolarComponenteCurricularAssuntoDuplicadoException)
        {
        }
    }
}