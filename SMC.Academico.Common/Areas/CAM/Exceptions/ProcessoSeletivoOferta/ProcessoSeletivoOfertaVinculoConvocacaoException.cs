using SMC.Academico.Common.Areas.CAM.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.CAM.Exceptions
{
    public class ProcessoSeletivoOfertaVinculoConvocacaoException : SMCApplicationException
    {
        public ProcessoSeletivoOfertaVinculoConvocacaoException()
            : base(ExceptionsResource.ERR_ProcessoSeletivoOfertaVinculoConvocacaoException)
        { }
    }
}
