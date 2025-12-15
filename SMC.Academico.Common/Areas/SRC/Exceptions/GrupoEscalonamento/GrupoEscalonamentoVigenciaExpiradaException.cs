using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoVigenciaExpiradaException : SMCApplicationException
    {
        public GrupoEscalonamentoVigenciaExpiradaException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoVigenciaExpiradaException)
        {
        }
    }
}