using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class OfertaCursoComLocalidadeAtivaEmAtivacaoException : SMCApplicationException
    {
        public OfertaCursoComLocalidadeAtivaEmAtivacaoException()
            : base(ExceptionsResource.ERR_OfertaCursoComLocalidadeAtivaEmAtivacaoException)
        { }
    }
}