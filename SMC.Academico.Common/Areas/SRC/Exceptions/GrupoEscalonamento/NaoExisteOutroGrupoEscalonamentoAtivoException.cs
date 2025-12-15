using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class NaoExisteOutroGrupoEscalonamentoAtivoException : SMCApplicationException
    {
        public NaoExisteOutroGrupoEscalonamentoAtivoException()
            : base(ExceptionsResource.ERR_NaoExisteOutroGrupoEscalonamentoAtivoException)
        {
        }
    }
}