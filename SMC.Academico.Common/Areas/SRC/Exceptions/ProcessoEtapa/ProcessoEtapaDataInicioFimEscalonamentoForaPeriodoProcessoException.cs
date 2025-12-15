using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataInicioFimEscalonamentoForaPeriodoProcessoException : SMCApplicationException
    {
        public ProcessoEtapaDataInicioFimEscalonamentoForaPeriodoProcessoException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataInicioFimEscalonamentoForaPeriodoProcesso)
        {
        }
    }
}