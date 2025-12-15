using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ExclusaoGrupoDocumentoAssociadoSolicitacaoException : SMCApplicationException
    {
        public ExclusaoGrupoDocumentoAssociadoSolicitacaoException()
            : base(ExceptionsResource.ERR_ExclusaoGrupoDocumentoAssociadoSolicitacaoException)
        {
        }
    }
}
