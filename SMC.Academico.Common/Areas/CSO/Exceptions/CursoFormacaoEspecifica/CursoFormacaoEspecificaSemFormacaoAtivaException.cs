using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaSemFormacaoAtivaException : SMCApplicationException
    {
        public CursoFormacaoEspecificaSemFormacaoAtivaException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaSemFormacaoAtivaException)
        {
        }
    }
}
