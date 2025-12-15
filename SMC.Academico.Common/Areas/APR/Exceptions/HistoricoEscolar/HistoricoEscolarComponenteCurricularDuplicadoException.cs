using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class HistoricoEscolarComponenteCurricularDuplicadoException : SMCApplicationException
    {
        public HistoricoEscolarComponenteCurricularDuplicadoException()
            : base(ExceptionsResource.ERR_HistoricoEscolarComponenteCurricularDuplicadoException)
        {
        }
    }
}