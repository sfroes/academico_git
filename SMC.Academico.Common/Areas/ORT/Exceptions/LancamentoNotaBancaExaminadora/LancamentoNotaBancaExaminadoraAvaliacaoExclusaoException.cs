using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class LancamentoNotaBancaExaminadoraAvaliacaoExclusaoException : SMCApplicationException
    {
        public LancamentoNotaBancaExaminadoraAvaliacaoExclusaoException()
            : base(ExceptionsResource.ERR_LancamentoNotaBancaExaminadoraAvaliacaoExclusaoException)
        {
        }
    }
}