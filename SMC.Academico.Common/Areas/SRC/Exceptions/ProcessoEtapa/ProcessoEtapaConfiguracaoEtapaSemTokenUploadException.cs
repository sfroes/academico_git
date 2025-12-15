using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaConfiguracaoEtapaSemTokenUploadException : SMCApplicationException
    {
        public ProcessoEtapaConfiguracaoEtapaSemTokenUploadException()
            : base(ExceptionsResource.ERR_ProcessoEtapaConfiguracaoEtapaSemTokenUpload)
        {
        }
    }
}