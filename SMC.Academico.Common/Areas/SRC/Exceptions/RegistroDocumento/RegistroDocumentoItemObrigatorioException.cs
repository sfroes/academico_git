using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class RegistroDocumentoItemObrigatorioException : SMCApplicationException
    {
        public RegistroDocumentoItemObrigatorioException()
            : base(ExceptionsResource.ERR_RegistroDocumentoItemObrigatorio)

        {
        }
    }
}