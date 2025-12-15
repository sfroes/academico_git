using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoDispensaComponenteCurricularNaoPermiteDispensaException : SMCApplicationException
    {
        public SolicitacaoDispensaComponenteCurricularNaoPermiteDispensaException(string componentes) 
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoDispensaComponenteCurricularNaoPermiteDispensaException, componentes))
        {
        }
    }
}