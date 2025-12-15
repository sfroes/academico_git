using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoArquivoAnexoObrigatorioException : SMCApplicationException
    {
        public RegistroDocumentoArquivoAnexoObrigatorioException()
            : base(ExceptionsResource.ERR_RegistroDocumentoArquivoAnexoObrigatorio)

        {
        }
    }
}