using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoExpiradoException : SMCApplicationException
    {
        public GrupoEscalonamentoExpiradoException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoExpiradoException)
        {
        }
    }
}