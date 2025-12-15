using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoFormacaoEspecificaNaoVigenteCursoOfertaAtivoException : SMCApplicationException
    {
        public CursoFormacaoEspecificaNaoVigenteCursoOfertaAtivoException()
            : base(ExceptionsResource.ERR_CursoFormacaoEspecificaNaoVigenteCursoOfertaAtivoException)
        {
        }
    }
}

