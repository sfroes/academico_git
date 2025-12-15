using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoConflitanteException : SMCApplicationException
    {
        public SolicitacaoServicoConflitanteException(string descricoesConflitos)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoConflitanteException, descricoesConflitos))
        {
        }
    }
}