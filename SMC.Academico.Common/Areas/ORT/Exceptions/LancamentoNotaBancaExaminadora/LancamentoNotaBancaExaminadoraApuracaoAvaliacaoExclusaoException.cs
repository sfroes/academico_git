using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class LancamentoNotaBancaExaminadoraApuracaoAvaliacaoExclusaoException : SMCApplicationException
    {
        public LancamentoNotaBancaExaminadoraApuracaoAvaliacaoExclusaoException()
            : base(ExceptionsResource.ERR_LancamentoNotaBancaExaminadoraApuracaoAvaliacaoExclusaoException)
        {
        }
    }
}