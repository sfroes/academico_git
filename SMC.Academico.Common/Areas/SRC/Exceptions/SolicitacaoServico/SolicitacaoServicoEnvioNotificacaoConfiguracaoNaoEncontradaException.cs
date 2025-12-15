using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class SolicitacaoServicoEnvioNotificacaoConfiguracaoNaoEncontradaException : SMCApplicationException
    {
        public SolicitacaoServicoEnvioNotificacaoConfiguracaoNaoEncontradaException()
            : base(ExceptionsResource.ERR_SolicitacaoServicoEnvioNotificacaoConfiguracaoNaoEncontradaException)
        { }
    }
}