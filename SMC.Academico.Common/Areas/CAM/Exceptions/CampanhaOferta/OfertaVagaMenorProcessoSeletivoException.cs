using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class OfertaVagaMenorProcessoSeletivoException : SMCApplicationException
    {
        public OfertaVagaMenorProcessoSeletivoException() 
            : base(ExceptionsResource.ERR_OfertaVagaMenorProcessoSeletivoException)
        { }
    }
}
