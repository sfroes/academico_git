using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoOfertaConvocacaoDuplicadaException : SMCApplicationException
    {
        public ProcessoSeletivoOfertaConvocacaoDuplicadaException(string oferta) 
            : base(string.Format(ExceptionsResource.ERR_ProcessoSeletivoOfertaConvocacaoDuplicadaException, oferta))
        { }
    }
}
