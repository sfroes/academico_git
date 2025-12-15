using SMC.Academico.Common.Areas.MAT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.MAT.Exceptions
{
    public class ErroAntesChamarServicoIntegracaoFinanceiroException : SMCApplicationException
    {
        public ErroAntesChamarServicoIntegracaoFinanceiroException(long seq)
            : base(string.Format(ExceptionsResource.ERR_ErroAntesChamarServicoIntegracaoFinanceiroException, seq))
        {
        }
    }
}