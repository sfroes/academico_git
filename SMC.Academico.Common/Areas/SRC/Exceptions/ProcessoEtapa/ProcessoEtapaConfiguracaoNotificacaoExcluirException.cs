using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtapaConfiguracaoNotificacaoExcluirException : SMCApplicationException
    {
        public ProcessoEtapaConfiguracaoNotificacaoExcluirException()
            : base(ExceptionsResource.ERR_ProcessoEtapaConfiguracaoNotificacaoExcluirException)
        {
        }
    }
}