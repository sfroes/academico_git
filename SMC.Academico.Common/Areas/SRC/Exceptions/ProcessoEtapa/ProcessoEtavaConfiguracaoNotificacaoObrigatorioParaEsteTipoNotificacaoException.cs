using SMC.Academico.Common.Areas.SRC.Resources;
using SMC.Framework.Exceptions;

namespace SMC.Academico.Common.Areas.SRC.Exceptions
{
    public class ProcessoEtavaConfiguracaoNotificacaoObrigatorioParaEsteTipoNotificacaoException : SMCApplicationException
    {
        public ProcessoEtavaConfiguracaoNotificacaoObrigatorioParaEsteTipoNotificacaoException()
             : base(ExceptionsResource.ERR_ProcessoEtavaConfiguracaoNotificacaoObrigatorioParaEsteTipoNotificacao)
        {}
    }
}