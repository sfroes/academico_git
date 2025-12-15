using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaCursadosCreditoException : SMCApplicationException
    {
        public SolicitacaoDispensaCursadosCreditoException(string percentual)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoDispensaCursadosCreditoException, percentual))
        {
        }
    }
}