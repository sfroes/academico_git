using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteApuracaoNotaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteApuracaoNotaException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteApuracaoNotaException)
        {
        }
    }
}