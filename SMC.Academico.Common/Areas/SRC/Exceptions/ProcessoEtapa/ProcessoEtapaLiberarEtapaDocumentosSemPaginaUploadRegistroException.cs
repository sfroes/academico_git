using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaDocumentosSemPaginaUploadRegistroException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaDocumentosSemPaginaUploadRegistroException()
            : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaDocumentosSemPaginaUploadRegistroException)
        {
        }
    }
}
