using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaDeferidaSemDestinoException : SMCApplicationException
    {
        public SolicitacaoDispensaDeferidaSemDestinoException()
            : base(ExceptionsResource.ERR_SolicitacaoDispensaDeferidaSemDestinoException)
        { }
    }
}