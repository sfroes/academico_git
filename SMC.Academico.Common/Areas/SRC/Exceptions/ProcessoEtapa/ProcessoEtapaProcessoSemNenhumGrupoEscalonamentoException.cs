using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaProcessoSemNenhumGrupoEscalonamentoException : SMCApplicationException
    {
        public ProcessoEtapaProcessoSemNenhumGrupoEscalonamentoException()
            : base(ExceptionsResource.ERR_ProcessoEtapaProcessoSemNenhumGrupoEscalonamento)
        {
        }
    }
}