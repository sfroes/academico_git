using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class GrupoEscalonamentoProcessoTodosEscalonamentosExpiradosException : SMCApplicationException
    {
        public GrupoEscalonamentoProcessoTodosEscalonamentosExpiradosException()
            : base(ExceptionsResource.ERR_GrupoEscalonamentoProcessoTodosEscalonamentosExpiradosException)
        {
        }
    }
}