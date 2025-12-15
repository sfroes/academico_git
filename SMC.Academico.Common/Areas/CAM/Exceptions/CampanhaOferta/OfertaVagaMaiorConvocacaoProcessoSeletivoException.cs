using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class OfertaVagaMaiorConvocacaoProcessoSeletivoException : SMCApplicationException
    {
        public OfertaVagaMaiorConvocacaoProcessoSeletivoException(string processoSeletivo)
            : base(string.Format(ExceptionsResource.ERR_OfertaVagaMaiorConvocacaoProcessoSeletivoException, processoSeletivo))
        { }
    }
}
