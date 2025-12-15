using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions.SolicitacaoServico
{
    public class SolicitacaoDepositoTrabalhoAcademicoJaRegistradaException : SMCApplicationException
    {
        public SolicitacaoDepositoTrabalhoAcademicoJaRegistradaException()
            : base(ExceptionsResource.ERR_SolicitacaoDepositoTrabalhoAcademicoJaRegistradaException)
        {
        }
    }
}