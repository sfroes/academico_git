using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaComOfertaException : SMCApplicationException
    {
        public CursoFormacaoEspecificaComOfertaException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaComOfertaException)
        {
        }
    }
}
