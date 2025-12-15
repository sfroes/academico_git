using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class MatrizCurricularConfiguracaoComponenteNenhumaDivisaoException : SMCApplicationException
    {
        public MatrizCurricularConfiguracaoComponenteNenhumaDivisaoException()
            : base(ExceptionsResource.ERR_MatrizCurricularConfiguracaoComponenteNenhumaDivisao)
        {
        }
    }
}