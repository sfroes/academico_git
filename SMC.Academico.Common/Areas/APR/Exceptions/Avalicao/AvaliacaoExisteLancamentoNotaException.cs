using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AvaliacaoExisteLancamentoNotaException : SMCApplicationException
    {
        public AvaliacaoExisteLancamentoNotaException()
            : base(ExceptionsResource.ERR_AvaliacaoExisteLancamentoNotaException)
        {
        }
    }
}