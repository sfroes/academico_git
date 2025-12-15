using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteNaoEncontradaException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteNaoEncontradaException()
           : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteNaoEncontradaException)
        {
        }
    }
}
