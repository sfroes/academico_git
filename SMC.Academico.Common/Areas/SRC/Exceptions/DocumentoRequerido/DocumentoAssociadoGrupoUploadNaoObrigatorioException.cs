using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{   
    public class DocumentoAssociadoGrupoUploadNaoObrigatorioException : SMCApplicationException
    {
        public DocumentoAssociadoGrupoUploadNaoObrigatorioException()
            : base(ExceptionsResource.ERR_DocumentoAssociadoGrupoUploadNaoObrigatorioException)
        {
        }
    }
}
