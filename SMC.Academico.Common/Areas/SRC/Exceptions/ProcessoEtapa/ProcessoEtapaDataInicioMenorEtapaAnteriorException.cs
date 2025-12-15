using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataInicioMenorEtapaAnteriorException : SMCApplicationException
    {
        public ProcessoEtapaDataInicioMenorEtapaAnteriorException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataInicioMenorEtapaAnterior)
        {
        }
    }
}