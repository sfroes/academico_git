using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteCriterioAprovacaoException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteCriterioAprovacaoException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteCriterioAprovacaoException)
        {
        }
    }
}