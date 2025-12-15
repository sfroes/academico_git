using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoUnidadeMesmoCursoMesmaUnidadeException : SMCApplicationException
    {
        public CursoUnidadeMesmoCursoMesmaUnidadeException()
            : base(ExceptionsResource.ERR_CursoUnidadeMesmoCursoMesmaUnidadeException)
        { }
    }
}