using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class TipoOfertaExigeCursoOfertaLocalidadeTurnoException : SMCApplicationException
    {
        public TipoOfertaExigeCursoOfertaLocalidadeTurnoException() : base(ExceptionsResource.ERR_TipoOfertaExigeCursoOfertaLocalidadeTurnoException)
        {
        }
    }
}