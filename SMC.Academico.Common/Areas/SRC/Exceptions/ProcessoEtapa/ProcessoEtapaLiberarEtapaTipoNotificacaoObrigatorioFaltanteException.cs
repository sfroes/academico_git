using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaLiberarEtapaTipoNotificacaoObrigatorioFaltanteException : SMCApplicationException
    {
        public ProcessoEtapaLiberarEtapaTipoNotificacaoObrigatorioFaltanteException(string status)
             : base(string.Format(ExceptionsResource.ERR_ProcessoEtapaLiberarEtapaTipoNotificacaoObrigatorioFaltanteException, status))
        {}
    }
}