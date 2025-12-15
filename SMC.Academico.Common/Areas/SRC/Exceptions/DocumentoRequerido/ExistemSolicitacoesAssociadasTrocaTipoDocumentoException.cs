using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ExistemSolicitacoesAssociadasTrocaTipoDocumentoException : SMCApplicationException
    {
        public ExistemSolicitacoesAssociadasTrocaTipoDocumentoException()
            : base(ExceptionsResource.ERR_ExistemSolicitacoesAssociadasTrocaTipoDocumentoException)
        {
        }
    }
}
