using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteApuracaoFrequenciaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteApuracaoFrequenciaException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteApuracaoFrequenciaException)
        {
        }
    }
}