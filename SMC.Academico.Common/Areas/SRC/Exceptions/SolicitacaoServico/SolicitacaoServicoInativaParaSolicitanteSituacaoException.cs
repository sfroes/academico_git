using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoInativaParaSolicitanteSituacaoException : SMCApplicationException
    {
        public SolicitacaoServicoInativaParaSolicitanteSituacaoException(string tipoAtuacao, string situacao, string acao)
            : base(string.Format(ExceptionsResource.ERR_SolicitacaoServicoInativaParaSolicitanteSituacaoException, tipoAtuacao, situacao, acao))
        { }
    }
}