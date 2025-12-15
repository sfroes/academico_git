using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaSemNenhumaEtapaComConfiguracaoDocumentosException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaSemNenhumaEtapaComConfiguracaoDocumentosException()
             : base(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaSemNenhumaEtapaComConfiguracaoDocumentosException)
        {}
    }
}