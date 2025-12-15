using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoUnidadeMesmoNomeException : SMCApplicationException
    {
        public CursoUnidadeMesmoNomeException()
            : base(ExceptionsResource.ERR_CursoUnidadeMesmoNomeException)
        { }
    }
}