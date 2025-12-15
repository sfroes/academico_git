using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataInicioFimEtapaForaPeriodoProcessoException : SMCApplicationException
    {
        public ProcessoEtapaDataInicioFimEtapaForaPeriodoProcessoException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataInicioFimEtapaForaPeriodoProcesso)
        {
        }
    }
}