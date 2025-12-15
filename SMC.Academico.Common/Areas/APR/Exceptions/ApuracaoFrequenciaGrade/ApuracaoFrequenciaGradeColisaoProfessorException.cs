using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions.Aula
{
    public class ApuracaoFrequenciaGradeColisaoProfessorException : SMCApplicationException
    {
        public ApuracaoFrequenciaGradeColisaoProfessorException()
            : base(ExceptionsResource.ERR_ApuracaoFrequenciaGradeColisaoProfessorException)
        {
        }
    }
}