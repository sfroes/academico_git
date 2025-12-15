using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteConceitoException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteConceitoException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteConceitoException)
        {
        }
    }
}