using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataInicioEscalonamentoMenorDataFimEscalonamentoAnteriorException : SMCApplicationException
    {
        public ProcessoEtapaDataInicioEscalonamentoMenorDataFimEscalonamentoAnteriorException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataInicioEscalonamentoMenorDataFimEscalonamentoAnterior)
        {
        }
    }
}