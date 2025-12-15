using SMC.Academico.Common.Areas.ORG.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.ORG.Exceptions
{
    public class EntidadeHistoricoSituacaoExclusaoException : SMCApplicationException
    {
        public EntidadeHistoricoSituacaoExclusaoException()
            : base(ExceptionsResource.ERR_EntidadeHistoricoSituacaoExclusaoException)
        {
        }
    }
}
