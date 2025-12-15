using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaTitulacaoDuplicadaException : SMCApplicationException
    {
        public CursoFormacaoEspecificaTitulacaoDuplicadaException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaTitulacaoDuplicadaException)
        { }
    }
}