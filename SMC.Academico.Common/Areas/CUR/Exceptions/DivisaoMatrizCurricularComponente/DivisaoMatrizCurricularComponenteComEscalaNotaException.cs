using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteComEscalaNotaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteComEscalaNotaException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteComEscalaNotaException)
        {
        }
    }
}