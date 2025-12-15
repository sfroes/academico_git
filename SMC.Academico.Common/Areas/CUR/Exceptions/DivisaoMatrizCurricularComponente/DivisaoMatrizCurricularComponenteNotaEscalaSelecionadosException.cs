using SMC.Academico.Common.Areas.CUR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CUR.Exceptions
{
    public class DivisaoMatrizCurricularComponenteNotaEscalaSelecionadosException : SMCApplicationException
    {
        public DivisaoMatrizCurricularComponenteNotaEscalaSelecionadosException()
            : base(ExceptionsResource.ERR_DivisaoMatrizCurricularComponenteNotaEscalaSelecionadosException)
        {
        }
    }
}