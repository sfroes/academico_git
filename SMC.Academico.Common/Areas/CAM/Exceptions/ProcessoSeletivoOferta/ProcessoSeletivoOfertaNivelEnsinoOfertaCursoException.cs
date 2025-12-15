using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoOfertaNivelEnsinoOfertaCursoException : SMCApplicationException
    {
        public ProcessoSeletivoOfertaNivelEnsinoOfertaCursoException() 
            : base(ExceptionsResource.ERR_ProcessoSeletivoOfertaNivelEnsinoOfertaCursoException)
        { }
    }
}
