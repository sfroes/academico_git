using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{   
    public class DocumentoAssociadoGrupoUploadObrigatorioException : SMCApplicationException
    {
        public DocumentoAssociadoGrupoUploadObrigatorioException()
            : base(ExceptionsResource.ERR_DocumentoAssociadoGrupoUploadObrigatorioException)
        {
        }
    }
}
