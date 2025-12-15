using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaTitulacaoObrigatorioException : SMCApplicationException
    {
        public CursoFormacaoEspecificaTitulacaoObrigatorioException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaTitulacaoObrigatorioException)
        { }
    }
}
