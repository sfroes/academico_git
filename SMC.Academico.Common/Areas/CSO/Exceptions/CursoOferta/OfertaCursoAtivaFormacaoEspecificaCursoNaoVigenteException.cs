using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class OfertaCursoAtivaFormacaoEspecificaCursoNaoVigenteException : SMCApplicationException
    {
        public OfertaCursoAtivaFormacaoEspecificaCursoNaoVigenteException()
            : base(ExceptionsResource.ERR_OfertaCursoAtivaFormacaoEspecificaCursoNaoVigenteException)
        { }
    }
}
