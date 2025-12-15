using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoOfertaDuplicadaException : SMCApplicationException
    {
        public ProcessoSeletivoOfertaDuplicadaException(string oferta) 
            : base(string.Format(ExceptionsResource.ERR_ProcessoSeletivoOfertaDuplicadaException, oferta))
        { }
    }
}
