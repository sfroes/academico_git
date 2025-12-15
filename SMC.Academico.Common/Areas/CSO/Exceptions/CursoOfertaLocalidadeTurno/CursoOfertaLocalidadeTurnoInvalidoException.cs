using SMC.Academico.Common.Areas.CSO.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CSO.Exceptions
{
    public class CursoOfertaLocalidadeTurnoInvalidoException : SMCApplicationException
    {
        public CursoOfertaLocalidadeTurnoInvalidoException()
            : base(ExceptionsResource.ERR_CursoOfertaLocalidadeTurnoInvalidoException)
        { }
    }
}