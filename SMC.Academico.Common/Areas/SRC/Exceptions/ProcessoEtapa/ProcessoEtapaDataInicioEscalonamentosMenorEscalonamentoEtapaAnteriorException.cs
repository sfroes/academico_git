using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataInicioEscalonamentosMenorEscalonamentoEtapaAnteriorException : SMCApplicationException
    {
        public ProcessoEtapaDataInicioEscalonamentosMenorEscalonamentoEtapaAnteriorException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataInicioEscalonamentosMenorEscalonamentoEtapaAnterior)
        {
        }
    }
}