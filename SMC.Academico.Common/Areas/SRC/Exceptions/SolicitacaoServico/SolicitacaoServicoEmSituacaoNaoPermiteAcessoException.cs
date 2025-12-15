using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoEmSituacaoNaoPermiteAcessoException : SMCApplicationException
    {
        public SolicitacaoServicoEmSituacaoNaoPermiteAcessoException(string situacao)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoEmSituacaoNaoPermiteAcessoException, situacao))
        { }
    }
}