using SMC.Academico.Common.Areas.APR.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.APR.Exceptions
{
    public class AvaliacaoExisteArquivoAvaliacaoException : SMCApplicationException
    {
        public AvaliacaoExisteArquivoAvaliacaoException()
            : base(ExceptionsResource.ERR_AvaliacaoExisteArquivoAvaliacaoException)
        {
        }
    }
}