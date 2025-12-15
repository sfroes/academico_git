using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaDataFimAnteriorMenorDataInicioException : SMCApplicationException
    {
        public ProcessoEtapaDataFimAnteriorMenorDataInicioException()
            : base(ExceptionsResource.ERR_ProcessoEtapaDataFimAnteriorMenorDataInicio)
        {
        }
    }
}