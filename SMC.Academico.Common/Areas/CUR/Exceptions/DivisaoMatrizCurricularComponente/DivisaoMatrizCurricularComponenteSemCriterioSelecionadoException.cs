using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteSemCriterioSelecionadoException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteSemCriterioSelecionadoException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteSemCriterioSelecionadoException)
        {
        }
    }
}