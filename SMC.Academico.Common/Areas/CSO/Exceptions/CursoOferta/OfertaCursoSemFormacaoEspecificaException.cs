using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class OfertaCursoSemFormacaoEspecificaException : SMCApplicationException
    {
        public OfertaCursoSemFormacaoEspecificaException(string curso)
            : base(string.Format(ExceptionsResource.ERR_OfertaCursoSemFormacaoEspecificaException, curso))
        { }
    }
}