using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaEtapaSemNenhumEscalonamentoException : SMCApplicationException
    {
        public ProcessoEtapaEtapaSemNenhumEscalonamentoException()
            : base(ExceptionsResource.ERR_ProcessoEtapaEtapaSemNenhumEscalonamento)
        {
        }
    }
}