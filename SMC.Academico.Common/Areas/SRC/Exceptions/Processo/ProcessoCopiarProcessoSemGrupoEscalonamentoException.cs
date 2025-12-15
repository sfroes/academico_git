using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoCopiarProcessoSemGrupoEscalonamentoException : SMCApplicationException
    {
        public ProcessoCopiarProcessoSemGrupoEscalonamentoException()
            : base(ExceptionsResource.ERR_ProcessoCopiarProcessoSemGrupoEscalonamento)
        {

        }
    }
}
