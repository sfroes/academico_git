using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteNotaOuEscalaObrigatorioException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteNotaOuEscalaObrigatorioException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteNotaOuEscalaObrigatorioException)
        {
        }
    }
}