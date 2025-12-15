using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaDeferidaPlanoVazioException : SMCApplicationException
    {
        public SolicitacaoDispensaDeferidaPlanoVazioException()
            : base(ExceptionsResource.ERR_SolicitacaoDispensaDeferidaPlanoVazioException)
        { }
    }
}