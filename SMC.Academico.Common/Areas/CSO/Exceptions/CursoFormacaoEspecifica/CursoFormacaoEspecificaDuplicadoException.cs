using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaDuplicadoException : SMCApplicationException
    {
        public CursoFormacaoEspecificaDuplicadoException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaDuplicadoException)
        {
        }
    }
}
