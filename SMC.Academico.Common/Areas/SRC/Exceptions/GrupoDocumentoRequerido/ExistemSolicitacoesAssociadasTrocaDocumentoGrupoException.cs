using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ExistemSolicitacoesAssociadasTrocaDocumentoGrupoException : SMCApplicationException
    {
        public ExistemSolicitacoesAssociadasTrocaDocumentoGrupoException()
            : base(ExceptionsResource.ERR_ExistemSolicitacoesAssociadasTrocaDocumentoGrupoException)
        {
        }
    }
}
