using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class TipoVinculoNivelEnsinoCursoJaAssociadaException : SMCApplicationException
    {
        public TipoVinculoNivelEnsinoCursoJaAssociadaException()
          : base(ExceptionsResource.ERR_TipoVinculoNivelEnsinoCursoJaAssociadaException)
        {
        }
    }
}
