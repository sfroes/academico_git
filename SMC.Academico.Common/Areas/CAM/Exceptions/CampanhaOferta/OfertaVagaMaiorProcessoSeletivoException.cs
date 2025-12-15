using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class OfertaVagaMaiorProcessoSeletivoException : SMCApplicationException
    {
        public OfertaVagaMaiorProcessoSeletivoException()
            : base(ExceptionsResource.ERR_OfertaVagaMaiorProcessoSeletivoException)
        { }
    }
}
