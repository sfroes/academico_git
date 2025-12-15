using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteSubstitutoException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteSubstitutoException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteSubstitutoException)
        {
        }
    }
}