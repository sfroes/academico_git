using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoSemCicloLetivoException : SMCApplicationException
    {
        public SolicitacaoServicoSemCicloLetivoException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoSemCicloLetivoException)
        {
        }
    }
}