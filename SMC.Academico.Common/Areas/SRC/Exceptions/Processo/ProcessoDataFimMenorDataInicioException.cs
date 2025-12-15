using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoDataFimMenorDataInicioException : SMCApplicationException
    {
        public ProcessoDataFimMenorDataInicioException()
            : base(ExceptionsResource.ERR_ProcessoDataFimMenorDataInicio)
        {
        }
    }
}