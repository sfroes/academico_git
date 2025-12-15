using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class LancamentoNotaBancaExaminadoraApuracaoAvaliacaoNaoExisteException : SMCApplicationException
    {
        public LancamentoNotaBancaExaminadoraApuracaoAvaliacaoNaoExisteException()
            : base(ExceptionsResource.ERR_LancamentoNotaBancaExaminadoraApuracaoAvaliacaoNaoExisteException)
        {
        }
    }
}