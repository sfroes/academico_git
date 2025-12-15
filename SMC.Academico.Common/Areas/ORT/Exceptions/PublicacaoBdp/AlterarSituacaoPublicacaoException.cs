using SMC.Academico.Common.Areas.ORT.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORT.Exceptions
{
    public class AlterarSituacaoPublicacaoException : SMCApplicationException
    {
        public AlterarSituacaoPublicacaoException(string situacao)
            : base(string.Format(ExceptionsResource.ERR_AlterarSituacaoPublicacaoException, situacao))
        {
        }
    }
}