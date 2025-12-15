using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ExclusaoDocumentoAssociadoSolicitacaoException : SMCApplicationException
    {
        public ExclusaoDocumentoAssociadoSolicitacaoException()
            : base(ExceptionsResource.ERR_ExclusaoDocumentoAssociadoSolicitacaoException)
        {
        }
    }
}
