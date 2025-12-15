using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteComApenasEscalaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteComApenasEscalaException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteComApenasEscalaException)
        {
        }
    }
}