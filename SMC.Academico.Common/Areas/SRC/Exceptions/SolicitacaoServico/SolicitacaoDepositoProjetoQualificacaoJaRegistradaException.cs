using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoServico
{
    public class SolicitacaoDepositoProjetoQualificacaoJaRegistradaException : SMCApplicationException
    {
        public SolicitacaoDepositoProjetoQualificacaoJaRegistradaException()
            : base(ExceptionsResource.ERR_SolicitacaoDepositoProjetoQualificacaoJaRegistradaException)
        {
        }
    }
}